using SPACS.SDK.CharacterController;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPACS.SDK.Avatars
{
    /// <summary>
    /// Avatar controller interface
    /// </summary>
    public interface IAvatarController
    {
        #region Properties

        /// <summary>
        /// "This" character, i.e. the character (avatar) that is associated to this avatar controller
        /// </summary>
        public CharacterBase CharacterReference { get; }

        /// <summary>
        /// The character controller to which this avatar controller "hooks up"
        /// <summary>
        public CharacterControllerBase SourceCharacterController { get; }

        #endregion

        #region Public API

        /// <summary>
        /// Setups the avatar controller base given a source controller. 
        /// The source controller is the object which the avatar is attached to.
        /// </summary>
        /// <param name="source">The source controller wich the htis avatar controller is attached to.</param>
        /// <returns>Task</returns>
        Task Setup(CharacterControllerBase source);

        /// <summary>
        /// Unsetups the current character controller.
        /// </summary>
        /// <returns>Task</returns>
        Task Unsetup();

        #endregion
    }
}
