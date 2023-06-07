using SpaceShooter;
// using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class UIMapLevel : MonoBehaviour
    {
        // [SerializeField]
        // private TextMeshProUGUI _text;

        [SerializeField]
        private RectTransform _resultPanel;

        [SerializeField]
        private Image[] _resultImages;

        private Episode _episode;

        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(_episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            _episode = episode;
            // _resultPanel.gameObject.SetActive(score > 0);

            for (var i = 0; i < score; i++)
            {
                _resultImages[i].color = Color.white;
            }

            // _text.text = $"{score}/3";
        }
    }
}
