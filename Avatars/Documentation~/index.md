# SPACS-SDK-Avatars

The `AvatarSystem` is designed to handle the hooking of an avatar (full-body and half-body) to the character controller.

## How to use the Avatar System

- Put the system as a subsystem of Characte Controller System, since it needs the character controller to be already set up
- Set the reference of an `AvatarControllerBase` prefab 
  (or mark the flag `AvatarAlreadyInScene` if the avatar is already instantiated in scene).
- If `CreateAvatarInstance` flag is turned off, the system does not create the avatar instance automatically
- If `SetupAvatarInstanceAutomatically` is turned on, the method `Setup()` of the `ICharacter` of the avatar is called during initialization.
