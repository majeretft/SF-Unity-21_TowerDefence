using System;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField]
        private int _lifeCount;
        public int LifeCount => _lifeCount;

        [SerializeField]
        private Spaceship _ship;
        public Spaceship PlayerShip => _ship;

        [SerializeField]
        private GameObject _shipPrefab;

        // [SerializeField]
        // private CameraController _cameraController;

        // [SerializeField]
        // private MovementController _movementController;

        protected override void Awake()
        {
            base.Awake();

            if (_ship != null)
                Destroy(_ship.gameObject);
        }

        private void Start()
        {
            Respawn();
        }

        private void RegisterEvent()
        {
            if (_ship)
                _ship.OnDeathEvent.AddListener(OnDeath);
        }

        private void OnDeath()
        {
            _lifeCount--;

            if (_lifeCount > 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip == null)
                return;

            var wasExplodible = _ship.IsExplodible;

            var instance = Instantiate(LevelSequenceController.PlayerShip);

            _ship = instance.GetComponent<Spaceship>();
            _ship.IsExplodible = wasExplodible;

            // _cameraController.SetTarget(instance.transform);
            // _movementController.SetTarget(_ship);

            RegisterEvent();
        }

        #region Scores

        public int Score { get; private set; }
        public int KillScore { get; private set; }

        public void AddScore(int amount)
        {
            Score += amount;
        }

        public void AddKillScore()
        {
            KillScore++;
        }

        protected void TakeDamage(int damage)
        {
            _lifeCount -= damage;

            if (_lifeCount <= 0)
                //LevelSequenceController.Instance.FinishCurrentLevel(false);
                LevelSequenceController.Instance.RestartLevel();
        }

        #endregion
    }
}
