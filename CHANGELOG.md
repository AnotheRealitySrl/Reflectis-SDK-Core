# Release notes

## v4.0.0

### Changed

- Change `AvatarConfigManager` to `AvatarConfigController` and separation on avatar loading logic
- Core: move auto-initialization of systems from subsystem to root systems
- Avatars: rename `AvatarConfigChanged` event to `OnPlayerAvatarConfigChanged`;
<!-- - Voice: add channel id to communication channels -->
- ContentSearch parameter of `ApiResponse` is not generic anymore

### Added

- New clientModels module, providing the data model used by the business logic and an API definition for data access
- New TextChat module
- New Audio module
- New Notification module
- New Faq module
- New ObjectSpawner module
- New Interaction module
- New Platform
- New Help module
- New ColorPicker module
- New ModelScaler module
- Avatar loading logic, `AvatarLoaderBase` is the base class for the implementation of any Avatar Loader and `AvatarLoadersController` handles the choice of the avatar loader.
- Fade: Fade implementation with a quad
- Core: `IsReady` bool property on `SM` to know if a system is ready, and add a static method `DoOnceReady` to trigger a callback once `SM` is has finished setup
- Transitions: `OnEnter` and `OnExit` transition callbacks
- Core: `GetSystem` implementation with `ISystem` as type parameter
- Transitions: implement `TransformTransitionProvider`, `AbstractColorTransitionProvider`, and `FloatTransitionProvider`
<!-- - RadialMenu: module prefab and logic -->
- CharacterController: `EnableCharacterMovement`, `EnableCharacterJump`, `GoToInteractState`, `GoToSetMovementState`, `EnableCameraRotation` and `EnableCameraZoom` methods in `ICharacterControllerSystem` interface and `CharacterControllerSystem` base class
<!-- - CharacterController: added AvatarPlacheholder prefab -->
- Utilities: Image downloader utility script
- Fade: added `FadeToBackground` and `FadeFromBackground` in `IFadeManager`, which use a `backgroundImage` property methods in `CanvasFadeManager` and a `backgroundColor` in `QuadFadeManager`
- VoiceChat: added `MuteLocalUser(bool muteAudio)` overload in `VoiceChatSystemBase`
- Utilities: added `ColorExtensions` extension class with `IsVerySimilarToWhite`, `NameToColor` and `HexToColor` methods
- Transitions: added `DoEnterExitTransitionAsync` method in abstract transition provider, which calls `DoTransitionAsync(true)` and `DoTransitionAsync(true)` in sequence
- Utilities: added `DateTimeExtensions` extension class with `ParseStringToDate` method
- Transitions: added `onEnterTransitionStart` callback and `onExitTransitionFinish` on transition providers
- VoiceChat: `GetVoiceDetection()` method
<!-- - CharacterController: added `TagReference` property in `CharacterBase` -->
- Utilities: added `StringExtensions` extension class with `CopyToClipboard`, `RemoveSpecialCharacters` and `ConcatenateStrings` method
- Avatars: added `IAvatarConfigController` reference of avatar instance in `IAvatarSystem`
- Avatars: added `AvatarId` and `AvatarPNG` properties in `IAvatarConfig`
- Utilities: added `TextAssetExtensions` extension class with `GetTextClassCompleteName` method
- Utilities: added `GetTextType` and `GetTypeFromString` methods
- Utilities: added `RecompileProjectEditorWindow` and `RestartProjectEditorWindow`
- Utilities: added `ResourceLockHandler<T>` utility script
- Utilities: added `DictionaryExtensions` extension class with `ToDictionary` method which converts an object to a dictionary and `CustomToString` method for debugging purposes
- CharacterController: add finger bones, references to interactor colliders and player height to `CharacterBase`
- CharacterController: Added logic to place label above the player
- Utilties: add `DrawIf` and `OnChangedCall` and `HelpBox` property drawers
- Utilities: added `IntExtensions` extensions class with `RoundUpDivision` method
- Utilities: added `totalCount` optional parameter in `ApiResponseArray` constructor
- Fade: Add fade in/out via material in `CanvasFadeManager`
- Utilities: add `AudioMixerExtension` extension class with `GetLinearFloat` and `DecibelToLinear` methods

### Deprecated

- Legacy Interaction module (working with old Toast System)

### Removed

- Synchronous scriptable actions working with legacy Interaction module

### Fixed

- Add missing dependencies in package.json
- Updated assembly files names
- Utilities: fixed private reference to `TextMeshPro` item in `TMProUGUIHyperlinks`;
<!-- - Utilities: added TryParse methods instead of Parse in ApiResponse -->
- Transitions: `CanvasTransitionProvider` now kills the active transition before executing the new one
- Fade: added `CanvasScaler` on fade canvas
- Avatars: added avatar existence checks on `AvatarConfigControllerDesktop` to prevent null reference exceptions
- Fade: fixed scale issue of the `CanvasFadeManagerDesktop`
- Avatars: added app execution check in `EnableAvatarMeshes` method of `AvatarControllerDesktop`
- Avatars: stored reference of current config in `AvatarConfigControllerBase`
- Avatars: fixed avatar animation issues
- Avatars: fixed layer mask issue
- Utilities: changed `WaitForSeconds` instruction to `WaitForSecondsRealtime` in `MonoBehaviourExtensions`, due to WebGL issues
- Transitions: improve `AnimationTransitionProvider` behaviour and added null checks in all transition providers
- Avatars: improved avatar activation/deactivation.
- CharacterController: improved character interaction activation/deactivation.
- Avatars: add check on avatar bounds for a better display of the label

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
