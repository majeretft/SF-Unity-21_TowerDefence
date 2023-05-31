using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class TDPlayer : Player
    {
        [SerializeField]
        private int _gold = 0;

        public void ChangeCold(int value)
        {
            _gold += value;
        }
    }
}
