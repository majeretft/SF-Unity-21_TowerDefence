using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneName = "MainMenu";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public static Spaceship PlayerShip { get; set; }

        public bool IsLevelSuccess { get; private set; }

        public PlayerStatistics LevelStats { get; private set; }

        public List<PlayerStatistics> GameStats { get; private set; }

        protected override void Awake() {
            base.Awake();

            GameStats = new List<PlayerStatistics>();
        }

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            LevelStats = new PlayerStatistics();
            LevelStats.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            // SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            SceneManager.LoadScene(0);
        }

        public void FinishCurrentLevel(bool success)
        {
            IsLevelSuccess = success;
            CalculateLevelStats();
            GameStats.Add(LevelStats.Clone());

            ResultPanelController.Instance.ShowResults(LevelStats, IsLevelSuccess);
        }

        public void StartNextLevel()
        {
            LevelStats.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneName);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        private void CalculateLevelStats()
        {
            LevelStats.Score = Player.Instance.Score;
            LevelStats.Kills = Player.Instance.KillScore;
            LevelStats.Time = (int)LevelController.Instance.LevelTimer;
            LevelStats.TimeRef = (int)LevelController.Instance.ReferenceTime;

            var isTimeRecord = LevelController.Instance.LevelTimer < LevelController.Instance.ReferenceTime;
            if (isTimeRecord)
                LevelStats.Score *= 2;
            LevelStats.IsTimeRecord = isTimeRecord;
        }
    }
}
