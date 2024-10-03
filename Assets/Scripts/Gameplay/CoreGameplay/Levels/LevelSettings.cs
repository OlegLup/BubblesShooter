using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelSettings_0", menuName = "Settings/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        [InfoBox("Go to Window -> UIToolkit -> LevelSettingsEditor", InfoMessageType.Warning)]
        [ReadOnly]
        public int shots = 5;
        [ReadOnly]
        [SerializeField]
        public ItemsRow[] rows = new ItemsRow[7];

        [Serializable]
        public class ItemsRow
        {
            [ReadOnly]
            [SerializeField]
            public ItemType[] items = new ItemType[7];
        }
    }
}
