using SPACS.SDK.CharacterController;
using SPACS.SDK.Transitions;

using System.Threading.Tasks;

namespace SPACS.SDK.Avatars
{
    public class AvatarControllerBase : CharacterControllerBase
    {
        #region Private variables

        private AbstractTransitionProvider transitionProvider;

        #endregion

        #region Properties

        protected CharacterControllerBase SourceController { get; private set; }

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            transitionProvider = GetComponent<AbstractTransitionProvider>();
        }

        #endregion

        #region Public API

        public async override Task Setup(CharacterControllerBase source)
        {
            SourceController = string.IsNullOrEmpty(source.gameObject.scene.name)
                ? Instantiate(source.gameObject).GetComponent<CharacterControllerBase>()
                : source;
            await DoTransition(true);
        }

        public async override Task Unsetup()
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
