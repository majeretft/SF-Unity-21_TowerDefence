using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    [CreateAssetMenu(menuName = "Tower Defence / Tower")]
    public class TowerProperties : ScriptableObject
    {
        public int goldCost = 15;
        public Sprite spriteGui;
        public Sprite sprite;

        public float radius = 3.5f;

        public TurretProperties turretProps;
    }
}
