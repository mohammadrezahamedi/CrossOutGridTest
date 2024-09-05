using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace RealityUnit
{
    using static UISummaryScreen;
    public class GameManager : MonoBehaviour
    {

        //If using UnityEvent is a must we can simply replace UnityEvent with the C# events but the C# events are more optimized and faster.
        //They start out allocating less garbage than UnityEvent especially when the event has parameter and is not supposed to disply in the inspector.
        //Reference : https://www.jacksondunstan.com/articles/3335

        public static Action<SummaryInfoDM, UnityAction> OnGameFinish;
        public static Action<int> OnDisplayTotalScore;
        [SerializeField] private GameObject _UISummaryScreen;


        //The best choice for undo - Cleaner implementation-Faster operations
        private Stack<int> selectedCells = new Stack<int>();

        private int _totalScore;
        public int TotalScore
        {
            get => _totalScore;
            private set
            {
                _totalScore = Mathf.Max(0, value);

                if (_totalScore >= 60)
                {
                    _UISummaryScreen.gameObject.SetActive(true);

                    SummaryInfoDM info = new SummaryInfoDM
                    {
                        PlayerWon = true,
                        Score = _totalScore
                    };
                    OnGameFinish?.Invoke(info, OnSummaryClosed);
                }
            }
        }

        private void OnEnable()
        {
            Cell.OnCellSelected += OnCellSelect;
            Cell.OnCellDeselected += OnCellDeselect;
        }

        private void OnDisable()
        {
            Cell.OnCellSelected -= OnCellSelect;
            Cell.OnCellDeselected -= OnCellDeselect;
        }

        private void OnCellDeselect(int cellValue)
        {
            if (selectedCells.Count > 0 && selectedCells.Peek() == cellValue)
            {
                selectedCells.Pop(); // Remove the last cell
                TotalScore -= cellValue;
                OnDisplayTotalScore?.Invoke(TotalScore);
            }
        }

        private void OnCellSelect(int cellValue)
        {
            selectedCells.Push(cellValue);
            TotalScore += cellValue;

            if (TotalScore == 21)
            {
                int penalty = TotalScore / 2;
                TotalScore -= penalty;
                Debug.Log("Exceeded 21 points! Penalty applied. Total Score: " + TotalScore);
            }
            OnDisplayTotalScore?.Invoke(TotalScore);
        }

        private void OnSummaryClosed()
        {
            _UISummaryScreen.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}