using Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameField
{
    [CreateAssetMenu(fileName = "GameFieldSettings", menuName = "Settings/GameFieldSettings")]
    public class GameFieldSettings : ScriptableObject
    {
        public Vector3 startPosition = new Vector3(-3f, 4f);
        public Vector2 itemOffsets = new Vector2(1f, 1f);
        public int topItemsToVictory = 3;
        public List<ItemDescriptor> itemDescriptors = new();

        [Serializable]
        public class ItemDescriptor
        {
            public ItemType itemType;
            public Color color = Color.white;
        }

        public Color GetItemColor(ItemType itemType)
        {
            foreach (var descriptor in itemDescriptors)
            {
                if (descriptor.itemType == itemType)
                {
                    return descriptor.color;
                }
            }

            return Color.white;
        }
    }
}
