using System;
using System.Linq;
using TowerDefence;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Spaceship : Distructible
    {
        #region Fields

        /// <summary>
        /// Weight of space ship
        /// </summary>
        [Header("Space ship")]
        [SerializeField]
        private float _mass;

        /// <summary>
        /// Power of moving in forward direction
        /// </summary>
        [SerializeField]
        private float _thrust;

        /// <summary>
        /// Power of moving in radial direction
        /// </summary>
        [SerializeField]
        private float _torque;

        /// <summary>
        /// Max linear speed
        /// </summary>
        [SerializeField]
        private float _speedLinearMax;
        private float _speedLinearMaxInitial;
        public float SpeedLinearMax => _speedLinearMax;

        /// <summary>
        /// Max radial speed
        /// </summary>
        [SerializeField]
        private float _speedAngularMax;
        public float SpeedAngularMax => _speedAngularMax;

        /// <summary>
        /// Reference to Rigit Body 2D
        /// </summary>
        private Rigidbody2D _rigidBodyComponent;

        /// <summary>
        /// Reference to Trail renderer
        /// </summary>
        // private TrailRenderer _trailComponent;

        /// <summary>
        /// Ship weapons
        /// </summary>
        [SerializeField]
        private Turret[] _turrets;

        /// <summary>
        /// Max energy for turrets
        /// </summary>
        [SerializeField]
        // private int _maxEnergy;

        /// <summary>
        /// Max ammo for turrets
        /// </summary>
        // [SerializeField]
        // private int _maxAmmo;

        /// <summary>
        /// Current energy for turrets
        /// </summary>
        // private int _currentEnergy;

        /// <summary>
        /// Energy generator amount
        /// </summary>
        // private float _currentEnergyGenerated;

        /// <summary>
        /// Current ammo for turrets
        /// </summary>
        // private int _currentAmmo;

        /// <summary>
        /// Energy regeneration overtime
        /// </summary>
        // [SerializeField]
        // private int _energyRegen;

        /// <summary>
        /// Image reference of underlying sprite
        /// </summary>
        private Sprite _shipImage;
        public Sprite ShipImage
        {
            get
            {
                if (_shipImage)
                    return _shipImage;

                var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer)
                    _shipImage = spriteRenderer.sprite;

                return _shipImage;
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Current linear thrust of main engine (from -1.0 to 1.0)
        /// </summary>
        /// <value></value>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Current angular thrust of maneuver engine (from -1.0 to 1.0)
        /// </summary>
        /// <value></value>
        public float TorqueControl { get; set; }

        /// <summary>
        /// TODO: Fire selected weapons
        /// </summary>
        /// <param name="mode">Selected weapon</param>
        public void Fire(TurretModeEnum mode)
        {
            // _turrets
            //     .Where(x => x.Mode == mode)
            //     .ToList()
            //     .ForEach(x => x.Fire());
            return;
        }

        public void DecreaseLinearVelocity()
        {
            _speedLinearMax = _speedLinearMaxInitial / 2;
        }

        public void RestoreLinearVelocity()
        {
            _speedLinearMax = _speedLinearMaxInitial;
        }

        /// <summary>
        /// Add energy for turrets
        /// </summary>
        /// <param name="amount">Energy amount</param>
        // public void AddEnergy(int amount)
        // {
        //     _currentEnergy = Mathf.Clamp(_currentEnergy + amount, 0, _maxEnergy);
        // }

        /// <summary>
        /// Add ammo for turrets
        /// </summary>
        /// <param name="amount">Ammo amount</param>
        // public void AddAmmo(int amount)
        // {
        //     _currentAmmo = Mathf.Clamp(_currentAmmo + amount, 0, _maxAmmo);
        // }

        #endregion

        #region Unity Events

        protected override void Start()
        {
            base.Start();

            _rigidBodyComponent = GetComponent<Rigidbody2D>();
            _rigidBodyComponent.mass = _mass;
            _rigidBodyComponent.inertia = 1;

            // _trailComponent = GetComponentInChildren<TrailRenderer>();
            // _trailComponent.gameObject.SetActive(false);

            // InitResources();
        }

        private void Update()
        {
            // if (_trailComponent)
            //     _trailComponent.gameObject.SetActive(ThrustControl > 0);

            // RegenEnergy();
        }

        private void FixedUpdate()
        {
            UpdateRigitBody();
        }

        #endregion

        /// <summary>
        /// Change thrust and torque of space ship
        /// </summary>
        private void UpdateRigitBody()
        {
            _rigidBodyComponent.AddForce(_thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            _rigidBodyComponent.AddForce(-_rigidBodyComponent.velocity * (_thrust / _speedLinearMax) * Time.fixedDeltaTime, ForceMode2D.Force);

            _rigidBodyComponent.AddTorque(_torque * TorqueControl * Time.fixedDeltaTime, ForceMode2D.Force);
            _rigidBodyComponent.AddTorque(-_rigidBodyComponent.angularVelocity * (_torque / _speedAngularMax) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        // private void InitResources()
        // {
        //     _currentAmmo = _maxAmmo;
        //     _currentEnergy = _maxEnergy;
        // }

        // private void RegenEnergy()
        // {
        //     _currentEnergyGenerated += _energyRegen * Time.deltaTime;

        //     if (_currentEnergyGenerated >= _energyRegen)
        //     {
        //         _currentEnergy = Mathf.Clamp(_currentEnergy + _energyRegen, 0, _maxEnergy);
        //         _currentEnergyGenerated -= _energyRegen;
        //     }
        // }

        // TODO: Заглушка
        public bool DrawAmmo(int amount)
        {
            return true;
        }

        // TODO: Заглушка
        public bool DrawEnergy(int amount)
        {
            return true;
        }

        public void AssignWeapon(TurretProperties props)
        {
            _turrets
                .ToList()
                .ForEach(x => x.AssingProperties(props));
        }

        public new void UseProps(EnemyProperties props)
        {
            base.UseProps(props);
            _speedLinearMax = props.moveSpeed;
            _speedLinearMaxInitial = _speedLinearMax;
        }
    }
}
