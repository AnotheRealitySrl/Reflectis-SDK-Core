using Newtonsoft.Json;

using Reflectis.SDK.Authentication;
using Reflectis.SDK.Core.ApplicationManagement.Samples;
using Reflectis.SDK.Core.Authentication;
using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Utilities;
using Reflectis.SDK.Http;
using Reflectis.SDK.ReflectisApi;

using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class QueryStringSceneManager : MonoBehaviour
{
    [SerializeField] private int worldId;
    [SerializeField] private int experienceId;

    [SerializeField] private QueryStringParserSample queryStringParserSample;
    [SerializeField] private UrlParametersParserBase urlParametersParser;

    [Header("Debug")]
    [SerializeField] private TextMeshProUGUI profile;
    [SerializeField] private TextMeshProUGUI worldData;
    [SerializeField] private TextMeshProUGUI experienceData;
    [SerializeField] private TextMeshProUGUI sessionData;
    [SerializeField] private TextMeshProUGUI websocket;

    [Header("Sample analytics")]
    [SerializeField] private string encryptionPassword = "Reflectis2024";
    [SerializeField] private string experienceKey = "sample";

    [SerializeField] private ExperienceJoinDTO sampleExperienceJoin;
    [SerializeField] private ExperienceStartDTO sampleExperienceStart;
    [SerializeField] private ExperienceCompleteDTO sampleExperienceComplete;
    [SerializeField] private ExperienceStepStartDTO sampleExperienceStepStart;
    [SerializeField] private ExperienceStepCompleteDTO sampleExperienceStepComplete;
    [SerializeField] private ExperienceTranscriptDTO sampleExperienceTranscript;


    private int sessionId = -1;
    private string experienceUniqueId;

    public int VerbIntValue { get; set; }
    private EAnalyticVerb Verb => (EAnalyticVerb)(VerbIntValue + 1);

    private void Awake()
    {
        JsonConverters.SetJsonConvertDefaultSettings();

        SM.OnAllSystemsSetupsDone.AddListener(OnAllSystemsSetupsDone);
    }

    private async void OnAllSystemsSetupsDone()
    {
        Debug.Log("All systems setups done. App is ready.");

        // If the user is already authenticated, get and display his profile, otherwise wait for authentication
        // (it will be managed by the QueryStringParserSample).
        if (SM.GetSystem<AuthenticationSystem>().AuthenticationStatus == IAuthenticationSystem.EAuthStatus.Authenticated)
        {
            ApiResponse<object> prefs = await SM.GetSystem<AuthenticationSystem>().GetMyPreferences(false);
            profile.text += JsonConvert.SerializeObject(prefs.Content);
        }
        else SM.GetSystem<AuthenticationSystem>().OnAuthStatusChange.AddListener(async (authState) =>
        {
            if (authState == IAuthenticationSystem.EAuthStatus.Authenticated)
            {
                ApiResponse<object> prefs = await SM.GetSystem<AuthenticationSystem>().GetMyPreferences(false);
                profile.text += JsonConvert.SerializeObject(prefs);
            }
        });

        // register to events fired by the QueryStringParserSample
        queryStringParserSample.OnWorldRetrieved.AddListener((world) =>
        {
            worldData.text += JsonConvert.SerializeObject(world);
        });

        queryStringParserSample.OnExperienceRetrieved.AddListener((experience) =>
        {
            experienceData.text += JsonConvert.SerializeObject(experience);
        });

        queryStringParserSample.OnSessionCreated.AddListener((session) =>
        {
            sessionData.text += JsonConvert.SerializeObject(session);
            sessionId = session.Id;
            experienceUniqueId = GenerateUniqueKey(experienceKey);
        });

        queryStringParserSample.OnConnectionCreated.AddListener((handshake) =>
        {
            websocket.text += JsonConvert.SerializeObject(handshake);
        });


        // Parse the URL parameters and start the flow
        Dictionary<string, string> parameters = urlParametersParser.ParseUrlParameters();
        // Case 1: the application is started from a deep link: parse the parameters from the URL
        if (parameters.Count > 0)
        {
            queryStringParserSample.ParseQuerystringParameters(parameters);
        }
        // Case 2: the application is started in standalone mode: read the hardcoded worldId and experienceId
        // and use them to retrieve data and create a session
        else
        {
            UserDTO user = await queryStringParserSample.RetrieveUserData();
            await queryStringParserSample.RetrieveWorldData(worldId);
            await queryStringParserSample.CreateSessionFromExperience(worldId, experienceId, user.Id);
        }
    }

    /// <summary>
    /// Test method to send an analytic to Reflectis API.
    /// </summary>
    public async void SendAnalytic()
    {
        ExperienceAnalyticDTO experienceAnalyticDTO = null;

        switch (Verb)
        {
            case EAnalyticVerb.ExpJoin:
                experienceAnalyticDTO = sampleExperienceJoin;
                break;

            case
                EAnalyticVerb.ExpStart:
                experienceAnalyticDTO = sampleExperienceStart;
                break;

            case
                EAnalyticVerb.ExpComplete:
                experienceAnalyticDTO = sampleExperienceComplete;
                break;

            case
                EAnalyticVerb.StepStart:
                experienceAnalyticDTO = sampleExperienceStepStart;
                break;

            case
                EAnalyticVerb.StepComplete:
                experienceAnalyticDTO = sampleExperienceStepComplete;
                break;

            case
                EAnalyticVerb.ExpTranscript:
                experienceAnalyticDTO = sampleExperienceTranscript;
                break;

            default:
                Debug.LogError($"Unsupported verb type: {Verb}");
                break;
        }

        experienceAnalyticDTO.SessionId = sessionId;
        experienceAnalyticDTO.uniqueId = experienceUniqueId;
        experienceAnalyticDTO.Statement = null;

        Debug.Log($"Sending analytic: {JsonConvert.SerializeObject(experienceAnalyticDTO)}");

        ApiResponse experienceAnalyticReq = await SM.GetSystem<ReflectisDataAccessSystem>().CreateExperienceAnalytic(experienceAnalyticDTO);
        if (experienceAnalyticReq.IsSuccess)
        {
            Debug.Log("Experience analytic sent successfully.");
        }
        else
        {
            Debug.LogError($"Error sending experience analytic: {experienceAnalyticReq.StatusCode} {experienceAnalyticReq.ReasonPhrase}");
        }
    }


    private string GenerateUniqueKey(string key)
    {
        return $"{DateTime.UtcNow:yyyyMMddHHmmssfff}{sessionId:00000}{StringExtensions.GenerateRandomAlphanumericString(8)}{key}".EncryptDecriptXOR(encryptionPassword);
    }
}
