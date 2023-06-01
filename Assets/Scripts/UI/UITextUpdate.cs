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
                TDPlayer.OnGoldUpdate += UpdateText;

            if (source == UpdateSource.HP)
                TDPlayer.OnHpUpdate += UpdateText;
        }

        private void UpdateText(int value)
        {
            _text.text = value.ToString();
        }
    }
}
