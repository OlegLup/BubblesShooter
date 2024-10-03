using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System.Linq;
using System;
using VContainer;
using Levels;

namespace GameField
{
    public class ItemField
    {
        [Inject]
        private RootSettings rootSettings;
        [Inject]
        private CoreSettings coreSettings;
        [Inject]
        private CoreSceneContent sceneContent;
        [Inject]
        private NodeField nodeField;

        public ItemField()
        {

        }

        public void Init()
        {
            Init(rootSettings.levelSettings);
        }

        private void Init(LevelSettings levelSettings)
        {
            var h = levelSettings.rows.Length;
            var w = levelSettings.rows[0].items.Length;

            nodeField.Init(h, w);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (levelSettings.rows[i].items[j] == ItemType.None)
                        continue;

                    nodeField.nodes[i, j].Item = CreateItem(levelSettings.rows[i].items[j]);
                }
            }
        }

        public Node PlaceItem(Vector2 position, ItemType itemType)
        {
            var node = nodeField.GetClosestNode(position);
            node.Item = CreateItem(itemType);

            return node;
        }

        public void RemoveItem(Node node)
        {
            node.RemoveItem();
        }

        public List<Node> TryMatch(Node node)
        {
            return GetSameItemNodes(node);
        }

        public List<Node> GetNotConnectedItems()
        {
            List<Node> notConnectedItems = new List<Node>();

            foreach (var notEmptyNode in GetAllNotEmptyNodes())
            {
                if (IsItemFreeFall(notEmptyNode))
                {
                    notConnectedItems.Add(notEmptyNode);
                }
            }

            return notConnectedItems;
        }

        public int GetTopItemsCount()
        {
            var topItems = from n in nodeField.TopNodes
                            where n.Item != null
                            select n;

            return topItems.Count();
        }

        public List<Node> GetNeighbourItems(Node node)
        {
            var neighbourItems = from n in node.neighbours
                           where n.Item != null
                           select n;

            return neighbourItems.ToList();
        }

        private Item CreateItem(ItemType itemType)
        {
            Item item = GameObject.Instantiate(coreSettings.itemPrefab, sceneContent.itemsOrigin);
            item.Init(itemType, coreSettings.gameFieldSettings);

            return item;
        }

        private List<Node> GetSameItemNodes(Node node)
        {
            List<Node> result = new();
            List<Node> checkedNodes = new();
            List<Node> matchedNodes = new();

            if (node.Item == null) return result;

            ItemType itemType = node.Item.ItemType;

            result.Add(node);
            checkedNodes.Add(node);
            matchedNodes.Add(node);

            while (true)
            {
                if (matchedNodes.Count <= 0) break;

                node = matchedNodes[0];
                matchedNodes.RemoveAt(0);

                foreach (Node neighbour in node.neighbours)
                {
                    if (neighbour.Item != null
                        && neighbour.Item.ItemType == itemType
                        && !checkedNodes.Contains(neighbour))
                    {
                        result.Add(neighbour);
                        matchedNodes.Add(neighbour);
                    }

                    checkedNodes.Add(neighbour);
                }
            }

            return result;
        }

        private bool IsItemFreeFall(Node node)
        {
            if (node.Item == null) return false;

            var wallNodes = from n in nodeField.WallNodes
                                 where n.Item != null
                                 select n;

            List<Node> connectedNodes = wallNodes.ToList();
            List<Node> checkedNodes = new();
            Node connectedNode;

            while (connectedNodes.Count > 0)
            {
                connectedNode = connectedNodes[0];
                checkedNodes.Add(connectedNode);

                if (connectedNode == node) return false;

                foreach (var neighbour in connectedNode.neighbours)
                {
                    if (neighbour.Item != null
                        && !checkedNodes.Contains(neighbour)
                        && !connectedNodes.Contains(neighbour))
                    {
                        connectedNodes.Add(neighbour);
                    }
                }

                connectedNodes.RemoveAt(0);
            }

            return true;
        }

        private List<Node> GetAllNotEmptyNodes()
        {
            List<Node> result = new();

            foreach (var node in nodeField.nodes)
            {
                if (node.Item != null)
                {
                    result.Add(node);
                }
            }

            return result;
        }
    }
}
