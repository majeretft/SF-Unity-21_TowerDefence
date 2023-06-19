using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField]
        private SoundProperties _sounds;

        [SerializeField]
        private AudioClip _bgMusic;

        [SerializeField, Range(0f, 1f)]
        private float _volume = 0.25f;

        private AudioSource _audioSource;

        private bool _isMenuBgPlaying = false;

        protected override void Awake()
        {
            base.Awake();

            if (!Instance._isMenuBgPlaying)
            {
                _audioSource = GetComponent<AudioSource>();
                Instance._audioSource.clip = _bgMusic;
                Instance._audioSource.volume = _volume;
                Instance._audioSource.Play();
                Instance._isMenuBgPlaying = true;
            }
        }

        public void Play(Sound sound)
        {
            _audioSource.PlayOneShot(_sounds[sound]);
        }
    }
}
