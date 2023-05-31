using SpaceShooter;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public class TDPatrolController : AiController
    {
        private Path _path;
        private int _pathIndex;

        [SerializeField]
        private UnityEvent OnPathEnd;

        public void SetPath(Path path)
        {
            _path = path;
            _pathIndex = 0;
            SetPatrolBehaviour(_path[_pathIndex]);
        }

        protected override void AssignNewPatrolPoint()
        {
            _pathIndex++;

            if (_path.Length > _pathIndex)
            {
                SetPatrolBehaviour(_path[_pathIndex]);
            }
            else
            {
                Destroy(gameObject);
                OnPathEnd?.Invoke();
            }
        }
    }
}
