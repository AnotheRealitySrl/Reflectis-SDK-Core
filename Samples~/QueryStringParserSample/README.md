# How to use the sample

## Retrieving information from Reflectis Worlds

The `QuerystringParserSample` scene contains everything needed to parse data from the browser URL querystring and convert it into the data required to connect an external app to the Reflectis ecosystem.

The `GameManager` GameObject contains the main scripts for managing the flow.

`BrowserUrlParser` is the WebGL-specific script responsible for reading the URL and parsing the querystring parameters.  
It provides the method `ParseUrlParameters()`, which returns a `Dictionary<string, string>` representing the querystring keys and values.  
Note: this version reads the browser URL, so it's compatible with WebGL.  
For the VR version, use the `AndroidDeepLinkParser` script, which works similarly but reads parameters from a deeplink.

`QuerystringParserSample` is the script responsible for translating the querystring parameters and injecting them into the SDK logic.  
In theory, it can be used without modification.  
The following steps are fully implemented by `QuerystringParserSample` and are listed here for clarity.

To connect to Reflectis services, the external application needs three parameters:

- `authSessionHash`: the session hash used to identify the user  
  and make HMAC header calls to the profile API, in order to obtain tokens for other APIs.
- `worldId`: the ID of the world from which the user accessed the external app.  
  This is needed because experience and presence metrics are indexed by world.
- `experienceId`: this ID is mainly used for analytics, both for platform presence and experience tracking.
  - For presence analytics, the external app must create a single-player session associated with the experience  
    using the ID provided by Reflectis Worlds, and then connect via websocket to the Realtime API to ping its presence.
  - The session ID is also used to track experience analytics, which must be implemented directly by the developer.

## Tracking Analytics

To save an experience analytic, you need to call the Reflectis API endpoint: `CreateExperienceAnalytic(ExperienceAnalyticDTO analytic)`.  
The Reflectis API is accessible via the public methods of `DataAccessSystem`.

The `ExperienceAnalyticDTO` object is structured as follows:

```
public class ExperienceAnalyticDTO
{
    EAnalyticVerb verb;
    int sessionId; // Deve essere valorizzato con l'id di sessione creato in precedenza
    object customAttributes;
    XAPIStatement statement; // Deve essere settato a null, altrimenti l'analitica viene mandata alle XAPI
    string locale; // Serve solo per XAPI, si può ignorare
    string context;
}
```

However, this object is abstract, so in practice you will need to instantiate one of its derived objects:  
`ExperienceJoinDTO`, `ExperienceStartDTO`, `ExperienceCompleteDTO`, `ExperienceStepStartDTO`, `ExperienceStepCompleteDTO`, `ExperienceTranscriptDTO`.
