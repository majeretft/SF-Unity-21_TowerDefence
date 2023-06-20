using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField]
        private float _radius;
        public float Radius => _radius;

        [SerializeField]
        private Color _color = new Color(0, 1, 0, 0.25f);

        public Vector2 GetRandomPointInsideArea()
        {
            return (Vector2)transform.position + Random.insideUnitCircle * _radius;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = _color;
            Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
        }
#endif
    }
}
