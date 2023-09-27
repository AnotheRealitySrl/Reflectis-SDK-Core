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
        [SerializeField] bool haveToSaveSettings = true;

        public AudioMixer Mixer => mixer;

        public bool HaveToSaveSettings => haveToSaveSettings;

        public override void Init()
        {
            
        }

        #region Public Methods

        /// <summary>
        /// Function to set music and sound mixer volume.
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusicSoundVolume(float volume)
        {
            if(volume < 1f) volume = 0.001f;

            var volumeVal = Mathf.Log10(volume / 100) * 20;
            mixer.SetFloat("Music&Sound", volumeVal);
        }

        /// <summary>
        /// Function to set voice chat mixer volume.
        /// </summary>
        /// <param name="volume"></param>
        public void SetVoiceChatVolume(float volume)
        {
            if (volume < 1f) volume = 0.001f;

            var volumeVal = Mathf.Log10(volume / 100) * 20;
            mixer.SetFloat("VoiceChat", volumeVal);
        }

        #endregion
    }
}
