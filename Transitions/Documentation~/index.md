# SPACS-SDK-Transitions

Collection of scripts that help the visual transitions of the GameObjects they are attached to.

There are various types of `TransitionProvider`s:

- [AbstractTransitionProvider](../Runtime/Scripts/AbstractTransitionProvider.cs): Abstract class with definitions of methods performing transitions
- [NoTransitionProvider](../Runtime/Scripts/AbstractTransitionProvider.cs): Fake transition provider, it only activates/deactivates the referenced GameObject
- [AnimatorTransitionProvider](../Runtime/Scripts/AnimatorTransitionProvider.cs): Transition provider that operates on a bool parameter of an animator controller
- [MultipleAnimatorsTransitionProvider](../Runtime/Scripts/MultipleAnimatorsTransitionProvider.cs): Transition provider that operates on a multiple animators simultauneously
- [CanvasTransitionProvider](../Runtime/Scripts/CanvasTransitionProvider.cs): Transition provider that operates on the alpha value of a canvas group
