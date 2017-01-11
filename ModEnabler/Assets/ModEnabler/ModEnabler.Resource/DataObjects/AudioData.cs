using NVorbis;
using System;
using System.IO;
using UnityEngine;

namespace ModEnabler.Resource.DataObjects
{
    [Serializable]
    public static class AudioData
    {
        public static AudioClip ToUnity(byte[] bytes)
        {
            AudioClip clip;

            using (MemoryStream memStream = new MemoryStream(bytes))
            using (VorbisReader vorbisReader = new VorbisReader(memStream, false))
            {
                clip = AudioClip.Create("ModdedAudio", (int)vorbisReader.TotalSamples, vorbisReader.Channels, vorbisReader.SampleRate, false);

                var buffer = new float[1024 * 8];
                int pos = 0;
                while (vorbisReader.ReadSamples(buffer, 0, buffer.Length) > 0)
                {
                    clip.SetData(buffer, pos);
                    pos = (int)vorbisReader.DecodedPosition;
                }
            }

            return clip;
        }

        public static AudioClip ToUnityProgressive(byte[] bytes)
        {
            AsyncAudioLoader loader = AsyncAudioLoader.Create(bytes);
            return loader.clip;
        }

        private class AsyncAudioLoader : MonoBehaviour
        {
            public AudioClip clip { get; private set; }

            private MemoryStream memStream;
            private VorbisReader vorbisReader;

            private float[] buffer = new float[1024 * 8];
            private int readPos = 0;

            public static AsyncAudioLoader Create(byte[] bytes)
            {
                GameObject go = new GameObject("AsyncAudioLoader");
                go.hideFlags = HideFlags.HideAndDontSave;
                AsyncAudioLoader audioLoader = go.AddComponent<AsyncAudioLoader>();
                audioLoader.Init(bytes);
                return audioLoader;
            }

            private void Init(byte[] bytes)
            {
                memStream = new MemoryStream(bytes);
                vorbisReader = new VorbisReader(memStream, true);

                clip = AudioClip.Create("ModdedAudio", (int)vorbisReader.TotalSamples, vorbisReader.Channels, vorbisReader.SampleRate, false);
                LoadChunk();
            }

            private void Update()
            {
                LoadChunk();
            }

            private void LoadChunk()
            {
                if (vorbisReader.ReadSamples(buffer, 0, buffer.Length) > 0)
                {
                    clip.SetData(buffer, readPos);
                    readPos = (int)vorbisReader.DecodedPosition;
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            private void OnDestroy()
            {
                if (vorbisReader != null)
                    vorbisReader.Dispose();
                if (memStream != null)
                    memStream.Dispose();
            }
        }
    }
}