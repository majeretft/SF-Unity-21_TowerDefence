using SpaceShooter;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefence
{
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private int _damage = 1;

        [SerializeField]
        private int _gold = 1;

        public void UseProps(EnemyProperties props)
        {
            _damage = props.damage;
            _gold = props.gold;

            var view = transform.Find("View");

            var sr = view.GetComponent<SpriteRenderer>();
            sr.color = props.color;
            sr.transform.localScale = new Vector3(props.spriteScale.x, props.spriteScale.y, 1);

            var anim = view.GetComponent<Animator>();
            anim.runtimeAnimatorController = props.animation;
            if (TryGetFirstSpriteFromAnimation(props.animation, out var s))
                sr.sprite = s;

            GetComponent<Spaceship>().UseProps(props);
            GetComponentInChildren<CircleCollider2D>().radius = props.radius;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceHp(_damage);
        }

        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeCold(_gold);
        }

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
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var props = EditorGUILayout.ObjectField(null, typeof(EnemyProperties), false) as EnemyProperties;

            if (props)
            {
                (target as Enemy).UseProps(props);
            }
        }
    }
#endif
}
