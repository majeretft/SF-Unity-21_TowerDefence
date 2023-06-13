using TMPro;
using UnityEngine;

namespace TowerDefence
{
    public class UITextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            HP,
            Gold,
        }

        private TextMeshProUGUI _text;

        public UpdateSource source = UpdateSource.Gold;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();

            if (source == UpdateSource.Gold)
                TDPlayer.Instance.SubscribeGoldUpdate(UpdateText);

            if (source == UpdateSource.HP)
                TDPlayer.Instance.SubscribeHpUpdate(UpdateText);
        }

        private void UpdateText(int value)
        {
            _text.text = value.ToString();
        }

        private void OnDestroy()
        {
            if (source == UpdateSource.Gold)
                TDPlayer.Instance.UnSubscribeGoldUpdate(UpdateText);

            if (source == UpdateSource.HP)
                TDPlayer.Instance.UnSubscribeHpUpdate(UpdateText);
        }
    }
}
