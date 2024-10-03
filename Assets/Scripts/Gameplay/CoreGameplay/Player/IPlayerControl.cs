using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IPlayerControl
    {
        public event Action<Vector2> onControlStart;
        public event Action<Vector2> onControlUpdate;
        public event Action<Vector2> onControlEnd;

        public bool ControlEnabled { get; set; }

        public void Init();
    }
}
