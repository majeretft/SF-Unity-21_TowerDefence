using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class UpdateShopUI : MonoBehaviour
    {
        [SerializeField]
        private int _money;

        [SerializeField]
        private TextMeshProUGUI _moneyText;

        [SerializeField]
        private BuyUpdateUI[] _sales;

        private void Start()
        {
            foreach (var s in _sales)
            {
                s.Initialize();
                s.GetComponentInChildren<Button>().onClick.AddListener(UpdateMoney);
            }

            UpdateMoney();
        }

        public void UpdateMoney()
        {
            _money = UIMapCompletion.Instance.ScoreTotal;
            _money -= Updates.GetTotalCost();
            _moneyText.text = _money.ToString();

            foreach (var s in _sales)
            {
                s.CheckCost(_money);
            }
        }
    }
}
