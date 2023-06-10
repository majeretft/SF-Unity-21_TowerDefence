using System;
using System.Linq;
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

        private int _scoreTotal;
        public int ScoreTotal => _scoreTotal;

        public const string FILE_NAME = "game.json";

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            else
                Debug.Log($"Level completed with score: {levelScore}");
        }

        public static void ResetSavedData()
        {
            FileHandler.Reset(FILE_NAME);
        }

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore>.TryLoad(FILE_NAME, ref _completionData);
            _scoreTotal = _completionData.Aggregate(0, (acc, val) => acc += val.score);
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in _completionData)
            {
                if (item.episode == currentEpisode && levelScore > item.score)
                {
                    item.score = levelScore;
                    Saver<EpisodeScore>.TrySave(FILE_NAME, _completionData);
                }
            }
        }

        public int GetEpisodeScore(Episode episode)
        {
            if (!episode)
                return 0;

            for (int i = 0; i < _completionData.Length; i++)
            {
                if (_completionData[i].episode.GetInstanceID() != episode.GetInstanceID())
                    continue;

                return _completionData[i].score;
            }

            return 0;
        }

        public void GetEpisodeScore(Episode episode, out int prevScore, out int score, out bool isFirst)
        {
            isFirst = false;
            score = 0;
            prevScore = 0;

            if (!episode) {
                return;
            }

            for (int i = 0; i < _completionData.Length; i++)
            {
                if (_completionData[i].episode.GetInstanceID() != episode.GetInstanceID())
                    continue;

                score = _completionData[i].score;
                isFirst = i == 0;

                if (isFirst)
                    break;

                prevScore = _completionData[i - 1].score;

                break;
            }
        }
    }
}
