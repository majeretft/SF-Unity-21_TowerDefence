using System;
using System.Collections;
using SpaceShooter;
using TMPro;
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
            public int Cost => _cost;
            [SerializeField]
            private Color _targetColor = Color.white;
            [SerializeField]
            private int _radius = 1;
            public int Radius => _radius;
            [SerializeField]
            private ImpactEffect _impactEffect;
            [SerializeField]
            private UpdateProperties _requiredUpdate;
            public void Use()
            {
                Instance._mouseFollower.enabled = true;
                Instance._fireAbilityTarget.gameObject.SetActive(true);
                TDPlayer.Instance.ChangeGold(-_cost);

                ClickProtectionUI.Instance.Activate((Vector2 v) =>
                {
                    Instance._mouseFollower.enabled = false;
                    Instance._fireAbilityTarget.gameObject.SetActive(false);

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
                            if (_impactEffect)
                                Instantiate(_impactEffect, e.transform.position, Quaternion.identity);
                        }
                    }
                });
            }

            public void UpdateStatus(int gold)
            {
                var hasUpdate = _requiredUpdate ? Updates.GetLevel(_requiredUpdate) > 0 : true;
                Instance._fireButton.interactable = gold >= _cost && hasUpdate;
            }
        }

        [Serializable]
        public class SlowAbility
        {
            [SerializeField]
            private float _duration = 1;
            [SerializeField]
            private int _cost = 5;
            public int Cost => _cost;
            [SerializeField]
            private float _cooldown = 5;
            [SerializeField]
            private UpdateProperties _requiredUpdate;

            private bool _isCooldown = false;
            public void Use()
            {
                TDPlayer.Instance.ChangeGold(-_cost);

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
                    _isCooldown = true;
                    Instance._slowButton.interactable = false;
                    yield return new WaitForSeconds(Mathf.Max(_cooldown, _duration));
                    _isCooldown = false;
                    UpdateStatus(Instance._lastGoldAmount);
                }
                Instance.StartCoroutine(DisableAbilityButton());
            }

            public void UpdateStatus(int gold)
            {
                var hasUpdate = _requiredUpdate ? Updates.GetLevel(_requiredUpdate) > 0 : true;
                Instance._slowButton.interactable = gold >= _cost && !_isCooldown && hasUpdate;
            }
        }

        [SerializeField]
        private Button _fireButton;
        [SerializeField]
        private FireAbility _fireAbility;
        [SerializeField]
        private Image _fireAbilityTarget;
        [SerializeField]
        private TextMeshProUGUI _fireAbilityCostText;

        [SerializeField]
        private Button _slowButton;
        [SerializeField]
        private SlowAbility _slowAbility;
        [SerializeField]
        private TextMeshProUGUI _slowAbilityCostText;

        private MouseFolliwerUI _mouseFollower;

        private int _lastGoldAmount = 0;

        protected override void Awake()
        {
            base.Awake();
            _mouseFollower = GetComponent<MouseFolliwerUI>();

            var vecWorld = Vector2.one * _fireAbility.Radius * 2;
            var vecScreen = Camera.main.WorldToScreenPoint(vecWorld);

            _fireAbilityTarget.rectTransform.sizeDelta = Vector2.one * vecScreen.magnitude / 4;
        }

        private void Start()
        {
            TDPlayer.Instance.SubscribeGoldUpdate(gold =>
            {
                _fireAbility.UpdateStatus(gold);
                _slowAbility.UpdateStatus(gold);
                _lastGoldAmount = gold;
            });

            _fireAbilityCostText.text = _fireAbility.Cost.ToString();
            _slowAbilityCostText.text = _slowAbility.Cost.ToString();
        }

        public void UseFireAbility() => _fireAbility.Use();
        public void UseSlowAbility() => _slowAbility.Use();

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            var pos = Camera.main.ScreenToWorldPoint(_fireAbilityTarget.transform.position);
            Gizmos.DrawWireSphere(pos, _fireAbility.Radius);
        }
#endif
    }
}
