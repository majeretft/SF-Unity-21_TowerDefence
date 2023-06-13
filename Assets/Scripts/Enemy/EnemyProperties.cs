using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(menuName = "Tower Defence / Enemy")]
    public class EnemyProperties : ScriptableObject
    {
        [Header("Appearance")]
        public Color color = Color.white;
        public Vector2 spriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController animation;

        [Header("Stats")]
        public float moveSpeed = 1;
        public int hp = 1;
        public int armor = 0;
        public ArmorEnum _armorType;
        public int score = 1;
        [Range(0.1f, 10f)]
        public float radius = 0.2f;
        public int damage = 1;
        public int gold = 1;
    }
}
