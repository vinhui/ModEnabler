using ModEnabler.Resource.DataObjects;
using UnityEditor;
using UnityEngine;

namespace ModEnabler.Editor.Utils
{
    public static class AnimationUtils
    {
        public static AnimationClipData ExportAnimationClip(AnimationClip clip)
        {
            AnimationClipData animData = new AnimationClipData();
            animData.name = clip.name;
            animData.legacy = clip.legacy;
            animData.localBounds = clip.localBounds;
            animData.wrapMode = clip.wrapMode;

            EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings(clip);
            animData.curves = new AnimationClipData.ClipCurveData[bindings.Length];

            for (int i = 0; i < bindings.Length; i++)
            {
                AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, bindings[i]);
                animData.curves[i] = new AnimationClipData.ClipCurveData()
                {
                    relativePath = bindings[i].path,
                    propertyName = bindings[i].propertyName,
                    type = bindings[i].type.FullName + ", " + bindings[i].type.Assembly.FullName,
                    curve = new AnimationClipData.AnimationCurveData(curve)
                };
            }

            return animData;
        }
    }
}