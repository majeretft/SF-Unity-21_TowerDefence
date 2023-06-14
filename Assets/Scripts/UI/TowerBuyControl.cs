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

        public void SetBuildSite(Transform position)
        {
            _buildSite = position;
        }

        public void SetProps(TowerProperties props)
        {
            _props = props;
        }

        private void Start()
        {
            TDPlayer.Instance.SubscribeGoldUpdate(GoldStatusCheck);
            _text.text = _props.goldCost.ToString();
            _button.GetComponent<Image>().sprite = _props.spriteGui;
        }

        private void OnDestroy()
        {
            TDPlayer.Instance.UnSubscribeGoldUpdate(GoldStatusCheck);
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
            BuildSite.HideControls();
        }
    }
}
