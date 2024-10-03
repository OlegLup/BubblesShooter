using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace GameField
{
    public class Node : MonoBehaviour
    {
        public List<Node> neighbours = new List<Node>();
        public Item Item
        {
            get => item;
            set
            {
                RemoveItem();
                item = value;
                item.transform.SetParent(transform);
                item.transform.localPosition = Vector3.zero;
            }
        }

        private Item item;

        public void RemoveItem()
        {
            if (item != null)
            {
                Destroy(item.gameObject);
                item = null;
            }
        }
    }
}
