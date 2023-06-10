using TMPro;
using UnityEngine;

namespace TowerDefence
{
    public class NextWaveUI : MonoBehaviour
    {
        private EnemyWaveController _enemyController;

        private float _timeToNextWave;

        [SerializeField]
        private TextMeshProUGUI _bonusText;

        private void Start()
        {
            _enemyController = FindAnyObjectByType<EnemyWaveController>();

            EnemyWave.OnWavePrepare += (float time) =>
            {
                _timeToNextWave = time;
            };
        }

        private void Update()
        {
            var bonus = (int)_timeToNextWave;
            _bonusText.text = bonus < 0 ? "0" : bonus.ToString();
            _timeToNextWave -= Time.deltaTime;
        }

        public void CallWave()
        {
            if (_enemyController && enabled)
                _enemyController.ForceNextWave();
        }
    }
}
