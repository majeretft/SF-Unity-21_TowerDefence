using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class LevelConditionTime : MonoBehaviour, ILevelCondition
    {
        [SerializeField]
        private float _timeLimit = 5f;

        private void Start()
        {
            _timeLimit += Time.time;
        }

        public bool IsCompleted => Time.time > _timeLimit;
    }
}
