using Reflectis.SDK.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Reflectis.SDK.Audio
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-Audio/AudioSystem", fileName = "AudioSystemConfig")]
    public class AudioSystem : BaseSystem
    {
        [SerializeField] AudioMixer mixer;

        public AudioMixer Mixer => mixer;

        public override void Init()
        {
            //Call Player prefs for set at the init the old volume setting saved?
        }

        #region Public Methods
        /// <summary>
        /// Function to set music and sound mixer volume.
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusicSoundVolume(float volume)
        {
            if(volume < 1f) volume = 0.001f;

            //Use Player prefs for save the float of volume?
            mixer.SetFloat("Music&Sound", Mathf.Log10(volume / 100) * 20);
        }

        /// <summary>
        /// Function to set voice chat mixer volume.
        /// </summary>
        /// <param name="volume"></param>
        public void SetVoiceChatVolume(float volume)
        {
            if (volume < 1f) volume = 0.001f;

            //Use Player prefs for save the float of volume?
            mixer.SetFloat("VoiceChat", Mathf.Log10(volume / 100) * 20);
        }

        #endregion
    }
}
