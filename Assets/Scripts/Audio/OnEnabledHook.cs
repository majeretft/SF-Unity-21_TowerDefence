using UnityEngine;

namespace TowerDefence
{
    public class OnEnabledHook : MonoBehaviour
    {
        [SerializeField]
        private Sound _sound;

        public void Start()
        {
            _sound.Play();
        }
    }
}
