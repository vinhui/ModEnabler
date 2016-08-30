using System;
using UnityEngine;

namespace ModEnabler.Resource.DataObjects
{
    public struct AnimationClipData
    {
        public string name;
        public bool legacy;
        public Bounds localBounds;
        public WrapMode wrapMode;
        public ClipCurveData[] curves;

        public AnimationClip ToUnity()
        {
            AnimationClip clip = new AnimationClip();
            clip.name = name;
            clip.legacy = legacy;
            clip.localBounds = localBounds;
            clip.wrapMode = wrapMode;

            for (int i = 0; i < curves.Length; i++)
            {
                clip.SetCurve(
                    curves[i].relativePath,
                    Type.GetType(curves[i].type),
                    curves[i].propertyName,
                    curves[i].curve.ToUnity());
            }
            return clip;
        }

        public struct ClipCurveData
        {
            public string relativePath;
            public string type;
            public string propertyName;
            public AnimationCurveData curve;
        }

        public struct AnimationCurveData
        {
            public WrapMode postWrapMode;
            public WrapMode preWrapMode;
            public Keyframe[] keys;

            public AnimationCurveData(AnimationCurve curve)
            {
                postWrapMode = curve.postWrapMode;
                preWrapMode = curve.preWrapMode;
                keys = curve.keys;
            }

            public AnimationCurve ToUnity()
            {
                return new AnimationCurve()
                {
                    postWrapMode = postWrapMode,
                    preWrapMode = preWrapMode,
                    keys = keys
                };
            }
        }
    }
}