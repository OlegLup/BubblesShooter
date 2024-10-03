using Cysharp.Threading.Tasks;
using DG.Tweening;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFXs
{
    public class ItemShakeFx : MonoBehaviour
    {
        [SerializeField]
        private Item item;
        [SerializeField]
        private Transform content;

        public void Play(Vector2 origin)
        {
            ShakeTask(origin).Forget();
        }

        private async UniTask ShakeTask(Vector2 origin)
        {
            Vector2 offset = ((Vector2)content.transform.position - origin).normalized * Random.Range(0.1f, 0.2f);

            Sequence sequence = DOTween.Sequence();

            sequence.Append(content.DOLocalMove(offset, 0.1f));
            sequence.Append(content.DOLocalMove(Vector2.zero, 0.2f));

            sequence.Play();

            await UniTask.WaitWhile(sequence.IsActive);

            sequence.Kill();
        }
    }
}
