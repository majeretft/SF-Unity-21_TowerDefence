using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(menuName = "Tower Defence / Update")]
    public class UpdateProperties : ScriptableObject
    {
        public Sprite sprite;
        public int[] costByLevel = { 3 };
    }
}
