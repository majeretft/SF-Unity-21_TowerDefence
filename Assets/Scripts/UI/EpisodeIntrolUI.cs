using SpaceShooter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class EpisodeIntrolUI : MonoBehaviour
    {
        private Episode _episode;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private TextMeshProUGUI _header;

        [SerializeField]
        private TextMeshProUGUI _descr;

        public void Show(Episode eposide)
        {
            gameObject.SetActive(true);
            _episode = eposide;

            _image.sprite = _episode.PreviewImage;
            _header.text = _episode.EpisodeName;
            _descr.text = _episode.EpisodeText;
        }

        public void Hide()
        {
            _episode = null;
            gameObject.SetActive(false);
        }

        public void OnProceedButtonClick()
        {
            LevelSequenceController.Instance.StartEpisode(_episode);
        }

        public void OnBackButtonClick()
        {
            Hide();
        }
    }
}
