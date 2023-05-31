using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class Path : MonoBehaviour
    {
        [SerializeField]
        private AIPointPatrol[] _points;

        public int Length => _points.Length;

        public AIPointPatrol this[int i] => _points[i];

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            for (var i = 0; i < _points.Length; i++)
            {
                if (_points[i])
                {
                    Gizmos.DrawSphere(_points[i].transform.position, _points[i].Radius);

                    if (i > 0 && _points[i - 1])
                        Gizmos.DrawLine(_points[i - 1].transform.position, _points[i].transform.position);
                }
            }
        }
    }
}
