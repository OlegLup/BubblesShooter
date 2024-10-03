using GameField;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        private GameFieldSettings levelSettings;

        [SerializeField]
        private UnityEvent onPlacedFx;
        [SerializeField]
        private UnityEvent onRemovedFx;
        [SerializeField]
        private UnityEvent onFreeFallFx;
        [SerializeField]
        private UnityEvent<Vector2> onShakeFx;

        public ItemType ItemType { get; private set; }

        public void Init(ItemType itemType, GameFieldSettings levelSettings)
        {
            this.levelSettings = levelSettings;
            SetType(itemType);
        }

        public void SetType(ItemType itemType)
        {
            ItemType = itemType;
            spriteRenderer.color = levelSettings.GetItemColor(ItemType);
        }

        public Color GetColor()
        {
            return spriteRenderer.color;
        }

        public void Placed()
        {
            onPlacedFx?.Invoke();
        }

        public void Removed()
        {
            onRemovedFx?.Invoke();
        }

        public void Shake(Vector2 origin)
        {
            onShakeFx?.Invoke(origin);
        }

        public void FreeFall()
        {
            onFreeFallFx?.Invoke();
        }
    }
}
