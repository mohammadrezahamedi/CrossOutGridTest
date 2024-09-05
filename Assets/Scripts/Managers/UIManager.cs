using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RealityUnit
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TMP_Text _totalScoreTxt;
        private void OnEnable()
        {
            GameManager.OnDisplayTotalScore += DisplayTotalScore;
        }

        private void DisplayTotalScore(int score)
        {
            _totalScoreTxt.text = $"Total Score: {score}";
        }

        private void OnDisable()
        {
            GameManager.OnDisplayTotalScore += DisplayTotalScore;
        }
    }
}