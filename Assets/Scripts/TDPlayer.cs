using System;
using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance => Player.Instance as TDPlayer;

        [SerializeField]
        private int _gold = 0;

        public static event Action<int> OnGoldUpdate;
        public static event Action<int> OnHpUpdate;

        private void Start()
        {
            OnGoldUpdate(_gold);
            OnHpUpdate(LifeCount);
        }

        public void ChangeCold(int value)
        {
            _gold += value;
            OnGoldUpdate(_gold);
        }

        public void ReduceHp(int value)
        {
            TakeDamage(value);
            OnHpUpdate(LifeCount);
        }
    }
}
