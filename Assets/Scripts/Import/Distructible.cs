using System.Collections.Generic;
using TowerDefence;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Base class for all distructible entities
    /// </summary>
    public class Distructible : Entity
    {
        #region Properties

        /// <summary>
        /// Object can`t be destroyed
        /// </summary>
        [SerializeField]
        protected bool _isInvincible;

        /// <summary>
        /// Object will explode before being destroyed
        /// </summary>
        [SerializeField]
        protected bool _isExplodible;
        public bool IsExplodible
        {
            get => _isExplodible;
            set => _isExplodible = value;
        }

        /// <summary>
        /// Gets if object can`t be destroyed
        /// </summary>
        public bool IsInvincible => _isInvincible;

        /// <summary>
        /// Initial hit points amount
        /// </summary>
        [SerializeField]
        private int _initialHitPoints;

        /// <summary>
        /// Gets initial hit points amount
        /// </summary>
        public int InitialHitPoints => _initialHitPoints;

        /// <summary>
        /// Current hit points amount
        /// </summary>
        private int _hitPoints;

        /// <summary>
        /// Gets current hit points amount
        /// </summary>
        public int HitPoints => _hitPoints;

        [SerializeField]
        private UnityEvent _onDeathEvent;
        public UnityEvent OnDeathEvent => _onDeathEvent;

        private static HashSet<Distructible> _allDistructibles = new HashSet<Distructible>();
        public static IReadOnlyCollection<Distructible> AllDistructibles => _allDistructibles;

        public const int TEAM_NEUTRAL_ID = 0;

        [SerializeField]
        private int _teamId;
        public int TeamId { get => _teamId; set => _teamId = value; }

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            _hitPoints = _initialHitPoints;
        }

        #endregion

        #region Public API

        /// <summary>
        /// Applies damage to current object
        /// </summary>
        /// <param name="damage">Damage to apply</param>
        public void ApplyDamage(int damage)
        {
            if (this.IsInvincible || damage < 0)
                return;

            _hitPoints -= damage;

            if (_hitPoints <= 0)
            {
                if (_isExplodible)
                    Explode();
                else
                    HandleDistruction();
            }
        }

        #endregion

        /// <summary>
        /// Fires when HP becomes less then 0
        /// </summary>
        protected virtual void Explode()
        {
            var explosion = ExplosionGenerator.Instance.GenerateExplosion(transform.position, transform.rotation);
            explosion.ExplosionFinishedEvent.AddListener(HandleDistruction);
        }

        /// <summary>
        /// Fires when HP becomes less then 0
        /// </summary>
        protected virtual void HandleDistruction()
        {
            _onDeathEvent?.Invoke();
            Destroy(gameObject);
        }

        protected virtual void OnEnable()
        {
            if (_allDistructibles == null)
                _allDistructibles = new HashSet<Distructible>();

            _allDistructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            _allDistructibles.Remove(this);
        }

        #region Scores

        [SerializeField]
        private int _scoreValue;
        public int ScoreValue => _scoreValue;

        #endregion

        public void UseProps(EnemyProperties props)
        {
            _initialHitPoints = props.hp;
            _scoreValue = props.score;
        }
    }
}
