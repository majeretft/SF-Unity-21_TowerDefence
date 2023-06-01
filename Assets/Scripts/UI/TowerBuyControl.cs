using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField]
        private TowerProperties _props;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Transform _buildSite;
        public Transform BuildSite
        {
            set
            {
                _buildSite = value;
            }
        }

        private void Awake()
        {
            TDPlayer.SubscribeGoldUpdate(GoldStatusCheck);
        }

        private void Start()
        {
            _text.text = _props.goldCost.ToString();
            _button.GetComponent<Image>().sprite = _props.spriteGui;
        }

        private void GoldStatusCheck(int gold)
        {
            if (gold >= _props.goldCost != _button.interactable)
            {
                _button.interactable = !_button.interactable;
                _text.color = _button.interactable ? Color.white : Color.red;
            }
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(_props, _buildSite);
        }
    }
}
