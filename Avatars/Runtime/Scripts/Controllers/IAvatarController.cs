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

        public CharacterControllerBase SourceController { get; }

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
