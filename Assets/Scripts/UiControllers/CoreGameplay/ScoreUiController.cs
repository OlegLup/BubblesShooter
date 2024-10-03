using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Doozy.Runtime.Reactor;
using Doozy.Runtime.Reactor.Animators;

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
            coreRules.OnScoreChange += UpdateScore;
            scoreProgressor.SetValueAt(coreRules.Score);
        }

        private void UpdateScore()
        {
            scoreProgressor.fromValue = scoreProgressor.toValue;
            scoreProgressor.toValue = coreRules.Score;
            scoreProgressor.Play();
            uIanimator.Play();
        }
    }
}
