# Release notes

## v6.0.0

### Changed

- ClientModels: added `worldId` int parameter to `GetDefaultWorldEvent` signature.
- ClientModels: change `GetPlayerData` method name to `GetMyUserData` in `IClientModelSystem`.

### Added

- Add new `SceneHierarchy` module for better scene objects grouping and management.
- Avatars: added `Finish` method in `AvatarSystem` to unload the current avatar.
- Avatars: added `EnableSpeaker` value in `EFacetGroup` enum of `CMFacet`.
- Avatars: added `VoiceChatSource` in `AvatarControllerBase`.
- Avatars: added `AvatarConfigBase` class.
- Avatars: implemented height management for RPM avatars, added `CalibrateAvatar` method in `ICharacter` and `IAvatarConfigController`.
- ClientModels: added `GetWorld` method in `IClientModelSystem`.
- ClientModels: added `Description` and `ThumbnailUri` field to `CMWorld`.
- ClientModels: added `UserPrefs` to `IClientModelSystem`.
- ClientModels: added NickName property in CMUser in order to differentiate the name of a User from its DisplayName.
- ClientModels: added `GetUserCode` method in `IClientModelSystem`.
- ClientModels: added `Height` property in `CMUserPreference`.
- ClientModels: added `LocalizationName` and `LocalizationCSV` to `CMEnvironment`.
- ClientModels: Added `UserPrefs` property in `IClientModelSystem`.
- Interaction: added `OnGrabManipulableStart`, `OnGrabManipulableEnd`, `OnRayGrabManipulableStart` and `OnRayGrabManipulableEnd` events in `Manipulable`.

### Removed

- ClientModels: Remove `DefaultEvent` property from `IClientModelSystem`.

### Fixed

- Update Addressables package dependency to version 1.21.20.
- Fade: fixed set float method in `CanvasFadeManager` which prevented the fade to be completed.
- Avatars: fixed issue of avatar not being attached to character controller on setup, in the case the avatar is setupped automatically.
- Interaction: fixed search of disabled `InteractableRef`s in `InteractableBehaviourBase`.
- Interaction: added a minimum treshold under which a `Manipulable` can be moved in WebGL.
- Avatars: added `EnableAvatarInstanceLabel` in `IAvatarSystem` (with implementation in `AvatarSystem`) and `EnableAvatarLabel` in `IAvatarConfigController` (with implementation in `AvatarControllerBase` and `AvatarControllerDesktop`).
- Interaction: Fixed networked contextual menu option and removed hard reference to bounding box collider renamed scene hierarchy to scene objects

## v5.0.0

### Changed

- Avatars: added references to network avatar prefabs.
- ClientModels: changed saved color in `CMTemplateObj` from Color to string, to allow for an empty/null color value.
- ColorPicker: renamed method used to load saved model color state, from `AssignColorToPicker` to `AssignSavedColorToPicker`.
- Core: systems initialization is now awaitable.
- Interaction: `GenericInteractable` now accepts script machines instead of scriptable actions to define custom logic.
- Interaction: `ContextualMenuManageable`'s dictionary with button ids as keys now has `UnityAction`s as values instead of `UnityEvent`s.
- ModelExploder: renamed method used to load saved model explosion state, from `AssignExplosionToModelExploder` to `AssignSavedExplosionToModelExploder`.
- Utilities: renamed `ReflectionExtensions` to `ReflectionUtilities`.

### Added

- Added new Popup module with `IPopupSystem` interface.
- ClientModels: added `Catalog` property to `CMEnvironment`.
- ClientModels: added `Email` property to `CMUser`.
- ClientModels: added `WorldId` property to `CMEvent`.
- ClientModels: added `CMCatalog` entity and `GetWorldCatalogs(int worldId)` in `IClientModelSystem`.
- ColorPicker: added optional boolean parameter `networkedContext` to `AssignColorPicker`, to allow for offline/networked color changer component setup.
- Interaction: added new visual scripting nodes - generic interactable event nodes and expose node.
- Interaction: implement logic of object destroying on `ContextualMenuManageable`.
- Interaction: added new public property `IsSubMesh` in `Manipulable`, to check if the `Manipulable` is applied to a submesh.
- Interaction: added new public property `RootManipulable` in `Manipulable`, which returns the component at the root of the interactable object, whether the `Manipulable` reference is on the root of the interactable object or on one of its submeshes.
- ModelExploder: added optional boolean parameter `networkedContext` to `AssignModelExploder`, to allow for offline/networked model exploder component setup.
- Utilities: added new `IApplicationManager` interface that defines application management methods (`QuitApplication`, `ErasePlayerSessionData`, `HideEverithing`).
- Utilities: added new `NetworkUtilities` which contains static utilitiy methods such as `CheckUserInternetConnection`.
- Utilities: added `GetFieldsRecurse`, `GetPropertiesRecurse` and `GetMethodRecurse` in `ReflectionUtilities`.

### Removed

- ClientModels: removed `CMEnvironmentNameComparer` in `CMEnvironment`.
- Interaction: removed awaitable scriptable actions.
- Interaction: removed legacy `IDesktopInteractionSystem` interface.

### Fixed

- Interaction: fixed `Manipulable`'s `BoundingBox` initialization.
- Interaction: updated `ObjectCenter` and `ObjectSize` properties of `Manipulable` to be more coherent in checking if the `Manipulable` is on a submesh or not.

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
- New ModelExploder module with base logic for exploding a 3D asset.
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
