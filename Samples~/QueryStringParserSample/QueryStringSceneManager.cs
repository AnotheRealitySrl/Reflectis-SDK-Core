using Newtonsoft.Json;

using Reflectis.SDK.Authentication;
using Reflectis.SDK.Core.ApplicationManagement.Samples;
using Reflectis.SDK.Core.Authentication;
using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Utilities;
using Reflectis.SDK.Http;

using TMPro;

using UnityEngine;

public class QueryStringSceneManager : MonoBehaviour
{
    [SerializeField] private QueryStringParserSample queryStringParserSample;
    [SerializeField] private UrlParametersParserBase urlParametersParser;

    [SerializeField] private TextMeshProUGUI profile;
    [SerializeField] private TextMeshProUGUI worldData;
    [SerializeField] private TextMeshProUGUI experienceData;
    [SerializeField] private TextMeshProUGUI sessionData;
    [SerializeField] private TextMeshProUGUI websocket;

    private void Awake()
    {
        JsonConverters.SetJsonConvertDefaultSettings();

        SM.OnAllSystemsSetupsDone.AddListener(OnAllSystemsSetupsDone);
    }

    private async void OnAllSystemsSetupsDone()
    {
        Debug.Log("All systems setups done. App is ready.");

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
        });

        queryStringParserSample.OnConnectionCreated.AddListener((handshake) =>
        {
            websocket.text += JsonConvert.SerializeObject(handshake);
        });

        urlParametersParser.ParseUrlParameters();
    }
}
