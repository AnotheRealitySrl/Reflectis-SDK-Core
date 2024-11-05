using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Transitions;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Avatars
{
    /// <summary>
    /// Base implementation of an <see cref="IAvatarController">. 
    /// Provides <see cref="Setup"> and <see cref="Unsetup"> virtual methods, 
    /// specific hooking into another <see cref="CharacterControllerBase"/> must be implemented.
    /// </summary>
    public class AvatarControllerBase : MonoBehaviour, IAvatarController
    {
        #region Inspector variables

        [SerializeField, Tooltip("\"This\" character, i.e. the character (avatar) that is associated to this avatar controller")]
        protected CharacterBase characterReference;
        #endregion

        #region Private variables

        private AbstractTransitionProvider transitionProvider;

        #endregion

        #region Properties

        public CharacterBase CharacterReference { get => characterReference; private set => characterReference = value; }
        public CharacterControllerBase SourceCharacterController { get; private set; }

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            if (!characterReference)
            {
                characterReference = GetComponent<CharacterBase>();
            }

            transitionProvider = GetComponent<AbstractTransitionProvider>();
        }

        #endregion

        #region Public API

        public virtual async Task Setup(CharacterControllerBase sourceController)
        {
            SourceCharacterController = sourceController;

            await DoTransition(true);
        }

        public virtual async Task Unsetup()
        {
            await DoTransition(false);

            Destroy(gameObject);
            SourceCharacterController = null;
        }

        #endregion

        #region Private methods

        private async Task DoTransition(bool show)
        {
            if (transitionProvider != null)
            {
                await transitionProvider.DoTransitionAsync(show);
            }
            else
            {
                gameObject.SetActive(show);
            }
        }

        #endregion
    }
}
