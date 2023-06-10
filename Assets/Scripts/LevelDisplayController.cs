using UnityEngine;

namespace TowerDefence
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField]
        private UIMapLevel[] _levels;

        [SerializeField]
        private BonusLevelUI[] _bonusLevels;

        private void Start()
        {
            foreach (var l in _levels)
                l.Initialize();

            for (var i = 0; i < _bonusLevels.Length; i++)
                _bonusLevels[i].TryActivate();
        }
    }
}
