using System;
using System.Collections;
using SpaceShooter;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class AbilitiesUI : SingletonBase<AbilitiesUI>
    {
        [Serializable]
        public class FireAbility
        {
            [SerializeField]
            private int _damage = 1;
            [SerializeField]
            private int _cost = 10;
            [SerializeField]
            private Color _targetColor = Color.white;
            [SerializeField]
            private int _radius = 1;
            public void Use()
            {
                ClickProtectionUI.Instance.Activate((Vector2 v) =>
                {
                    Vector3 vector = v;
                    vector.z = -Camera.main.transform.position.z;

                    var pos = Camera.main.ScreenToWorldPoint(vector);

                    var colliders = Physics2D.OverlapCircleAll(pos, _radius);
                    // print(colliders.Length);
                    foreach (var c in colliders)
                    {
                        if (c.transform.root.TryGetComponent<Enemy>(out var e))
                        {
                            e.TakeDamage(_damage, DamageEnum.Magical);
                        }
                    }
                });
            }
        }

        [Serializable]
        public class SlowAbility
        {
            [SerializeField]
            private float _duration = 1;
            [SerializeField]
            private int _cost = 5;
            [SerializeField]
            private float _cooldown = 5;
            public void Use()
            {
                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<Spaceship>().DecreaseLinearVelocity();
                };

                foreach (var enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
                    Slow(enemy);

                EnemyWaveController.OnEnemySpawned += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(_duration);

                    EnemyWaveController.OnEnemySpawned -= Slow;

                    foreach (var ship in FindObjectsByType<Spaceship>(FindObjectsSortMode.None))
                        ship.RestoreLinearVelocity();
                };

                Instance.StartCoroutine(Restore());

                IEnumerator DisableAbilityButton()
                {
                    Instance._slowButton.interactable = false;
                    yield return new WaitForSeconds(Mathf.Max(_cooldown, _duration));
                    Instance._slowButton.interactable = true;
                }
                Instance.StartCoroutine(DisableAbilityButton());
            }
        }

        [SerializeField]
        private FireAbility _fireAbility;

        [SerializeField]
        private SlowAbility _slowAbility;

        [SerializeField]
        private Button _fireButton;

        [SerializeField]
        private Image _fireAbilityTarget;

        [SerializeField]
        private Button _slowButton;

        public void UseFireAbility() => _fireAbility.Use();
        public void UseSlowAbility() => _slowAbility.Use();
    }
}
