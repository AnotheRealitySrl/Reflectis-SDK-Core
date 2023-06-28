# SPACS-SDK-CharacterController

This module contains the interfaces and base classes needed for building the interaction with a character controller, 
i.e. a character that is usually controlled by the input of the user.

## How to use the Character Controller System

- Put the system in the `SystemsManagerController`.
- Set the reference of a `CharacterControllerBase` prefab 
  (or mark the flag `CharacterControllerAlreadyInScene` if the character controller is already instantiated in scene).
- If `CreateCharacterControllerInstance` flag is turned off, the system does not create the character controller instance automatically.
