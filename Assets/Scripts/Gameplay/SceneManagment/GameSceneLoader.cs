using Cysharp.Threading.Tasks;
using Data;
using Levels;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using VContainer;

public class GameSceneLoader : MonoBehaviour
{
    [SerializeField]
    private GameSceneLoaderSettings settings;

    [Inject]
    private GameData gameData;
    [Inject]
    private RootSettings rootSettings;

    private AsyncOperationHandle<LevelSettings> levelHandler;

    void Awake()
    {
        SwitchScene("MainMenu", LoadSceneMode.Additive);
    }

    public async void SwitchScene(string sceneLoad, LoadSceneMode mode, string sceneUnload = "")
    {
        await UniTask.Delay(300);

        if (!string.IsNullOrEmpty(sceneUnload))
        {
            await UnloadSceneTask(sceneUnload);
        }

        if (sceneLoad == "CoreGameplay")
        {
            await UpdateLevelSettings();
        }

        await LoadSceneTask(sceneLoad, mode);
    }

    private async UniTask LoadSceneTask(string sceneName, LoadSceneMode mode)
    {
        var sceneSettings = settings.addresablesSceneSettingsList.Find(s => s.sceneName == sceneName);

        if (sceneSettings == null) return;

        await Addressables.LoadSceneAsync(sceneSettings.scene, mode).Task;
    }

    private async UniTask UnloadSceneTask(string sceneName)
    {
        var sceneSettings = settings.addresablesSceneSettingsList.Find(s => s.sceneName == sceneName);

        if (sceneSettings == null) return;

        if (!SceneManager.GetSceneByName(sceneName).IsValid()) return;

        await SceneManager.UnloadSceneAsync(sceneName);
    }

    private async UniTask UpdateLevelSettings()
    {
        if (levelHandler.IsValid())
        {
            Addressables.Release(levelHandler);
        }

        if (gameData.progressData.levelIndex >= settings.sddressablesLevelSettingsList.Count)
        {
            gameData.progressData.levelIndex = 0;
        }

        levelHandler = Addressables.LoadAssetAsync<LevelSettings>(settings.sddressablesLevelSettingsList[gameData.progressData.levelIndex].levelSettings);

        await levelHandler.Task;

        rootSettings.levelSettings = levelHandler.Result;
    }

    private void OnDestroy()
    {
        if (levelHandler.IsValid())
        {
            Addressables.Release(levelHandler);
        }
    }
}
