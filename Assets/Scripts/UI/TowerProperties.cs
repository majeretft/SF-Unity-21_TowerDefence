using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(menuName = "Tower Defence / Tower")]
    public class TowerProperties : ScriptableObject
    {
        public int goldCost = 15;
        public Sprite spriteGui;
        public Sprite sprite;
    }
}
