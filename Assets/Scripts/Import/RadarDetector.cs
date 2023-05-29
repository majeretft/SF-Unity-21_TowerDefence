using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SpaceShooter
{
    public class RadarDetector : MonoBehaviour
    {
        [SerializeField]
        private float _detectionRadius = 1;
        public float DetectionRadius
        {
            get => _detectionRadius;
            set => _detectionRadius = value > 0 ? value : 1;
        }

        [SerializeField]
        private Color _helperColor = new Color(0, 1, 0, 1f);

        public Transform DetectEntity()
        {
            var other = Physics2D.OverlapCircle(transform.position, _detectionRadius);

            return other?.transform;
        }

        public Transform[] DetectEntities()
        {
            var other = Physics2D.OverlapCircleAll(transform.position, _detectionRadius);

            return other
                .Where(x => x.transform.root.GetComponent<Distructible>() != null)
                .Select(x => x.transform)
                .ToArray();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = _helperColor;
            Handles.DrawWireDisc(transform.position, transform.forward, _detectionRadius);
        }
#endif
    }
}
