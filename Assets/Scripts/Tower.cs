using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        private float _radius = 5f;

        private Turret[] _turrets;

        private Distructible _target;

        private void Start()
        {
            _turrets = GetComponentsInChildren<Turret>();
        }

        private void Update()
        {
            if (_target)
            {
                Vector2 targetDirection = _target.transform.position - transform.position;
                if (targetDirection.magnitude > _radius)
                {
                    _target = null;
                }
                else
                {
                    foreach (var t in _turrets)
                    {
                        t.transform.up = targetDirection;
                        t.Fire();
                    }
                }
            }
            else
            {
                var collider = Physics2D.OverlapCircle(transform.position, _radius);
                if (collider)
                    _target = collider.transform.root.GetComponent<Distructible>();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
