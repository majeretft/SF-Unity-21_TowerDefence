using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField]
        protected float _speed;

        [SerializeField]
        protected float _angularSpeed;

        [SerializeField]
        private float _lifeSpan;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private ImpactEffect _impactEffectPrefab;

        private Distructible _parent;

        private float _timer;

        private void Update()
        {
            var movement = Time.deltaTime * _speed;
            var movementVector = transform.up * movement;

            var hit = Physics2D.Raycast(transform.position, transform.up, movement);

            if (hit)
            {
                var distructible = hit.collider.transform.root.GetComponent<Distructible>();

                if (distructible && distructible != _parent)
                {
                    distructible.ApplyDamage(_damage);

                    // if (_parent == Player.Instance.PlayerShip)
                    //     Player.Instance.AddScore(distructible.ScoreValue);
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            _timer += Time.deltaTime;

            if (_timer > _lifeSpan)
                Destroy(gameObject);

            transform.position += new Vector3(movementVector.x, movementVector.y, transform.position.z);
        }

        private void OnProjectileLifeEnd(Collider2D collider, Vector2 position)
        {
            Destroy(gameObject);

            if (_impactEffectPrefab)
                SpawnImpactEffect(position);
        }

        public void SetParentShooter(Distructible parent)
        {
            _parent = parent;
        }

        private void SpawnImpactEffect(Vector3 position)
        {
            Instantiate(_impactEffectPrefab, position, Quaternion.identity);
        }
    }
}
