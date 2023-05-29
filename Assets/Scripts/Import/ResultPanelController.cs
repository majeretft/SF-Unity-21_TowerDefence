using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField]
        private TextMeshProUGUI _kills;

        [SerializeField]
        private TextMeshProUGUI _score;

        [SerializeField]
        private TextMeshProUGUI _time;

        [SerializeField]
        private TextMeshProUGUI _timeRecord;

        [SerializeField]
        private TextMeshProUGUI _result;

        [SerializeField]
        private TextMeshProUGUI _buttonNextText;

        private bool _isSuccess;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool isSuccess)
        {
            gameObject.SetActive(true);

            _isSuccess = isSuccess;
            _result.text = _isSuccess ? "Win" : "Loose";
            _buttonNextText.text = _isSuccess ? "Next" : "Restart";

            _kills.text = $"Kills: {levelResults.Kills}";
            _score.text = $"Score: {levelResults.Score}";
            _time.text = $"Time: {levelResults.Time} / Ref time: {levelResults.TimeRef}";
            _timeRecord.text = levelResults.IsTimeRecord ? "Double Score for time achivement!" : string.Empty;

            Time.timeScale = 0;
        }

        public void OnButtonNextClick()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (_isSuccess)
                LevelSequenceController.Instance.StartNextLevel();
            else
                LevelSequenceController.Instance.RestartLevel();
        }
    }
}
