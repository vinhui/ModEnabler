using System;
using UnityEngine;

namespace ModEnabler.Resource.DataObjects
{
    /// <summary>
    /// Properties of a physic materials
    /// </summary>
    [Serializable]
    public struct PhysicMaterialData
    {
        public string name;
        public PhysicMaterialCombine bounceCombine;
        public float bounciness;
        public float dynamicFriction;
        public PhysicMaterialCombine frictionCombine;
        public float staticFriction;

        public PhysicMaterialData(PhysicMaterial mat)
        {
            name = mat.name;
            bounceCombine = mat.bounceCombine;
            bounciness = mat.bounciness;
            dynamicFriction = mat.dynamicFriction;
            frictionCombine = mat.frictionCombine;
            staticFriction = mat.staticFriction;
        }

        public PhysicMaterial ToUnity()
        {
            return new PhysicMaterial(name)
            {
                bounceCombine = bounceCombine,
                bounciness = bounciness,
                dynamicFriction = dynamicFriction,
                frictionCombine = frictionCombine,
                staticFriction = staticFriction
            };
        }
    }
}