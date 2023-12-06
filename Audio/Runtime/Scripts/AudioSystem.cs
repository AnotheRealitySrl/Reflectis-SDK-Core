using Reflectis.SDK.Core;
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

        }

        #region Public Methods

        /// <summary>
        /// Function to set music and sound mixer volume.
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusicSoundVolume(float volume)
        {
            if (volume < 1f) volume = 0.001f;

            var volumeVal = Mathf.Log10(volume / 100) * 20;
            mixer.SetFloat("Music&Sound", volumeVal);
        }

        public float DecibelToLinear(float dB)
        {
            float linear = Mathf.Pow(10.0f, dB / 20.0f);

            return linear;
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
