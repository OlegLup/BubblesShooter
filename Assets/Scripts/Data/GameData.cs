using UnityEngine;

namespace Data
{
    public class GameData
    {
        [SerializeField]
        public SettingsData settingsData = new();
        [SerializeField]
        public ProgressData progressData = new();
    }
}
