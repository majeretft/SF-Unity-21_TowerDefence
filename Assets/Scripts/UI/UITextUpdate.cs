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

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();

            if (source == UpdateSource.Gold)
                TDPlayer.SubscribeGoldUpdate(UpdateText);

            if (source == UpdateSource.HP)
                TDPlayer.SubscribeHpUpdate(UpdateText);
        }

        private void UpdateText(int value)
        {
            _text.text = value.ToString();
        }

        private void OnDestroy()
        {
            if (source == UpdateSource.Gold)
                TDPlayer.UnSubscribeGoldUpdate(UpdateText);

            if (source == UpdateSource.HP)
                TDPlayer.UnSubscribeHpUpdate(UpdateText);
        }
    }
}
