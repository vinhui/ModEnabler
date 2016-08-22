using NVorbis;
using System;
using System.IO;
using UnityEngine;

namespace ModEnabler.Resource.DataObjects
{
    [Serializable]
    public struct AudioData
    {
        private const int intByteLength = 4;

        public int channels;
        public int frequency;
        public float[] samples;

        public AudioData(AudioClip clip)
        {
            channels = clip.channels;
            frequency = clip.frequency;
            samples = new float[clip.samples * channels];
            clip.GetData(samples, 0);
        }

        public AudioData(byte[] bytes)
        {
            using (MemoryStream memStream = new MemoryStream(bytes))
            using (VorbisReader vorbisReader = new VorbisReader(memStream, false))
            {
                channels = vorbisReader.Channels;
                frequency = vorbisReader.SampleRate;
                samples = new float[vorbisReader.TotalSamples];
                vorbisReader.ReadSamples(samples, 0, (int)vorbisReader.TotalSamples);
            }
        }

        public AudioClip ToUnity()
        {
            AudioClip clip = AudioClip.Create("ModdedAudio", samples.Length, channels, frequency, false);
            clip.SetData(samples, 0);
            return clip;
        }
    }
}