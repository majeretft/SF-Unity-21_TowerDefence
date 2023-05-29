using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileHoming : Projectile
    {
        [SerializeField]
        private Transform _target;
        public Transform Target
        {
            get => _target;
            set => _target = value;
        }

        private Rigidbody2D _rb;

        private void Start() {
            _rb = transform.root.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!_target)
                return;

            var direction = _target.position - transform.position;
            direction.Normalize();
            var rotateAmount = Vector3.Cross(direction, transform.up).z;

            _rb.angularVelocity = -rotateAmount * _angularSpeed;
        }
    }
}
