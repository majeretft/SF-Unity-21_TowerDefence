using UnityEngine;

namespace SpaceShooter
{
    public enum TurretModeEnum
    {
        Primary,
        Secondary,
        Auto,
    }

    [CreateAssetMenu(menuName = "Space Shooter / Turret")]
    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField]
        private TurretModeEnum _mode;
        public TurretModeEnum Mode => _mode;

        [SerializeField]
        private Projectile _projectilePrefab;
        public Projectile ProjectilePrefab => _projectilePrefab;

        [SerializeField]
        private float _fireRate;
        public float FireRate => _fireRate;

        [SerializeField]
        private int _energyCost;
        public int EnergyCost => _energyCost;

        [SerializeField]
        private int _ammoCost;
        public int AmmoCost => _ammoCost;

        [SerializeField]
        private bool _isHoming;
        public bool IsHoming => _isHoming;

        [SerializeField]
        private int _multishotAmount;
        public int MultishotAmount => _multishotAmount;

        [SerializeField]
        private float _multishotFireRate;
        public float MultishotFireRate => _multishotFireRate;

        [SerializeField]
        private AudioClip _fireSoundEffect;
        public AudioClip FireSoundEffect => _fireSoundEffect;
    }
}
