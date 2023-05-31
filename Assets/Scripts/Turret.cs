using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private TurretModeEnum _mode;
        public TurretModeEnum Mode => _mode;

        [SerializeField]
        private TurretProperties _properties;

        private float _fireTimer;
        private float _multishotFireTimer;
        private float _multishotRestAmount = 0;
        private bool _isAutomaticLaunch = false;

        public bool CanFire => _fireTimer <= 0 && !_isAutomaticLaunch;

        private Spaceship _ship;
        private RadarDetector _radar;
        private Transform[] _radarTargets;
        private int _radarCurrentTargetIndex;
        #endregion

        #region Unity Events
        private void Start()
        {
            _ship = transform.root.GetComponent<Spaceship>();
            _radar = transform.root.GetComponent<RadarDetector>();
        }

        private void Update()
        {
            if (_fireTimer > 0)
                _fireTimer -= Time.deltaTime;

            if (_multishotFireTimer > 0)
                _multishotFireTimer -= Time.deltaTime;

            if (_multishotRestAmount <= 0)
                DisableAutomaticLaunch();

            if (_mode == TurretModeEnum.Auto && _fireTimer <= 0)
                Fire();

            if (_isAutomaticLaunch && _multishotFireTimer <= 0)
            {
                if (_properties.IsHoming)
                {
                    if (_radarCurrentTargetIndex < _radarTargets.Length)
                    {
                        var closestTarget = _radarTargets[_radarCurrentTargetIndex];
                        _radarCurrentTargetIndex++;

                        LaunchSingleHomingProjectile(closestTarget);
                        _multishotRestAmount--;
                        _multishotFireTimer = _properties.MultishotFireRate;
                    }
                    else
                    {
                        DisableAutomaticLaunch();
                    }
                }
                else
                {
                    DisableAutomaticLaunch();
                }
            }
        }
        #endregion

        #region Public API
        public void Fire()
        {
            if (!_properties || !CanFire)
                return;

            if (_ship)
            {
                if (_mode == TurretModeEnum.Primary && _ship.DrawEnergy(_properties.EnergyCost) == false)
                    return;

                if (_mode == TurretModeEnum.Secondary && _ship.DrawAmmo(_properties.AmmoCost) == false)
                    return;
            }

            if (_properties.MultishotAmount > 1)
                EnableAutomaticLaunch();
            else
                LaunchSingleProjectile();

            _fireTimer = _properties.FireRate;

            // TODO Sounds SFX
        }

        public void AssingProperties(TurretProperties props)
        {
            if (_mode != props.Mode)
                return;

            _fireTimer = 0;
            _properties = props;
            _mode = props.Mode;
        }
        #endregion

        #region Private Methods
        private void LaunchSingleProjectile()
        {
            var projectile = Instantiate(_properties.ProjectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.up = transform.up;
            projectile.SetParentShooter(_ship);
        }

        private void LaunchSingleHomingProjectile(Transform target)
        {
            var prefab = _properties.ProjectilePrefab as ProjectileHoming;

            if (!prefab)
            {
                Debug.LogWarning("Unable to cast ProjectilePrefab to ProjectileHoming");
                return;
            }

            var projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            projectile.transform.up = transform.up;
            projectile.Target = target;
            projectile.SetParentShooter(_ship);
        }

        private void EnableAutomaticLaunch()
        {
            _radarTargets = _radar.DetectEntities();
            if (_radarTargets.Length <= 0)
                return;

            _multishotRestAmount = _properties.MultishotAmount;
            _isAutomaticLaunch = true;
            _radarCurrentTargetIndex = 0;
        }

        private void DisableAutomaticLaunch()
        {
            _multishotRestAmount = 0;
            _isAutomaticLaunch = false;
            _radarTargets = null;
        }
        #endregion
    }
}
