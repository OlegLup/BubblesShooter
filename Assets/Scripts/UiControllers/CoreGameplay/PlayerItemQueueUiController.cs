using Doozy.Runtime.Reactor.Animators;
using GameField;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
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
            UpdateUi(playerItemQueue.ItemsRemain.Value);
            playerItemQueue.ItemsRemain.Subscribe(UpdateUi);
        }

        private void UpdateUi(int itemsRemain)
        {
            shotImage.color = coreSettings.gameFieldSettings.GetItemColor(playerItemQueue.NextItemType);
            shotsRemainText.text = itemsRemain.ToString("D");
            uIAnimator.Play();
        }
    }
}
