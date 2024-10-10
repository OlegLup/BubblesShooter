using Items;
using System;
using VContainer;
using UniRx;

namespace Player
{
    public class PlayerItemQueue
    {
        public IntReactiveProperty ItemsRemain { get; private set; } = new();
        public ItemType CurrentItemType { get; private set; } = ItemType.None;
        public ItemType NextItemType { get; private set; } = ItemType.None;

        [Inject]
        private RootSettings rootSettings;

        public PlayerItemQueue()
        {

        }

        public void Init()
        {
            RandomizeItem(); // Next item
            RandomizeItem(); // Current item
            ItemsRemain.Value = rootSettings.levelSettings.shots;
        }

        public void NextItem()
        {
            RandomizeItem();
            ItemsRemain.Value--;
        }

        public void IncreaseItemsRemain()
        {
            ItemsRemain.Value++;
        }

        private void RandomizeItem()
        {
            if (NextItemType != ItemType.None) CurrentItemType = NextItemType;

            NextItemType = (ItemType)UnityEngine.Random.Range((int)ItemType.None + 1, (int)ItemType.Last);
        }
    }
}
