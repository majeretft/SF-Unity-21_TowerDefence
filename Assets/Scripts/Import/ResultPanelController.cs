// using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        // [SerializeField]
        // private TextMeshProUGUI _kills;

        // [SerializeField]
        // private TextMeshProUGUI _score;

        // [SerializeField]
        // private TextMeshProUGUI _time;

        // [SerializeField]
        // private TextMeshProUGUI _timeRecord;

        // [SerializeField]
        // private TextMeshProUGUI _result;

        // [SerializeField]
        // private TextMeshProUGUI _buttonNextText;

        [SerializeField]
        private GameObject _panelSuccess;

        [SerializeField]
        private GameObject _panelFailure;

        // private bool _isSuccess;

        private void Start()
        {
            // gameObject.SetActive(false);
        }

        public void ShowResults(bool isSuccess)
        {
            // _isSuccess = isSuccess;
            // _result.text = _isSuccess ? "Win" : "Loose";
            // _buttonNextText.text = _isSuccess ? "Next" : "Restart";

            // _kills.text = $"Kills: {levelResults.Kills}";
            // _score.text = $"Score: {levelResults.Score}";
            // _time.text = $"Time: {levelResults.Time} / Ref time: {levelResults.TimeRef}";
            // _timeRecord.text = levelResults.IsTimeRecord ? "Double Score for time achivement!" : string.Empty;

            _panelSuccess.SetActive(isSuccess);
            _panelFailure.SetActive(!isSuccess);

            // Time.timeScale = 0;
        }

        public void OnButtonNextClick()
        {
            LevelSequenceController.Instance.StartNextLevel();
        }

        public void OnButtonRestartClick()
        {
            LevelSequenceController.Instance.RestartLevel();
        }
    }
}
