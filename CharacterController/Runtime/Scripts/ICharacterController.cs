using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SPACS.SDK.CharacterController
{
    /// <summary>
    /// Represents a character controller, i.e. an character that is controlled by some other entity (spoiler: the user, most of the times)
    /// </summary>
    public interface ICharacterController : ICharacter
    {
        #region Enums

        public enum EInteractionType
        {
            Controllers,
            Hands
        }

        #endregion

        #region Interface properties

        /// <summary>
        /// Reference to the main camera
        /// </summary>
        Camera Camera { get; }

        /// <summary>
        /// Controllers/Hands
        /// </summary>
        EInteractionType InteractorsType { get; }

        #endregion
    }
}
