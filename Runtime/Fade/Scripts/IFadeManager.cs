using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.Fade
{
    public interface IFadeManager
    {
        // Config
        float FadeTime { get; set; }
        bool FadeOnStart { get; set; }

        // State
        ILayerManager LayerManager { get; set; }

        // Events
        UnityEvent OnFadeStart { get; }
        UnityEvent OnFadeEnd { get; }

        // Public API
        void Init();
        void FadeToBackground(System.Action onEnd = null);
        void FadeFromBackground(System.Action onEnd = null);
        void FadeToBlack(System.Action onEnd = null);
        void FadeFromBlack(System.Action onEnd = null);
        void FadeToDesaturated(System.Action onEnd = null);
        void FadeFromDesaturated(System.Action onEnd = null);
        void InterruptFade();
        void SetTargetCamera(Camera camera);
    }
}