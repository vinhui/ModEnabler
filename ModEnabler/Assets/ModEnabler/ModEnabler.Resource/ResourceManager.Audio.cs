using ModEnabler.Resource.DataObjects;
using UnityEngine;

namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        /// <summary>
        /// Load an audio clip
        /// </summary>
        /// <param name="name">Full name of the audio clip</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static AudioClip LoadAudioClipInstant(string name)
        {
            return GetResource(name, ModsManager.settings.audioDirectory, (bytes) =>
            {
                return AudioData.ToUnity(bytes);
            }, "Audio Clip");
        }

        /// <summary>
        /// Load an audio clip
        /// This will load the audioclip progressively, so not all the audio data is available instantly
        /// </summary>
        /// <param name="name">Full name of the audio clip</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static AudioClip LoadAudioClip(string name)
        {
            return GetResource(name, ModsManager.settings.audioDirectory, (bytes) =>
            {
                return AudioData.ToUnityProgressive(bytes);
            }, "Audio Clip");
        }
    }
}