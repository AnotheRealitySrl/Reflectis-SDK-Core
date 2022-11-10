using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.CharacterController
{
    public interface ICharacterController : ICharacter
    {
        #region Enums

        public enum InteractionType
        {
            Controllers,
            Hands
        }

        #endregion

        #region Interface properties

        Camera Camera { get; }
        InteractionType InteractorsType { get; }

        #endregion
    }
}
