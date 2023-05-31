using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    public abstract class Spawner : MonoBehaviour
    {
        public enum SpawnModeEnum
        {
            Start,
            Loop,
        }

        [SerializeField]
        private CircleArea _spawnArea;

        [SerializeField]
        private SpawnModeEnum _spawnMode;

        [SerializeField]
        private int _spawnAmount;

        [SerializeField]
        private float _spawnTimeInterval;

        private float _timer;

        private void Start()
        {
            SpawnEntities();

            _timer = _spawnTimeInterval;
        }

        private void Update()
        {
            if (_timer > 0)
                _timer -= Time.deltaTime;

            if (_spawnMode == SpawnModeEnum.Loop && _timer <= 0)
            {
                SpawnEntities();

                _timer = _spawnTimeInterval;
            }
        }

        protected abstract GameObject GetSwapnedEntity();

        private void SpawnEntities()
        {
            for (int i = 0; i < _spawnAmount; i++)
            {
                var instance = GetSwapnedEntity();
                instance.transform.position = _spawnArea.GetRandomPointInsideArea();
            }
        }
    }
}
