using Levels;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "RootSettings", menuName = "Settings/RootSettings")]
public class RootSettings : ScriptableObject
{
    [ReadOnly]
    [Tooltip("Updates via GameSceneLoader during CoreGameplay scene loading")]
    public LevelSettings levelSettings;
}
