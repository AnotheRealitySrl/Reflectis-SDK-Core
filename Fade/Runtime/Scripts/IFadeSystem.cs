using DG.Tweening.Core.Easing;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.SDK.Fade
{
    /// <summary>
    /// The Fade System manages fade in and fade out of what camera sees.
    /// Optionally, is possible to filter out some objects from fade.
    /// </summary>
    public interface IFadeSystem 
    {
        /// <summary>
        /// Fades to black
        /// </summary>
        /// <param name="onEnd">Executed after fade is completed</param>
        public void FadeToBlack(Action onEnd = null);

        /// <summary>
        /// Fades from black
        /// </summary>
        /// <param name="onEnd">Executed after fade is completed</param>
        public void FadeFromBlack(Action onEnd = null);

        /// <summary>
        /// Fades to desaturated. 
        /// </summary>
        /// <param name="onEnd">Executed after fade is completed</param>
        public void FadeToDesaturated(Action onEnd = null);

        /// <summary>
        /// Fades from desaturated
        /// </summary>
        /// <param name="onEnd">Executed after fade is completed</param>
        public void FadeFromDesaturated(Action onEnd = null);

        /// <summary>
        /// Adds to the current Fade Manager a list of objects that should be not affected by fade. 
        /// </summary>
        /// <param name="objsUnaffectedByFade">The list of GameObjects unaffedcted by fade</param>
        public void UpdateObjsUnaffectedByFade(List<GameObject> objsUnaffectedByFade);

        /// <summary>
        /// Resets the list of objects unaffected by fade of the current Fade Manager
        /// </summary>        
        public void ResetObjsUnaffectedByFade();

        /// <summary>
        /// Interrupts the fade.
        /// </summary>
        public void InterruptFade();
    }
}
