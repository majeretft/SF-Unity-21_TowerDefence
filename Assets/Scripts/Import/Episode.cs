using UnityEngine;

namespace SpaceShooter
{
    [CreateAssetMenu(menuName = "Space Shooter / Episode")]
    public class Episode : ScriptableObject
    {
        [SerializeField]
        private string _episodeName;
        public string EpisodeName => _episodeName;

        [SerializeField, TextArea(5, 20)]
        private string _episodeText;
        public string EpisodeText => _episodeText;

        [SerializeField]
        private string[] _levels;
        public string[] Levels => _levels;

        [SerializeField]
        private Sprite _previewImage;
        public Sprite PreviewImage => _previewImage;
    }
}
