using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        [SerializeField]
        private int _referenceTime;
        public int ReferenceTime => _referenceTime;

        [SerializeField]
        protected UnityEvent _levelCompletedEvent;

        private ILevelCondition[] _conditions;

        private bool _isLevelCompleted;

        private float _levelTimer;
        public float LevelTimer => _levelTimer;

        protected void Start()
        {
            _conditions = GetComponents<ILevelCondition>();
        }

        protected virtual void Update()
        {
            if (!_isLevelCompleted)
            {
                _levelTimer += Time.deltaTime;
                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            if (_conditions == null || _conditions.Length == 0)
                return;

            int numCompleted = 0;

            foreach (var c in _conditions)
            {
                if (c.IsCompleted)
                    numCompleted++;
            }

            if (numCompleted == _conditions.Length)
            {
                _isLevelCompleted = true;
                _levelCompletedEvent?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }
    }
}
