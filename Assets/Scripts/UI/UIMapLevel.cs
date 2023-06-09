using SpaceShooter;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class UIMapLevel : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _resultPanel;

        [SerializeField]
        private Image[] _resultImages;

        [SerializeField]
        private Episode _episode;

        [SerializeField]
        private EpisodeIntrolUI _introUi;

        private int _score;
        public int Score => _score;

        public void LoadLevel()
        {
            _introUi.Show(_episode);
        }

        public void Initialize()
        {
            UIMapCompletion.Instance.GetEpisodeScore(_episode, out var prevScore, out _score, out var isFirst);

            SetStatus(prevScore > 0 || isFirst);
        }

        public void Initialize(UIMapLevel parent)
        {
            _score = UIMapCompletion.Instance.GetEpisodeScore(_episode);

            SetStatus(parent.Score > 0);
        }

        private void SetStatus(bool isActive)
        {
            gameObject.SetActive(isActive);

            for (var i = 0; i < _score; i++)
                _resultImages[i].color = Color.white;
        }
    }
}
