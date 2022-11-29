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

        public CharacterControllerBase SourceController { get; private set; }
        public CharacterBase SourceCharacter { get => sourceCharacter; private set => sourceCharacter = value; }

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
