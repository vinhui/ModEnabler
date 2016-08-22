﻿using UnityEngine;
using UnityEngine.UI;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(Image))]
    internal class LoadImageResource : LoadResourceComponent<Image>
    {
        [SerializeField]
        private Rect spriteRect;

        [SerializeField]
        private Vector2 pivot = Vector2.zero;

        [SerializeField]
        private float pixelsPerUnit = 100f;

        [SerializeField]
        private uint extrude = 0;

        [SerializeField]
        private SpriteMeshType meshType = SpriteMeshType.Tight;

        [SerializeField]
        private Vector4 border;

        protected override void Set()
        {
            var s = ResourceManager.LoadSprite(fileName, spriteRect, pivot, pixelsPerUnit, extrude, meshType, border);
            if (s != null)
                (componentToSet as Image).sprite = s;
        }
    }
}