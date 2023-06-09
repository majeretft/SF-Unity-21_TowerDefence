using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class Path : MonoBehaviour
    {
        [SerializeField]
        private CircleArea _startPoint;
        public CircleArea StartPoint => _startPoint;

        [SerializeField]
        private AIPointPatrol[] _points;

        public int Length => _points.Length;

        public AIPointPatrol this[int i] => _points[i];

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawSphere(_startPoint.transform.position, _startPoint.Radius);

            for (var i = 0; i < _points.Length; i++)
            {
                if (_points[i])
                {
                    Gizmos.DrawSphere(_points[i].transform.position, _points[i].Radius);

                    if (i > 0 && _points[i - 1])
                        Gizmos.DrawLine(_points[i - 1].transform.position, _points[i].transform.position);
                    else if (_startPoint)
                        Gizmos.DrawLine(_startPoint.transform.position, _points[i].transform.position);
                }
            }
        }
    }
}
