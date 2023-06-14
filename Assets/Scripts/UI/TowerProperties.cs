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
        public TowerProperties[] updatesTo;
        
        [SerializeField]
        private UpdateProperties requiredUpdate;
        [SerializeField]
        private int requiredUpdateLevel;

        public bool IsAvailable()
        {
            if (!requiredUpdate)
                return true;

            return Updates.GetLevel(requiredUpdate) >= requiredUpdateLevel;
        }
    }
}
