using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System;
using VContainer;
using UnityEngine.Events;

namespace Player
{
    public class PlayerItem : Item
    {
        [SerializeField]
        private Rigidbody2D rigid;
        [Inject]
        private IPlayerControl playerControl;
        [Inject]
        private CoreSettings coreSettings;

        public event Action onMoveStart;
        public event Action onHit;
        public event Action onMoveEnd;

        [SerializeField]
        private UnityEvent onAimStartFx;
        [SerializeField]
        private UnityEvent onShotFx;

        private State state;

        private enum State
        {
            None,
            Idle,
            Move
        }

        public void Init()
        {
            base.Init(ItemType.None, coreSettings.gameFieldSettings);
            playerControl.onControlStart += AimStart;
            playerControl.onControlEnd += MoveStart;
            state = State.Idle;
        }

        public void AimStart(Vector2 force)
        {
            onAimStartFx?.Invoke();
        }

        public void MoveStart(Vector2 force)
        {
            onShotFx?.Invoke();
            state = State.Move;
            force = force.normalized * coreSettings.playerShotSettings.shotForce;
            rigid.velocity = force;
            onMoveStart?.Invoke();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (state != State.Move)            return;
            if (collision.rigidbody == null)    return;

            string layerName = LayerMask.LayerToName(collision.rigidbody.gameObject.layer);

            switch (layerName)
            {
                case "Bottom":
                    state = State.Idle;
                    onMoveEnd?.Invoke();
                    break;
                case "Item":
                    state = State.Idle;
                    onHit?.Invoke();
                    break;
            }
        }

        public void ResetPosition()
        {
            transform.localPosition = Vector3.zero;
            rigid.velocity = Vector3.zero;
        }
    }
}
