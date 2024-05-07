using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.CharacterController
{
    /// <summary>
    /// Represents the structure of a character. A character rig has some transform that are useful for its manipulation
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        /// The pivot of the character
        /// </summary>
        Transform PivotReference { get; }

        /// <summary>
        /// The head of the character
        /// </summary>
        Transform HeadReference { get; }

        /// <summary>
        /// The left intereactor of the character (suitable mostly for VR)
        /// </summary>
        Transform LeftInteractorReference { get; }

        /// <summary>
        /// The right intereactor of the character (suitable mostly for VR)
        /// </summary>
        Transform RightInteractorReference { get; }

        /// <summary>
        /// If the character has a reference to a label with its information
        /// </summary>
        Transform LabelReference { get; }
        float LabelOffsetFromBounds { get; }

        /// <summary>
        /// Setups a character controller
        /// </summary>
        /// <returns>Task</returns>
        Task Setup();

        /// <summary>
        /// Unsetups a character controller
        /// </summary>
        /// <returns>Task</returns>
        Task Unsetup();

        /// <summary>
        /// Calibrates the avatar height based on player height
        /// </summary>
        void CalibrateAvatar();
    }
}