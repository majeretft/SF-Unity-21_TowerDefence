using SpaceShooter;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        private float _radius = 5f;

        private Turret[] _turrets;
#if UNITY_EDITOR
        public Turret[] Turrets
        {
            get => _turrets;
            set => _turrets = value;
        }
#endif

        private Distructible _target;

        private void Awake()
        {
            _turrets = GetComponentsInChildren<Turret>();
        }

        public void UseProps(TowerProperties props)
        {
            _radius = props.radius;
            GetComponentInChildren<SpriteRenderer>().sprite = props.sprite;

            foreach (var turret in _turrets)
            {
                turret.UseProps(props.turretProps);
            }

            var buildSite = GetComponentInChildren<BuildSite>();
            buildSite.AvailableTowers = props.updatesTo;
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

#if UNITY_EDITOR
    [CustomEditor(typeof(Tower))]
    public class TowerInspector : Editor
    {
        private Tower _target;

        private void OnEnable()
        {
            _target = target as Tower;
            _target.Turrets = _target.GetComponentsInChildren<Turret>();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var props =
                EditorGUILayout.ObjectField(null, typeof(TowerProperties), false)
                as TowerProperties;

            if (props)
            {
                (target as Tower).UseProps(props);
            }
        }
    }
#endif
}
