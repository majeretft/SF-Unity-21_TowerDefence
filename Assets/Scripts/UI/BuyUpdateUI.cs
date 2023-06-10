using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class BuyUpdateUI : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TextMeshProUGUI _costText;

        [SerializeField]
        private TextMeshProUGUI _leveltext;

        [SerializeField]
        private Button _buyButton;

        [SerializeField]
        private UpdateProperties _props;

        private int _cost = 0;
        private bool _isUpdateFinished = false;

        public void Initialize()
        {
            var level = Updates.GetLevel(_props);
            _icon.sprite = _props.sprite;

            if (level >= _props.costByLevel.Length)
            {
                _buyButton.interactable = false;
                _leveltext.text = $"MAX LVL";
                _costText.text = "X";
                _isUpdateFinished = true;
            }
            else
            {
                var lvl = level >= _props.costByLevel.Length ? _props.costByLevel.Length - 1 : level;
                _cost = _props.costByLevel[lvl];
                _costText.text = _cost.ToString();
                _leveltext.text = $"LVL: {level + 1}";
            }
        }

        public void Buy()
        {
            Updates.BuyUpdate(_props);
            Initialize();
        }

        public void CheckCost(int money)
        {
            _buyButton.interactable = money >= _cost && !_isUpdateFinished;
        }
    }
}
