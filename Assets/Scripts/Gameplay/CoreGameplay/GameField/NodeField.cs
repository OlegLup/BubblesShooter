using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace GameField
{
    public class NodeField
    {
        public Node[,] nodes;
        public List<Node> WallNodes { get; private set; }
        public List<Node> TopNodes { get; private set; }

        [Inject]
        private CoreSettings coreSettings;
        [Inject]
        private CoreSceneContent sceneContent;

        public NodeField()
        {

        }

        public void Init(int h, int w)
        {
            Debug.Assert(h > 0 && w > 0);

            float offsetX = coreSettings.gameFieldSettings.itemOffsets.x;
            float offsetY = coreSettings.gameFieldSettings.itemOffsets.y;
            Vector3 startPosition = coreSettings.gameFieldSettings.startPosition;

            nodes = new Node[h, w];

            // Create empty nodes
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Node node = GameObject.Instantiate(coreSettings.nodePrefab, sceneContent.nodesOrigin);
                    node.name = $"Node_{j}_{i}";
                    Vector3 position = startPosition + new Vector3(j * offsetX, -i * offsetY);
                    if (j % 2 != 0) position += new Vector3(0f, -offsetY * 0.5f);
                    node.transform.position = position;
                    nodes[i, j] = node;
                }
            }

            // Set neighbours
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Node node = nodes[i, j];
                    Node neighbour;

                    if (i > 0)
                    {
                        neighbour = nodes[i - 1, j];

                        if (!node.neighbours.Contains(neighbour))
                        {
                            node.neighbours.Add(neighbour);
                        }
                    }
                    if (j > 0)
                    {
                        neighbour = nodes[i, j - 1];

                        if (!node.neighbours.Contains(neighbour))
                        {
                            node.neighbours.Add(neighbour);
                        }
                    }
                    if (i < h - 1)
                    {
                        neighbour = nodes[i + 1, j];

                        if (!node.neighbours.Contains(neighbour))
                        {
                            node.neighbours.Add(neighbour);
                        }
                    }
                    if (j < w - 1)
                    {
                        neighbour = nodes[i, j + 1];

                        if (!node.neighbours.Contains(neighbour))
                        {
                            node.neighbours.Add(neighbour);
                        }
                    }
                    // hexagonal neighbours
                    if (j % 2 != 0)
                    {
                        if (j > 0 && i < h - 1)
                        {
                            neighbour = nodes[i + 1, j - 1];

                            if (!node.neighbours.Contains(neighbour))
                            {
                                node.neighbours.Add(neighbour);
                            }
                        }
                        if (j < w - 1 && i < h - 1)
                        {
                            neighbour = nodes[i + 1, j + 1];

                            if (!node.neighbours.Contains(neighbour))
                            {
                                node.neighbours.Add(neighbour);
                            }
                        }
                    }
                    else
                    {
                        if (j > 0 && i > 0)
                        {
                            neighbour = nodes[i - 1, j - 1];

                            if (!node.neighbours.Contains(neighbour))
                            {
                                node.neighbours.Add(neighbour);
                            }
                        }
                        if (j < w - 1 && i > 0)
                        {
                            neighbour = nodes[i - 1, j + 1];

                            if (!node.neighbours.Contains(neighbour))
                            {
                                node.neighbours.Add(neighbour);
                            }
                        }
                    }
                }
            }

            // staff nodes lists init
            WallNodes = new();
            TopNodes = new();

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (i == 0)
                    {
                        TopNodes.Add(nodes[i, j]);
                    }

                    if (i == 0 || j == 0 || j == w - 1)
                    {
                        WallNodes.Add(nodes[i, j]);
                    }
                }
            }
        }

        public Node GetClosestNode(Vector3 position)
        {
            Node result = null;
            float minDistance = float.MaxValue;

            foreach (Node node in nodes)
            {
                var sqrMagnitude = (position - node.transform.position).sqrMagnitude;

                if (sqrMagnitude < minDistance)
                {
                    result = node;
                    minDistance = sqrMagnitude;
                }
            }

            return result;
        }
    }
}
