using Doozy.Runtime.Reactor.Animators;
using GameField;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Player
{
    public class PlayerItemQueueUiController : MonoBehaviour
    {
        [SerializeField]
        private Image shotImage;
        [SerializeField]
        private TMP_Text shotsRemainText;
        [SerializeField]
        private UIAnimator uIAnimator;
        [Inject]
        private CoreSettings coreSettings;
        [Inject]
        private PlayerItemQueue playerItemQueue;

        private void Start()
        {
            UpdateUi();
            playerItemQueue.OnDataChange += UpdateUi;
        }

        private void UpdateUi()
        {
            shotImage.color = coreSettings.gameFieldSettings.GetItemColor(playerItemQueue.NextItemType);
            shotsRemainText.text = playerItemQueue.ItemsRemain.ToString("D");
            uIAnimator.Play();
        }
    }
}
