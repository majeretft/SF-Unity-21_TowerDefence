using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class EnemyWave : MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyProperties props;
            public int count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        [SerializeField]
        private float _timer = 10f;

        [SerializeField]
        private PathGroup[] _groups;

        [SerializeField]
        private EnemyWave _nextWave;

        private event Action OnWaveReady;

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time >= _timer)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }

        public IEnumerable<(EnemyProperties props, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < _groups.Length; i++)
            {
                foreach (var squad in _groups[i].squads)
                {
                    yield return (squad.props, squad.count, i);
                }
            }
        }

        public void Prepare(Action spawnEnemies)
        {
            enabled = true;
            _timer += Time.time;

            OnWaveReady += spawnEnemies;
        }

        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;

            if (!_nextWave)
                return null;

            _nextWave.Prepare(spawnEnemies);

            return _nextWave;
        }
    }
}
