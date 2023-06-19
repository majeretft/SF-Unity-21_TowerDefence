using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public enum DamageEnum
    {
        Physical,
        Magical,
    }

    public class TDProjectile : Projectile
    {

        [SerializeField]
        private DamageEnum _damageType;

        [SerializeField]
        private Sound _soundStart = Sound.Turret_Archer_Attack;

        [SerializeField]
        private Sound _soundHit = Sound.Enemy_Hit;

        private void Start()
        {
            _soundStart.Play();
        }

        protected override void HandleHit(RaycastHit2D hit)
        {
            var enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy)
            {
                // print($"Projectile update level = {UpdateLevel}, damage bonus pre level = {_damageBonusPerUpdate}");
                enemy.TakeDamage(_damage + _damageBonusPerUpdate * UpdateLevel, _damageType);
                _soundHit.Play();
            }
        }
    }
}
