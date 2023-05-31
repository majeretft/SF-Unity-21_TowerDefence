using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class SpawnerEntity : Spawner
    {
        [SerializeField]
        private Entity[] _entityPrefab;

        protected override GameObject GetSwapnedEntity()
        {
            var index = UnityEngine.Random.Range(0, _entityPrefab.Length);
            var instance = Instantiate(_entityPrefab[index]);

            return instance.gameObject;
        }
    }
}
