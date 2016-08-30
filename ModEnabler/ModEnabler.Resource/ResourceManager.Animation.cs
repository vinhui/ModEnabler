using ModEnabler.Resource.DataObjects;
using UnityEngine;

namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        /// <summary>
        /// Load an animation clip
        /// </summary>
        /// <param name="name">Full name and path of the file</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static AnimationClip LoadAnimationClip(string name)
        {
            return GetResource(name, "", (bytes) =>
            {
                AnimationClipData animationData = ModsManager.serializer.Deserialize<AnimationClipData>(BytesToString(bytes));
                return animationData.ToUnity();
            }, "AnimationClip");
        }

        /// <summary>
        /// Load an animation curve
        /// </summary>
        /// <param name="name">Full name and path of the file</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static AnimationCurve LoadAnimationCurve(string name)
        {
            return GetResource(name, "", (bytes) =>
            {
                AnimationClipData.AnimationCurveData curveData = ModsManager.serializer.Deserialize<AnimationClipData.AnimationCurveData>(BytesToString(bytes));
                return curveData.ToUnity();
            }, "AnimationCurve");
        }
    }
}