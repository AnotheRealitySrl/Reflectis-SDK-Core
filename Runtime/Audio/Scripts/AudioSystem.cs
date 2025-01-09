using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.Audio
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-Audio/AudioSystem", fileName = "AudioSystemConfig")]
    public class AudioSystem : BaseSystem
    {
        [SerializeField] AudioMixerGroup musicMixerGroup;

        [SerializeField] AudioMixerGroup voiceChatMixerGroup;

        public AudioMixerGroup MusicMixerGroup { get => musicMixerGroup; }
        public AudioMixerGroup VoiceChatMixerGroup { get => voiceChatMixerGroup; }

        public UnityEvent<float> OnVoiceChatVolumeChanged { get; } = new();

        #region Public Methods

        /// <summary>
        /// Function to set music and sound mixer volume.
        /// </summary>
        /// <param name="volume"></param>
        public void SetMusicSoundVolume(float volume)
        {
            if (volume < 1f) volume = 0.001f;

            var volumeVal = Mathf.Log10(volume / 100) * 20;

            MusicMixerGroup.audioMixer.SetFloat(MusicMixerGroup.name, volumeVal);
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

            VoiceChatMixerGroup.audioMixer.SetFloat(VoiceChatMixerGroup.name, volumeVal);

            OnVoiceChatVolumeChanged.Invoke(DecibelToLinear(volumeVal));
        }

        #endregion
    }
}
