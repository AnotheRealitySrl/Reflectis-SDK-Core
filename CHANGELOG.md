# Release notes

## Unreleased

### Changed

- BaseSystem: Made system initiation awaitable
- AvatarSystem: Added references to network avatar prefabs
- InteractablePlaceholder: Now accepts script machines instead of scriptable actions to define custom logic
- `MTemplateObj`: changed model color saved data from Color to string, to allow for an empty/null color value
- `IColorPickerSystem`: renamed method used to load saved model color state, from `AssignColorToPicker` to `AssignSavedColorToPicker'
- `IModelExploderSystem`: renamed method used to load saved model explosion state, from `AssignExplosionToModelExploder` to `AssignSavedExplosionToModelExploder'

### Added

- New IApplicationManager: Interfaces that allows visibility on application management methods (QuitApplication, ErasePlayerSessionData, HideEverithing)
- New NetworkUtilities: Class that contains static network utilities methods such as CheckUserInternetConnection
- new VisualScriptingNodes: Added generic interactable event nodes and expose node
- `Manipulable`: new public property `IsSubMesh`, to check if the behavior is on an interactive object or a submesh
- `IColorPickerSystem`: added optional boolean parameter 'networkedContext' to `AssignColorPicker`, to allow for offline/networked color changer component setup
- `IModelExploderSystem`: added optional boolean parameter 'networkedContext' to `AssignModelExploder`, to allow for offline/networked model exploder component setup
- `Manipulable`: new public property `RootManipulable`, which returns the Manipulable component at the root of the interactive object, whether the Manipulable reference is on the root of the interactive object or on one of its submeshes

### Deprecated

### Removed

- Removed awaitable scriptable actions

### Fixed

- `Manipulable`: updated ObjectCenter and ObjectSize properties to be more coherent in checking if the manipulable is on a submesh or not

## v4.0.0

### Changed

- Avatars: refactored loading logic, `AvatarLoaderBase` is the base class for the implementation of any avatar loader and `AvatarLoadersController` handles the choice of the avatar loader.
- Avatars: renamed `AvatarConfigChanged` event to `OnPlayerAvatarConfigChanged`.
- Core: moved auto-initialization of systems to root systems instead of sub-systems.
- Voice: Added Photon global channel and id to communication channels. `CommunicationChannel` constructor is changed to add the id as parameter.
- Utilities: `ContentSearch` parameter of `ApiResponse` is not generic anymore.

### Added

- New ClientModels module, providing the data model used by the business logic and an API definition for data access.
- New TextChat module.
- New Audio module, which allows to manage volume and other audio-specific features.
- New Notification module.
- New Faq module.
- New ObjectSpawner module.
- New Interaction module.
- New Platform module to know in which platform the application is running.
- New Help module useful to implement tutorials.
- New ColorPicker module with base logic for changing the color of an interactable.
- New ModelScaler module with base logic for scaling an interactable.
- Avatars: Implemented avatar loading logic in `AvatarLoadersController`.
- Avatars: added `IAvatarConfigController` reference associated with the avatar instance in `IAvatarSystem`.
- Avatars: added `AvatarId` and `AvatarPNG` properties in `IAvatarConfig`.
- CharacterController: `EnableCharacterMovement`, `EnableCharacterJump`, `GoToInteractState`, `GoToSetMovementState`, `EnableCameraRotation` and `EnableCameraZoom` methods in `ICharacterControllerSystem`
  interface and `CharacterControllerSystem` base class. These methods allow to toggle character controller interaction.
