using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerShotSettings", menuName = "Settings/PlayerShotSettings", order = 2)]
    public class PlayerShotSettings : ScriptableObject
    {
        public float shotForce = 10f;
        public float trajectoryDistance = 5f;
    }
}
