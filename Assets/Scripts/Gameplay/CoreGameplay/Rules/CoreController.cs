using Data;
using GameField;
using Player;
using System;
using System.Threading.Tasks;
using VContainer;
using UniRx;

namespace Rules
{
    public class CoreController
    {
        public IntReactiveProperty Score { get; private set; } = new();

        [Inject]
        private GameData gameData;
        [Inject]
        private CoreSettings coreSettings;
        [Inject]
        private ItemField gameField;
        [Inject]
        private PlayerItemQueue playerItemQueue;
        [Inject]
        private PlayerItem playerItem;
        [Inject]
        private IPlayerControl playerControl;

        public event Action OnDefeat;
        public event Action OnVictory;

        public CoreController()
        {

        }

        public void Init()
        {
            UpdatePlayerItem();
            playerItem.onMoveStart += OnMoveStart;
            playerItem.onHit += OnHit;
            playerItem.onMoveEnd += OnMoveEnd;
        }

        private async void OnHit()
        {
            playerItem.gameObject.SetActive(false);

            var node = gameField.PlaceItem(playerItem.transform.position, playerItem.ItemType);
            node.Item.Placed();

            foreach (var neighbourItem in gameField.GetNeighbourItems(node))
            {
                neighbourItem.Item.Shake(node.transform.position);
            }

            var matched = gameField.TryMatch(node);

            if (matched.Count >= 3)
            {
                await Task.Delay(100);

                foreach (var matchedNode in matched)
                {
                    await Task.Delay(UnityEngine.Random.Range(30, 60));
                    matchedNode.Item.Removed();
                }

                await Task.Delay(200);

                foreach (var matchedNode in matched)
                {
                    gameField.RemoveItem(matchedNode);
                }

                var notConnectedItems = gameField.GetNotConnectedItems();

                foreach (var notConnected in notConnectedItems)
                {
                    notConnected.Item.FreeFall();
                    await Task.Delay(UnityEngine.Random.Range(20, 40));
                    gameField.RemoveItem(notConnected);
                }

                Score.Value += matched.Count;
                playerItemQueue.IncreaseItemsRemain();
            }

            if (CheckGameEndConditions()) return;

            NextTurn();
        }

        private void OnMoveStart()
        {
            playerControl.ControlEnabled = false;
        }

        private void OnMoveEnd()
        {
            NextTurn();
        }

        private void NextTurn()
        {
            playerItemQueue.NextItem();
            UpdatePlayerItem();
            playerItem.ResetPosition();

            if (CheckGameEndConditions()) return;

            playerItem.gameObject.SetActive(true);
            playerControl.ControlEnabled = true;
        }

        private void UpdatePlayerItem()
        {
            playerItem.SetType(playerItemQueue.CurrentItemType);
        }

        private bool CheckGameEndConditions()
        {
            if (playerItemQueue.ItemsRemain.Value <= 0)
            {
                GameDefeat();
                return true;
            }
            else if (IsVictroyCondition())
            {
                GameVictory();
                return true;
            }

            return false;
        }

        private void GameDefeat()
        {
            OnDefeat?.Invoke();
        }

        private void GameVictory()
        {
            gameData.progressData.levelIndex++;
            OnVictory?.Invoke();
        }

        private bool IsVictroyCondition()
        {
            return gameField.GetTopItemsCount() <= coreSettings.gameFieldSettings.topItemsToVictory;
        }
    }
}
