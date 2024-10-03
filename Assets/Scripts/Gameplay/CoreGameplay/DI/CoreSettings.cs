using GameField;
using Items;
using Levels;
using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "CoreSettings", menuName = "Settings/CoreSettings")]
public class CoreSettings : ScriptableObject
{
    [SerializeField]
    public GameFieldSettings gameFieldSettings;
    [SerializeField]
    public PlayerShotSettings playerShotSettings;
    [SerializeField]
    public Node nodePrefab;
    [SerializeField]
    public Item itemPrefab;
}
