using UnityEngine;

namespace SpaceShooter
{
    public class ExplosionGenerator : SingletonBase<ExplosionGenerator>
    {
        [SerializeField]
        private GameObject _explosionPrefab;

        public Explosion GenerateExplosion(Vector3 position, Quaternion rotation)
        {
            var instance = Instantiate(_explosionPrefab);
            var explosion = instance.GetComponent<Explosion>();

            explosion.transform.position = position;
            explosion.transform.rotation = rotation;
            
            return explosion;
        }
    }
}
