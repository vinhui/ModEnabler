using UnityEngine;

namespace ModEnabler.Resource.DataObjects
{
    public struct TextureSheetData
    {
        public FrameData[] frames;
        public MetaData meta;

        public struct FrameData
        {
            public string filename;
            public bool rotated, trimmed;
            public Vector2 pivot;
            public Size sourceSize;
            public Rect frame;
            public Rect spriteSourceSize;
        }

        public struct MetaData
        {
            public string app, version, image, format, scale, smartupdate;
            public Size size;
        }

        public struct Size
        {
            public int w, h;
        }

        public struct Rect
        {
            public float x, y, w, h;

            public UnityEngine.Rect ToUnity(int imageHeight)
            {
                return new UnityEngine.Rect(x, imageHeight - h, w, h);
            }
        }
    }
}