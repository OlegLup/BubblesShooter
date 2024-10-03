using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerControl : MonoBehaviour, IPlayerControl
    {
        public bool ControlEnabled { get; set; }

        private Camera cameraMain;

        public event Action<Vector2> onControlStart;
        public event Action<Vector2> onControlUpdate;
        public event Action<Vector2> onControlEnd;

        private State state;

        private enum State
        {
            None,
            Idle,
            Active
        }

        public void Init()
        {
            cameraMain = Camera.main;
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entryBeginDrag = new EventTrigger.Entry();
            entryBeginDrag.eventID = EventTriggerType.BeginDrag;
            entryBeginDrag.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
            trigger.triggers.Add(entryBeginDrag);
            EventTrigger.Entry entryDrag = new EventTrigger.Entry();
            entryDrag.eventID = EventTriggerType.Drag;
            entryDrag.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
            trigger.triggers.Add(entryDrag);
            EventTrigger.Entry entryDragEnd = new EventTrigger.Entry();
            entryDragEnd.eventID = EventTriggerType.EndDrag;
            entryDragEnd.callback.AddListener((data) => { OnDragEnd((PointerEventData)data); });
            trigger.triggers.Add(entryDragEnd);

            state = State.Idle;
            ControlEnabled = true;
        }

        public void OnBeginDrag(PointerEventData data)
        {
            if (!ControlEnabled || state != State.Idle) return;

            state = State.Active;
            onControlStart?.Invoke(GetControlVector(data.position));
        }

        public void OnDrag(PointerEventData data)
        {
            if (!ControlEnabled || state != State.Active) return;

            onControlUpdate?.Invoke(GetControlVector(data.position));
        }

        public void OnDragEnd(PointerEventData data)
        {
            if (!ControlEnabled || state != State.Active) return;

            state = State.Idle;
            onControlEnd?.Invoke(GetControlVector(data.position));
        }

        private Vector2 GetControlVector(Vector2 point)
        {
            Vector2 diff = cameraMain.ScreenToWorldPoint(point) - transform.position;
            diff /= Screen.height;

            return diff;
        }
    }
}
