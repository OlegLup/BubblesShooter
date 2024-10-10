using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Doozy.Runtime.Reactor;
using Doozy.Runtime.Reactor.Animators;
using UniRx;

namespace Rules
{
    public class ScoreUiController : MonoBehaviour
    {
        [SerializeField]
        private Progressor scoreProgressor;
        [SerializeField]
        private UIAnimator uIanimator;
        [Inject]
        private CoreController coreRules;

        private void Start()
        {
            scoreProgressor.SetValueAt(coreRules.Score.Value);
            coreRules.Score.Subscribe(UpdateScore);
        }

        private void UpdateScore(int score)
        {
            scoreProgressor.fromValue = scoreProgressor.toValue;
            scoreProgressor.toValue = score;
            scoreProgressor.Play();
            uIanimator.Play();
        }
    }
}
