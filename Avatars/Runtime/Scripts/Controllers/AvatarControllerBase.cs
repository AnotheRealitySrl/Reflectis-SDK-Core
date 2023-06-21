using SPACS.SDK.CharacterController;
using SPACS.SDK.Transitions;

using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.Avatars
{
    public class AvatarControllerBase : MonoBehaviour, IAvatarController
    {
        #region Inspector variables

        [SerializeField] protected CharacterBase sourceCharacter;

        #endregion

        #region Private variables

        private AbstractTransitionProvider transitionProvider;

        #endregion

        #region Properties

        // This character
        public CharacterBase SourceCharacter { get => sourceCharacter; private set => sourceCharacter = value; }

        // The character controller which this character is attached to.
        public CharacterControllerBase SourceController { get; private set; }

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            if (!sourceCharacter)
            {
                sourceCharacter = GetComponent<CharacterBase>();
            }

            transitionProvider = GetComponent<AbstractTransitionProvider>();
        }

        #endregion

        #region Public API

        public virtual async Task Setup(CharacterControllerBase source)
        {
            // If the source controller is already instantiated, use that.
            // Otherwise, instantiate the source controller.
            SourceController = string.IsNullOrEmpty(source.gameObject.scene.name)
                ? Instantiate(source.gameObject).GetComponent<AvatarControllerBase>().SourceController
                : source;
            await DoTransition(true);
        }

        public async Task Unsetup()
        {
            await DoTransition(false);
            Destroy(gameObject);
            SourceController = null;
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
