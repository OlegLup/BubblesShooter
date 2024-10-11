using Cysharp.Threading.Tasks;
using Data;
using Doozy.Runtime.UIManager.Components;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;
using VContainer;

public class SettingsWindowUiController : MonoBehaviour
{
    [SerializeField]
    private UIToggle soundToggle;
    [SerializeField]
    private UIToggleGroup languagesToggleGroup;
    [Inject]
    private GameData gameData;
    [Inject]
    private MMSoundManager soundManager;

    void Start()
    {
        LocalizationInitTask();

        soundToggle.isOn = !gameData.settingsData.sounds;
        ApplySounds(gameData.settingsData.sounds);
        soundToggle.OnValueChangedCallback.AddListener((s) => ApplySounds(!s));
    }

    private async void LocalizationInitTask()
    {
        await LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[gameData.languageIndex];
        var toggles = languagesToggleGroup.toggles.OrderBy(t => t.transform.GetSiblingIndex()).ToList();
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].isOn = (i == gameData.languageIndex ? true : false);
        }
        languagesToggleGroup.OnToggleTriggeredCallback.AddListener(t => ApplyLanguage());
    }

    private void ApplyLanguage()
    {
        var toggles = languagesToggleGroup.toggles.OrderBy(t => t.transform.GetSiblingIndex()).ToList();
        gameData.languageIndex = toggles.FindIndex(t => t.isOn);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[gameData.languageIndex];
    }

    private void ApplySounds(bool state)
    {
        gameData.settingsData.sounds = state;
        soundManager.SetVolumeMaster(state ? 1f : 0f);
    }
}
