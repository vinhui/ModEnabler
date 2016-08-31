using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(AudioSource))]
    [HelpURL("http://modenabler.greenzonegames.com/wiki/resources.audio.html")]
    public class LoadAudioClipResource : LoadResourceComponent<AudioSource>
    {
        public override void Set()
        {
            var c = ResourceManager.LoadAudioClip(fileName);
            if (c != null)
            {
                (componentToSet as AudioSource).clip = c;
                // I assume that when these things are true you want to play the file too
                if ((componentToSet as AudioSource).playOnAwake || (componentToSet as AudioSource).isPlaying)
                    (componentToSet as AudioSource).Play();
            }
        }
    }
}