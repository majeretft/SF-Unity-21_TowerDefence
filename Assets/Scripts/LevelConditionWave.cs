using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class LevelConditionWave : MonoBehaviour, ILevelCondition
    {
        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;

        private void Start()
        {
            FindObjectOfType<EnemyWaveController>().OnWavesComplete += () =>
            {
                _isCompleted = true;
            };
        }
    }
}
