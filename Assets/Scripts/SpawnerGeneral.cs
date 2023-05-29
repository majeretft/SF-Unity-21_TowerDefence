using System;
using UnityEngine;

namespace SpaceShooter
{
    public class SpawnerGeneral : MonoBehaviour
    {
        public enum SpawnModeEnum
        {
            Start,
            Loop,
        }

        [SerializeField]
        private Entity[] _entityPrefab;

        [SerializeField]
        private AIPointPatrol _patrolPointTarget;

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
            if (_spawnMode == SpawnModeEnum.Start)
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

        private void SpawnEntities()
        {
            for (int i = 0; i < _spawnAmount; i++)
            {
                var index = UnityEngine.Random.Range(0, _entityPrefab.Length);

                var instance = Instantiate(_entityPrefab[index]);

                instance.transform.position = _spawnArea.GetRandomPointInsideArea();

                if (instance.TryGetComponent<AiController>(out var ai))
                {
                    var values = Enum.GetValues(typeof(AiBehaviourEnum));
                    // ai.Behaviour = (AiBehaviourEnum)values.GetValue(UnityEngine.Random.Range(1, values.Length));
                    ai.Behaviour = AiBehaviourEnum.Patrol;
                    ai.PatrolPoint = _patrolPointTarget;
                    var d = instance.GetComponent<Distructible>();
                    d.TeamId = 3;
                }
            }
        }

        // private AIPointPatrol SpawnPatrolArea(Vector3 position)
        // {
        //     var instance = Instantiate(_patrolPointTarget);
        //     instance.transform.position = position;

        //     return instance;
        // }
    }
}
