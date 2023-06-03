using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class LevelConditionTime : MonoBehaviour, ILevelCondition
    {
        [SerializeField]
        private float _timeLimit = 1f;

        public bool IsCompleted => Time.time > _timeLimit;
    }
}
