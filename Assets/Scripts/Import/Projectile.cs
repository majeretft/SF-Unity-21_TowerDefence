using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField]
        protected float _speed;

        [SerializeField]
        protected float _angularSpeed;

        [SerializeField]
        protected float _lifeSpan;

        [SerializeField]
        protected int _damage;

        [SerializeField]
        protected int _damageBonusPerUpdate = 1;

        [SerializeField]
        private ImpactEffect _impactEffectPrefab;

        private Distructible _parent;

        private float _timer;

        public int UpdateLevel { get; set; }

        private void Update()
        {
            var movement = Time.deltaTime * _speed;
            var movementVector = transform.up * movement;

            var hit = Physics2D.Raycast(transform.position, transform.up, movement);

            if (hit)
            {
                HandleHit(hit);
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            _timer += Time.deltaTime;

            if (_timer > _lifeSpan)
                Destroy(gameObject);

            transform.position += new Vector3(movementVector.x, movementVector.y, transform.position.z);
        }

        protected virtual void HandleHit(RaycastHit2D hit)
        {
            var distructible = hit.collider.transform.root.GetComponent<Distructible>();

            if (distructible && distructible != _parent)
            {
                print($"Projectile update level = {UpdateLevel}, damage bonus pre level = {_damageBonusPerUpdate}");
                distructible.ApplyDamage(_damage + _damageBonusPerUpdate * UpdateLevel);

                // if (_parent == Player.Instance.PlayerShip)
                //     Player.Instance.AddScore(distructible.ScoreValue);
            }
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

        public void UseOtherProjectile(Projectile other)
        {
            other.GetData(out _speed, out _angularSpeed, out _damage, out _damageBonusPerUpdate, out _interactiveName, out _lifeSpan);
        }

        private void GetData(out float speed, out float angularSpeed, out int damage, out int damageBonusPerUpdate, out string interactiveName, out float lifeSpan)
        {
            speed = _speed;
            angularSpeed = _angularSpeed;
            damage = _damage;
            damageBonusPerUpdate = _damageBonusPerUpdate;
            interactiveName = _interactiveName;
            lifeSpan = _lifeSpan;
        }
    }
}

#if UNITY_EDITOR
namespace TowerDefence
{

    [CustomEditor(typeof(SpaceShooter.Projectile))]
    public class ProjectileInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Creare TD Projectile"))
            {
                var target = this.target as SpaceShooter.Projectile;
                var tdProj = target.gameObject.AddComponent<TDProjectile>();
                tdProj.UseOtherProjectile(target);
                DestroyImmediate(target, true);
            }
        }
    }
}
#endif