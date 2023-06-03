using System;
using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class UIMapCompletion : SingletonBase<UIMapCompletion>
    {
        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        [SerializeField]
        private EpisodeScore[] _completionData;

        private const string _filename = "game.json";

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore>.TryLoad(_filename, ref _completionData);
        }

        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < _completionData.Length)
            {
                episode = _completionData[id].episode;
                score = _completionData[id].score;
                return true;
            }

            episode = null;
            score = 0;
            return false;
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in _completionData)
            {
                if (item.episode == currentEpisode && levelScore > item.score)
                {
                    item.score = levelScore;
                    Saver<EpisodeScore>.TrySave(_filename, _completionData);
                }
            }
        }
    }
}
