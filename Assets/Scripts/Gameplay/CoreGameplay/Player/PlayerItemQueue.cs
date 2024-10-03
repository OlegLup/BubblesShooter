using Items;
using System;
using VContainer;

namespace Player
{
    public class PlayerItemQueue
    {
        public int ItemsRemain { get; private set; }
        public ItemType CurrentItemType { get; private set; } = ItemType.None;
        public ItemType NextItemType { get; private set; } = ItemType.None;

        [Inject]
        private RootSettings rootSettings;

        public event Action OnDataChange;

        public PlayerItemQueue()
        {

        }

        public void Init()
        {
            RandomizeItem(); // Next item
            RandomizeItem(); // Current item
            ItemsRemain = rootSettings.levelSettings.shots;
        }

        public void NextItem()
        {
            RandomizeItem();
            ItemsRemain--;
            OnDataChange.Invoke();
        }

        public void IncreaseItemsRemain()
        {
            ItemsRemain++;
            OnDataChange.Invoke();
        }

        private void RandomizeItem()
        {
            if (NextItemType != ItemType.None) CurrentItemType = NextItemType;

            NextItemType = (ItemType)UnityEngine.Random.Range((int)ItemType.None + 1, (int)ItemType.Last);
        }
    }
}
