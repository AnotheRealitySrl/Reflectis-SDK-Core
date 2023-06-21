using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.Fade
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
        void FadeToBlack(System.Action onEnd = null);
        void FadeFromBlack(System.Action onEnd = null);
        void FadeToDesaturated(System.Action onEnd = null);
        void FadeFromDesaturated(System.Action onEnd = null);
        void InterruptFade();
    }
}