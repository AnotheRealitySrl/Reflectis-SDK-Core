using System;

namespace Reflectis.SDK.Core.Interaction
{
    [Flags]
    public enum EManipulationInput
    {
        None = 0,
        LeftRayInteraction = 1,
        RightRayInteraction = 2,
        LeftHand = 4,
        RightHand = 8,
        Mouse = 16,
    }

}
