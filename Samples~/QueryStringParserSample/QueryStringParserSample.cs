using Reflectis.SDK.Authentication;
using Reflectis.SDK.Core.Authentication;
using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Utilities;
using Reflectis.SDK.Http;
using Reflectis.SDK.RealtimeApi;
using Reflectis.SDK.ReflectisApi;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.ApplicationManagement.Samples
{
    /// <summary>
    /// Parameters arriving in the query string: 
    /// - sessionHash: authentication session hash. Used to authenticate and receive API tokens.
    /// - worldId: used to track experience analytics.
    /// - experienceId: used to create a session and bind analytics to that session.
    /// </summary>
    public class QueryStringParserSample : MonoBehaviour
    {
        [SerializeField] private UrlParametersParserBase urlParametersParser;

        public UnityEvent<WorldDTO> OnWorldRetrieved;
        public UnityEvent<ExperienceDTO> OnExperienceRetrieved;
        public UnityEvent<SessionDTO> OnSessionCreated;
        public UnityEvent<string> OnConnectionCreated;

        private const string WORLD_ID = "worldId";
        private const string SESSION_HASH = "authSessionHash";
        private const string EXPERIENCE_ID = "experienceId";

        private void Awake()
        {
            urlParametersParser.OnUrlParameterdParsed.AddListener(ParseQuerystringParameters);
        }

        private async void ParseQuerystringParameters(Dictionary<string, string> querystring)
        {
            if (!querystring.ContainsKey(WORLD_ID))
            {
                throw new ArgumentNullException($"DeepLinkParserSample: {WORLD_ID} not found in deep link parameters.");
            }

            if (!querystring.ContainsKey(SESSION_HASH))
            {
                throw new ArgumentNullException($"DeepLinkParserSample: {SESSION_HASH} not found in deep link parameters.");
            }

            if (!querystring.ContainsKey(EXPERIENCE_ID))
            {
                throw new ArgumentNullException($"DeepLinkParserSample: {EXPERIENCE_ID} not found in deep link parameters.");
            }

            string sessionHash = querystring[SESSION_HASH];
            if (SM.GetSystem<AuthenticationSystem>().AuthenticationStatus != IAuthenticationSystem.EAuthStatus.Authenticated)
            {
                Debug.Log($"DeepLinkParserSample: reloading session with hash {sessionHash}");
                await SM.GetSystem<IAuthenticationSystem>().ReloadSession(sessionHash);
            }

            UserDTO user = new();
            ApiResponse<UserDTO> userReq = await SM.GetSystem<ReflectisDataAccessSystem>().GetMyUserData();
            if (userReq.IsSuccess)
            {
                user = userReq.Content;
            }
            else
            {
                Debug.LogError($"DeepLinkParserSample: Unable to retrieve user data");
            }

            int worldId = int.Parse(querystring[WORLD_ID]);
            ApiResponse<WorldDTO> worldReq = await SM.GetSystem<ReflectisDataAccessSystem>().GetWorld(worldId);
            if (worldReq.IsSuccess)
            {
                WorldDTO world = worldReq.Content;
                OnWorldRetrieved?.Invoke(world);
                Debug.Log($"DeepLinkParserSample: Successfully retrieved world {worldId} - {world.Label}");
            }
            else
            {
                Debug.LogError($"DeepLinkParserSample: Unable to retrieve world {worldId} - {worldReq.ReasonPhrase}");
            }

            int experienceId = int.Parse(querystring[EXPERIENCE_ID]);
            ApiResponse<ExperienceDTO> experienceReq = await SM.GetSystem<ReflectisDataAccessSystem>().GetExperience(worldId, experienceId);
            if (experienceReq.IsSuccess)
            {
                ExperienceDTO experience = experienceReq.Content;
                OnExperienceRetrieved?.Invoke(experience);
                Debug.Log($"DeepLinkParserSample: Successfully retrieved session data: {experienceId} - {experience.Label}");

                NewSessionDTO newSession = new()
                {
                    Label = $"{experience.Label} - {user.Id} - External experience",
                    StartDate = null,
                    EndDate = null,
                    Multiplayer = false,
                    TagIds = Array.Empty<int>(),
                    Accessibility = ESessionAccessibility.Closed,
                    UserIds = Array.Empty<int>(),
                };

                ApiResponse<SessionDTO> newSessionReq = await SM.GetSystem<ReflectisDataAccessSystem>().CreateSession(worldId, experienceId, newSession);
                if (newSessionReq.IsSuccess)
                {
                    SessionDTO createdSession = newSessionReq.Content;
                    Debug.Log($"DeepLinkParserSample: Successfully created session: {createdSession.Id} - {createdSession.Label}");
                    OnSessionCreated.Invoke(createdSession);

                    RealtimeApiSystem realtimeApiSystem = SM.GetSystem<RealtimeApiSystem>();
                    realtimeApiSystem.ConnectToReflectisRealtime(
                        (handshake) =>
                        {
                            Debug.Log($"DeepLinkParserSample: successfully created websocket connection, client id: {handshake.ConnectionId}");
                            realtimeApiSystem.JoinWorld(worldId, createdSession.Id, (value) =>
                            {
                                Debug.Log($"DeepLinkParserSample: successfully joined world {worldId} with session {createdSession.Id}");
                                OnConnectionCreated?.Invoke(handshake.ConnectionId);
                            });
                        },
                        (reason) =>
                        {
                            Debug.LogError($"DeepLinkParserSample: disconnected from websocket, reason : {reason}");
                        },
                        (kick) =>
                        {
                            Debug.Log($"DeepLinkParserSample: kicked from websocket, {kick}");
                        },
                        () =>
                        {
                            Debug.Log($"DeepLinkParserSample: Double session login");
                        });
                }
                else
                {
                    Debug.LogError($"DeepLinkParserSample: Unable to create session: {experienceId} - {newSessionReq.ReasonPhrase}");
                }
            }
            else
            {
                Debug.LogError($"DeepLinkParserSample: Unable to retrieve session data: {experienceId} - {experienceReq.ReasonPhrase}");
            }
        }
    }
}

