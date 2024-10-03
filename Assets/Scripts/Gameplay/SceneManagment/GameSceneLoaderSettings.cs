using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "GameSceneLoaderSettings", menuName = "Settings/GameSceneLoaderSettings")]
public class GameSceneLoaderSettings : ScriptableObject
{
    [SerializeField]
    public List<AddressablesSceneSettings> addresablesSceneSettingsList = new();
    [SerializeField]
    public List<AddressablesLevelSettings> sddressablesLevelSettingsList = new();

    [Serializable]
    public class AddressablesSceneSettings
    {
        public string sceneName;
        public AssetReference scene;
    }

    [Serializable]
    public class AddressablesLevelSettings
    {
        public AssetReference levelSettings;
    }
}
