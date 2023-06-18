using UnityEngine;

namespace TowerDefence
{
    public class MouseFolliwerUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _followTargets;

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            foreach (var t in _followTargets)
            {
                var pos = Input.mousePosition;
                t.transform.position = pos;
            }
        }
    }
}
