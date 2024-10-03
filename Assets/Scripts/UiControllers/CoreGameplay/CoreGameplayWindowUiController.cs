using Doozy.Runtime.Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Rules
{
    public class CoreGameplayWindowUiController : MonoBehaviour
    {
        [Inject]
        private GameSceneLoader gameSceneLoader;
        [Inject]
        private CoreController coreRules;
        private SignalReceiver signalReceiver1 = new();
        private SignalReceiver signalReceiver2 = new();
        private SignalReceiver signalReceiver3 = new();

        private void OnEnable()
        {
            signalReceiver1.SetOnSignalCallback(OnSwitchToMainMenuSignal);
            SignalStream.Get("Common", "SwitchToMainMenu").ConnectReceiver(signalReceiver1);
            signalReceiver2.SetOnSignalCallback(OnRestartLevelSignal);
            SignalStream.Get("Common", "RestartLevel").ConnectReceiver(signalReceiver2);
            signalReceiver3.SetOnSignalCallback(OnNextLevelSignal);
            SignalStream.Get("Common", "NextLevel").ConnectReceiver(signalReceiver3);
        }

        private void Start()
        {
            coreRules.OnDefeat += OnDefeat;
            coreRules.OnVictory += OnVictory;

            Signal.Send("Common", "Core");
        }

        private void OnDisable()
        {
            SignalStream.Get("Common", "SwitchToMainMenu").DisconnectReceiver(signalReceiver1);
            SignalStream.Get("Common", "RestartLevel").DisconnectReceiver(signalReceiver2);
            SignalStream.Get("Common", "NextLevel").DisconnectReceiver(signalReceiver3);
        }

        private void OnSwitchToMainMenuSignal(Signal signal)
        {
            Signal.Send("Common", "Loading");
            gameSceneLoader.SwitchScene("MainMenu", LoadSceneMode.Additive, "CoreGameplay");
        }

        private void OnRestartLevelSignal(Signal signal)
        {
            Signal.Send("Common", "Loading");
            gameSceneLoader.SwitchScene("CoreGameplay", LoadSceneMode.Additive, "CoreGameplay");
        }

        private void OnNextLevelSignal(Signal signal)
        {
            Signal.Send("Common", "Loading");
            gameSceneLoader.SwitchScene("CoreGameplay", LoadSceneMode.Additive, "CoreGameplay");
        }

        private void OnDefeat()
        {
            Signal.Send("Common", "Defeat");
        }

        private void OnVictory()
        {
            Signal.Send("Common", "Victory");
        }
    }
}
