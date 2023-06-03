using SpaceShooter;
using TMPro;
using UnityEngine;

namespace TowerDefence
{
    public class UIMapLevel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private Episode _episode;

        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(_episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            _episode = episode;
            _text.text = $"{score}/3";
        }
    }
}
