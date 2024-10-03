using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VFXs
{
    public class ItemFallFx : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Rigidbody2D rigid;

        [SerializeField]
        private UnityEvent onFallEndFx;

        private void Start()
        {
            var item = GetComponentInParent<Item>();
            spriteRenderer.color = item.GetColor();
            rigid.mass *= Random.Range(1f, 1.5f);
            transform.SetParent(null);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            onFallEndFx?.Invoke();
        }
    }
}
