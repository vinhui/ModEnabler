using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(AudioSource))]
    [HelpURL("http://modenabler.greenzonegames.com/wiki/resources.audio.html")]
    public class LoadAudioClipResource : LoadResourceComponent<AudioSource>
    {
        public bool loadProgressively = true;

        public override void Set()
        {
            AudioClip c;
            if (loadProgressively)
                c = ResourceManager.LoadAudioClip(fileName);
            else
                c = ResourceManager.LoadAudioClipInstant(fileName);

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