using Data;
using Doozy.Runtime.UIManager.Components;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class SettingsWindowUiController : MonoBehaviour
{
    [SerializeField]
    private UIToggle soundToggle;
    [Inject]
    private GameData gameData;
    [Inject]
    private MMSoundManager soundManager;

    void Start()
    {
        soundToggle.isOn = !gameData.settingsData.sounds;
        ApplySounds(gameData.settingsData.sounds);
        soundToggle.OnValueChangedCallback.AddListener((s) => ApplySounds(!s));
    }

    private void ApplySounds(bool state)
    {
        gameData.settingsData.sounds = state;
        soundManager.SetVolumeMaster(state ? 1f : 0f);
    }
}