- CharacterController: added AvatarPlacheholder prefab: a purple full-body rigged avatar for general purpose.
- CharacterController: add finger bones, references to interactor colliders, to tag and to player height to `CharacterBase`.
- CharacterController: Added logic to adjust the position of the label above the player.
- Core: Implemented `GetSystem` overload with `ISystem` as type parameter.
- Core: `IsReady` bool property on `SM` to know if a system is ready, and add a static method `DoOnceReady` to trigger a callback once `SM` is has finished setup.
- Fade: immplemented fade with a quad.
- Fade: added `FadeToBackground` and `FadeFromBackground` in `IFadeManager`, which use a `backgroundImage` field in `CanvasFadeManager` and a `backgroundColor` field in `QuadFadeManager`.
- Fade: added fade in/out via material in `CanvasFadeManager`.
- Transitions: implemented `OnEnter` and `OnExit` transition callbacks.
- Transitions: added `TransformTransitionProvider`, `AbstractColorTransitionProvider`, and `FloatTransitionProvider`.
- Transitions: added `DoEnterExitTransitionAsync` method in abstract transition provider, which calls `DoTransitionAsync(true)` and `DoTransitionAsync(true)` in sequence.
- Transitions: added `onEnterTransitionStart` callback and `onExitTransitionFinish` on transition providers.
- Utilities: added Image downloader utility script.
- Utilities: added `ColorExtensions` extension class with `IsVerySimilarToWhite`, `NameToColor` and `HexToColor` methods.
- Utilities: added `DateTimeExtensions` extension class with `ParseStringToDate` method.
- Utilities: added `StringExtensions` extension class with `CopyToClipboard`, `RemoveSpecialCharacters` and `ConcatenateStrings` method.
- Utilities: added `TextAssetExtensions` extension class with `GetTextClassCompleteName` method.
- Utilities: added `GetTextType` and `GetTypeFromString` methods.
- Utilities: added `RecompileProjectEditorWindow` and `RestartProjectEditorWindow`.
- Utilities: added `ResourceLockHandler<T>` utility script.
- Utilities: added `DictionaryExtensions` extension class with `ToDictionary` method which converts an object to a dictionary and `CustomToString` method for debugging purposes.
- Utilities: added `DrawIf` and `OnChangedCall` and `HelpBox` property drawers.
- Utilities: added `IntExtensions` extensions class with `RoundUpDivision` method.
- Utilities: added `totalCount` optional parameter in `ApiResponseArray` constructor.
- Utilities: added `AudioMixerExtension` extension class with `GetLinearFloat` and `DecibelToLinear` methods.
- VoiceChat: added `MuteLocalUser(bool muteAudio)` overload and `GetVoiceDetection()` method in `VoiceChatSystemBase`.

### Deprecated

- Legacy Interaction module (working with old Toast System)

### Removed

- Synchronous scriptable actions working with legacy Interaction module

### Fixed

- Added missing dependencies in package.json
- Updated assembly files names
- Avatars: added avatar existence checks on `AvatarConfigControllerDesktop` to prevent null reference exceptions.
- Avatars: improved avatar activation/deactivation.
- Avatars: add check on avatar bounds for a better display of the label.
- Avatars: added app execution check in `EnableAvatarMeshes` method of `AvatarControllerDesktop`
- Avatars: stored reference of current config in `AvatarConfigControllerBase`
- Avatars: fixed avatar animation issues
- Avatars: fixed layer mask issue
- CharacterController: improved character interaction activation/deactivation.
- Fade: added `CanvasScaler` on fade canvas
- Fade: fixed scale issue of the `CanvasFadeManagerDesktop`
- Transitions: `CanvasTransitionProvider` now kills the active transition before executing the new one
- Transitions: improve `AnimationTransitionProvider` behaviour and added null checks in all transition providers
- Utilities: fixed private reference to `TextMeshPro` item in `TMProUGUIHyperlinks`.
- Utilities: replaced `Parse` occurrences in `ApiResponse` with a more safe `TryParse` method
- Utilities: changed `WaitForSeconds` instruction to `WaitForSecondsRealtime` in `MonoBehaviourExtensions`, due to WebGL issues

## v3.0.0

### Changed

- Massive refactor SPACS -> Reflectis
- Utilities: merged Extensions module into Utilities one
- Fade: renamed "FadeSystem" occurrences (asmdef, namespaces, etc.) in "Fade"
- VoiceChat: renamed "Communication" occurrences (asmdef, namespaces, etc.) in "VoiceChat"
- CharacterController: change methods signatures in `ICharacterControllerSystem` API for better clarity
- Avatar: change methods signatures in `IAvatarSystem` API for better clarity

### Added

- Core module (moved from SPACS.Core)
- New Interaction module, with interactable objects base logic and scriptable actions from Core module, and with new `IDesktopInteractionSystem` interface
- New Object Spawner system
- Avatars: Spawn method in AvatarSystem's API
- Avatars: onBeforeAction and onAfterAction callbacks on avatar customization change
- Transitions: flag for performing reverse transitions in `AbstractTransitionsProvider`
- Transitions: `AnimatorTransitionProvider` now has a configurable string parameter
- Communication system: system base class
- Documentation
- Fade System with canvas

### Fixed

- Fade: condition that checks when all scenes have been loaded/unloaded

---

## v2.0.0

### Changed

- Migrate to Reflectis system framework

### Added

- Fade system (with URP volume fade sample)
- Scene loader system
- Character controller system
- Avatar system
- Communication system
- Extensions module
- Utilities module
- Transitions module

---

## v1.0.0

Initial release
