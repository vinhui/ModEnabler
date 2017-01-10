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
                int count;
                int pos = 0;
                while ((count = vorbisReader.ReadSamples(buffer, 0, buffer.Length)) > 0)
                {
                    clip.SetData(buffer, pos);
                    pos = (int)vorbisReader.DecodedPosition;
                }
            }

            return clip;
        }
    }
}