using UnityEngine;

namespace TowerDefence
{
    public class SoundHook : MonoBehaviour
    {
        public Sound Sound;

        public void Play() => Sound.Play();
    }
}
