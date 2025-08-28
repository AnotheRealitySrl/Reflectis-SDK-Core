using Reflectis.SDK.Core.Authentication;
using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Http;
using Reflectis.SDK.RealtimeApi;
using Reflectis.SDK.ReflectisApi;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.Core.ApplicationManagement.Samples
{
    /// <summary>
    /// Parametri che arrivano in querystring: 
    /// Parameters arriving in the query string: 
    /// - sessionHash: authentication session hash.Used to call the keepAlive of an already enabled session, 
    /// in order to authenticate and receive API tokens.
    /// - worldId: world ID used to track experience analytics.
    /// - sessionId: websocket connection ID, used to connect from the third-party app to reuse the Reflectis websocket connection smoothly
    /// </summary>
    public class QueryStringParserSample : MonoBehaviour, IDeepLinkPayloadParser
    {
        private const string WORLD_ID = "worldId";
        private const string SESSION_HASH = "sessionHash";
        private const string SESSION_ID = "sessionId";

        public async void ParseDeepLinkPayload(Dictionary<string, string> querystring)
        {
            if (!querystring.ContainsKey(WORLD_ID))
            {
                throw new ArgumentNullException($"DeepLinkParserSample: {WORLD_ID} not found in deep link parameters.");
            }

            if (!querystring.ContainsKey(SESSION_HASH))
            {
                throw new ArgumentNullException($"DeepLinkParserSample: {SESSION_HASH} not found in deep link parameters.");
            }

            if (!querystring.ContainsKey(SESSION_ID))
            {
                throw new ArgumentNullException($"DeepLinkParserSample: {SESSION_ID} not found in deep link parameters.");
            }

            string sessionHash = querystring[SESSION_HASH];

            IAuthenticationSystem authenticationSystem = SM.GetSystem<IAuthenticationSystem>();

            await authenticationSystem.LoadSession(sessionHash);

            int worldId = int.Parse(querystring[WORLD_ID]);
            ApiResponse<WorldDTO> worldReq = await SM.GetSystem<ReflectisDataAccessSystem>().GetWorld(worldId);
            if (worldReq.IsSuccess)
            {
                WorldDTO world = worldReq.Content;
                Debug.Log($"DeepLinkParserSample: Successfully retrieved world {worldId} - {world.Label}");
            }
            else
            {
                Debug.LogError($"DeepLinkParserSample: Unable to retrieve world {worldId} - {worldReq.ReasonPhrase}");
            }

            string sessionId = querystring[SESSION_ID];
            SM.GetSystem<RealtimeApiSystem>().EmbodyConnection(sessionId,
                () =>
                    {
                        Debug.Log($"DeepLinkParserSample: successfully created websocket connection with session id: {sessionId}");
                    },
                () =>
                    {
                        Debug.LogError($"DeepLinkParserSample: unable to create websocket connection with session id: {sessionId}");
                    });
        }
    }
}

