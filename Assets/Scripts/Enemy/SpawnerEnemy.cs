using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class SpawnerEnemy : Spawner
    {
        [SerializeField]
        private Enemy _enemyPrefab;

        [SerializeField]
        private EnemyProperties[] _enemyProps;

        [SerializeField]
        private Path _path;

        protected override GameObject GetSwapnedEntity()
        {
            var instance = Instantiate(_enemyPrefab);

            var i = Random.Range(0, _enemyProps.Length);
            instance.UseProps(_enemyProps[i]);

            var ai = instance.GetComponent<TDPatrolController>();
            ai.Behaviour = AiBehaviourEnum.Patrol;
            ai.SetPath(_path);
            var d = instance.GetComponent<Distructible>();
            d.TeamId = 3;

            return instance.gameObject;
        }
    }
}
