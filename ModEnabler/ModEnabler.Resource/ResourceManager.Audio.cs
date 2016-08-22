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
        public static AudioClip LoadAudioClip(string name)
        {
            return GetResource(name, ModsManager.settings.audioDirectory, (bytes) =>
            {
                return new AudioData(bytes).ToUnity();
            }, "Audio Clip");
        }
    }
}