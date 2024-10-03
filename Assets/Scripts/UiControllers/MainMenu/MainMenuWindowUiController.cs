using Doozy.Runtime.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace MainMenu
{
    public class MainMenuWindowUiController : MonoBehaviour
    {
        [Inject]
        private GameSceneLoader gameSceneLoader;
        private SignalReceiver signalReceiver = new();

        private void OnEnable()
        {
            signalReceiver.SetOnSignalCallback(OnSwitchToCoreSignal);
            SignalStream.Get("Common", "SwitchToCore").ConnectReceiver(signalReceiver);
        }

        void Start()
        {

            Signal.Send("Common", "MainMenu");
        }

        private void OnDisable()
        {
            SignalStream.Get("Common", "SwitchToCore").DisconnectReceiver(signalReceiver);
        }

        private void OnSwitchToCoreSignal(Signal signal)
        {
            Signal.Send("Common", "Loading");
            gameSceneLoader.SwitchScene("CoreGameplay", LoadSceneMode.Additive, "MainMenu");
        }
    }
}
