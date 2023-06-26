using SPACS.SDK.CharacterController;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPACS.SDK.Avatars
{
    /// <summary>
    /// An avatar controller handles the hook between a <see cref="CharacterBase"/> (e.g. an avatar), 
    /// and a <see cref="CharacterControllerBase"/> (usually, the character controller of the player).
    /// In other words, it attaches an avatar to a xr rig / character controller / etc, 
    /// so that the 
    /// </summary>
    public interface IAvatarController
    {
        #region Properties

        /// <summary>
        /// Reference to "this" character, i.e. the character (avatar) that is associated to this avatar controller
        /// </summary>
        CharacterBase CharacterReference { get; }

        /// <summary>
        /// The character controller to which this avatar controller "hooks up" during setup.
        /// <summary>
        CharacterControllerBase SourceCharacterController { get; }

        #endregion

        #region Public API

        /// <summary>
        /// Setups the referenced character given a source character controller. 
        /// The source character controller is the object which the character is hooked to.
        /// </summary>
        /// <param name="source">The source controller wich the avatar controller is hooked to.</param>
        /// <returns>Task</returns>
        Task Setup(CharacterControllerBase source);

        /// <summary>
        /// Detached the character from the current character controller
        /// </summary>
        /// <returns>Task</returns>
        Task Unsetup();

        #endregion
    }
}
