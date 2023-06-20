using SpaceShooter;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefence
{
    public enum ArmorEnum
    {
        Physical = 0,
        Magical = 1,
    }

    [RequireComponent(typeof(Distructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        private static Func<int, DamageEnum, int, int>[] ArmorDamageFunc =
        {
            // ArmorEnum.Physical
            (int power, DamageEnum type, int armor) =>
            {
                // print($"ArmorEnum.Physical: power {power}, damage type {type}, armor {armor}");
                switch (type)
                {
                    case DamageEnum.Magical:
                        return power;
                    default:
                        return Mathf.Max(power - armor, 1);
                }
            },
            // ArmorEnum.Magical
            (int power, DamageEnum type, int armor) =>
            {
                // print($"ArmorEnum.Magical: power {power}, damage type {type}, armor {armor}");
                if (type == DamageEnum.Physical)
                {
                    return power / 2;
                }

                return Mathf.Max(power - armor, 1);
            }
        };

        [SerializeField]
        private int _damage = 1;

        [SerializeField]
        private int _gold = 1;

        [SerializeField]
        private int _armor = 0;

        [SerializeField]
        private ArmorEnum _armorType;

        public event Action OnDestroyEvent;

        private Distructible _distructible;

        public void UseProps(EnemyProperties props)
        {
            _damage = props.damage;
            _gold = props.gold;
            _armor = props.armor;
            _armorType = props._armorType;

            var view = transform.Find("View");

            var sr = view.GetComponent<SpriteRenderer>();
            sr.color = props.color;
            sr.transform.localScale = new Vector3(props.spriteScale.x, props.spriteScale.y, 1);

#if UNITY_EDITOR
            var anim = view.GetComponent<Animator>();
            anim.runtimeAnimatorController = props.animation;
            if (TryGetFirstSpriteFromAnimation(props.animation, out var s))
                sr.sprite = s;
#endif

            GetComponent<Spaceship>().UseProps(props);
            GetComponentInChildren<CircleCollider2D>().radius = props.radius;
        }

        public void TakeDamage(int damage, DamageEnum damageType)
        {
            var val = ArmorDamageFunc[(int)_armorType](damage, damageType, _armor);
            _distructible.ApplyDamage(val);
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceHp(_damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.AddEnemyGold(_gold);
        }

#if UNITY_EDITOR
        private bool TryGetFirstSpriteFromAnimation(RuntimeAnimatorController ac, out Sprite sprite)
        {
            sprite = null;

            if (!ac.animationClips[0])
                return false;

            var clip = ac.animationClips[0];
            var binding = AnimationUtility.GetObjectReferenceCurveBindings(clip)[0];
            var keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);
            sprite = (Sprite)keyframes[0].value;

            return true;
        }
#endif

        private void OnDestroy()
        {
            OnDestroyEvent?.Invoke();
        }

        private void Awake()
        {
            _distructible = GetComponent<Distructible>();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var props =
                EditorGUILayout.ObjectField(null, typeof(EnemyProperties), false)
                as EnemyProperties;

            if (props)
            {
                (target as Enemy).UseProps(props);
            }
        }
    }
#endif
}
