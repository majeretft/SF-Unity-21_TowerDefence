using UnityEngine;

namespace TowerDefence
{
    public class OnEnabledHook : MonoBehaviour
    {
        [SerializeField]
        private Sound _sound;

        [SerializeField]
        private bool _stopBgMusic;

        public void Start()
        {
            if (_stopBgMusic)
                _sound.StopMusic();

            _sound.Play();
        }
    }
}
