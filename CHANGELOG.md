# Release notes

## Unreleased

### Changed:

- Merged Extensions module into Utilities one
- Renamed "FadeSystem" (asmdef, namespaces, etc.) in "Fade"

### Added:

- New Interaction module, with interactable objects base logic and scriptable actions from SPACS-Core, and with new IDesktopInteractionSystem interface
- New Object Spawner system
- Avatars: Spawn method in AvatarSystem's API
- Avatars: onBeforeAction and onAfterAction callbacks on avatar customization change
- Transitions: flag for performing reverse transitions in AbstractTransitionsProvider
- Communication system: Communication system base class
- Documentation

### Fixed:

- Fade: condition that checks when all scenes have been loaded/unloaded

---

## v2.0.0

### Changed:

- Migrate to SPACS system framework

### Added:

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
