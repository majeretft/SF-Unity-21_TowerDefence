using UnityEngine;

namespace SpaceShooter
{
    public class AIPointPatrol : MonoBehaviour
    {
        [SerializeField]
        private float _radius;
        public float Radius => _radius;

        [Range(0f, 3f)]
        [SerializeField]
        private float _searchAttackTargetCoef;
        public float SearchAttackTargetRadius => _searchAttackTargetCoef * _radius;

        private static readonly Color _gizmosColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = AIPointPatrol._gizmosColor;
            Gizmos.DrawSphere(transform.position, _radius);
            Gizmos.DrawWireSphere(transform.position, SearchAttackTargetRadius);
        }
    }
}
