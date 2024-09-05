using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RealityUnit
{
    public abstract class BaseCell : MonoBehaviour, ICell
    {
        protected Image _cell;
        [SerializeField] protected TMP_Text _cellTxt;
        protected Color _originColor;
        protected bool _isClicked;
        protected int _cellValue;

        public static Action<int> OnCellSelected;
        public static Action<int> OnCellDeselected;

        protected virtual void Start()
        {
            _cell = GetComponent<Image>();
            _originColor = _cell.color;
            _cell.GetComponent<Button>().onClick.AddListener(OnClickCell);
            _cellValue = UnityEngine.Random.Range(1, 12);
            _cellTxt.text = _cellValue.ToString();
        }

        public void Select()
        {
            _cell.color = Color.green;
            OnCellSelected?.Invoke(_cellValue);
            _isClicked = true;
        }

        public void Deselect()
        {
            _cell.color = _originColor;
            OnCellDeselected?.Invoke(_cellValue);
            _isClicked = false;
        }

        public int GetValue()
        {
            return _cellValue;
        }

        private void OnClickCell()
        {
            if (!_isClicked)
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }

        public abstract void SpecialBehavior();
    }
}
