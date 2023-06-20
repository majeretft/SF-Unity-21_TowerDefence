using SpaceShooter;
using Unity.VisualScripting;
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

        [SerializeField]
        private AudioClip _bgLevelMusic;

        [SerializeField, Range(0f, 1f)]
        private float _volume = 0.25f;

        private AudioSource _audioSource;

        private bool _isMenuBgPlaying = false;
        private bool _isLevelBgPlaying = false;

        protected override void Awake()
        {
            base.Awake();

            _audioSource = GetComponent<AudioSource>();
            Instance._audioSource.volume = _volume;
        }

        public void Play(Sound sound)
        {
            _audioSource.PlayOneShot(_sounds[sound]);
        }

        public void PlayMenuMusic()
        {
            if (!Instance._isMenuBgPlaying)
            {
                Instance._audioSource.clip = _bgMusic;
                Instance._audioSource.Play();
                Instance._isMenuBgPlaying = true;
            }

            Instance._isLevelBgPlaying = false;
        }

        public void PlayLevelMusic()
        {
            if (!Instance._isLevelBgPlaying)
            {
                Instance._audioSource.clip = _bgLevelMusic;
                Instance._audioSource.Play();
                Instance._isLevelBgPlaying = true;
            }

            Instance._isMenuBgPlaying = false;
        }
    }
}
