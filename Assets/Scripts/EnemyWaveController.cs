using System;
using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class EnemyWaveController : MonoBehaviour
    {
        [SerializeField]
        private Enemy _enemyPrefab;

        [SerializeField]
        private Path[] _paths;

        [SerializeField]
        private EnemyWave _currentWave;

        private int _aliveEnemyCount = 0;

        public event Action OnWavesComplete;

        private void Start()
        {
            _currentWave.Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            if (!_currentWave)
            {
                if (_aliveEnemyCount <= 0)
                    OnWavesComplete?.Invoke();
                
                return;
            }

            foreach ((EnemyProperties props, int count, int pathIndex) in _currentWave.EnumerateSquads())
            {
                if (pathIndex >= _paths.Length || pathIndex < 0)
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                    continue;
                }

                for (int i = 0; i < count; i++)
                {
                    var instance = Instantiate(_enemyPrefab, _paths[pathIndex].StartPoint.GetRandomPointInsideArea(), Quaternion.identity);
                    _aliveEnemyCount++;
                    instance.OnDestroyEvent += RecordEnemyDeath;

                    instance.UseProps(props);

                    var ai = instance.GetComponent<TDPatrolController>();
                    ai.Behaviour = AiBehaviourEnum.Patrol;
                    ai.SetPath(_paths[pathIndex]);
                }
            }

            _currentWave = _currentWave.PrepareNext(SpawnEnemies);
        }

        private void RecordEnemyDeath()
        {
            _aliveEnemyCount--;

            if (_aliveEnemyCount > 0)
                return;

            ForceNextWave();
        }

        public void ForceNextWave()
        {
            if (_currentWave)
                TDPlayer.Instance.ChangeGold((int)_currentWave.GetRemainingTime());

            SpawnEnemies();
        }
    }
}
