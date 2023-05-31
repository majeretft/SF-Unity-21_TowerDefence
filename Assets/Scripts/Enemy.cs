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
        public void UseProps(EnemyProperties props)
        {
            var view = transform.Find("View");

            var sr = view.GetComponent<SpriteRenderer>();
            sr.color = props.color;
            sr.transform.localScale = new Vector3(props.spriteScale.x, props.spriteScale.y, 1);

            var anim = view.GetComponent<Animator>();
            anim.runtimeAnimatorController = props.animation;

            GetComponent<Spaceship>().UseProps(props);
            GetComponentInChildren<CircleCollider2D>().radius = props.radius;
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

            if (props) {
                (target as Enemy).UseProps(props);
            }
        }
    }
#endif
}
