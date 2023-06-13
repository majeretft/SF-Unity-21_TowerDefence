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

        [SerializeField]
        private Tower _towerPrefab;

        [SerializeField]
        private UpdateProperties _hpUpdate;

        [SerializeField]
        private UpdateProperties _goldUpdate;

        [SerializeField]
        private int _goldBonusPerUpdate = 1;

        private event Action<int> OnGoldUpdate;
        public event Action<int> OnHpUpdate;

        private int _goldUpdateLevel = 0;

        public void SubscribeGoldUpdate(Action<int> handler)
        {
            OnGoldUpdate += handler;
            OnGoldUpdate(Instance ? Instance._gold : 0);
        }

        public void SubscribeHpUpdate(Action<int> handler)
        {
            OnHpUpdate += handler;
            OnHpUpdate(Instance ? Instance.LifeCount : 0);
        }

        public void UnSubscribeGoldUpdate(Action<int> handler)
        {
            OnGoldUpdate -= handler;
        }

        public void UnSubscribeHpUpdate(Action<int> handler)
        {
            OnHpUpdate -= handler;
        }

        private void Start()
        {
            var level = Updates.GetLevel(_hpUpdate);
            _goldUpdateLevel = Updates.GetLevel(_goldUpdate);
            AddHp(LifeCount * level);

            OnGoldUpdate(_gold);
            OnHpUpdate(LifeCount);
        }

        public void ChangeGold(int value)
        {
            _gold += value;
            OnGoldUpdate(_gold);
        }

        public void AddEnemyGold(int value)
        {
            print($"Value = {value}, Gold bonus = {_goldBonusPerUpdate * _goldUpdateLevel}");
            ChangeGold(value + _goldBonusPerUpdate * _goldUpdateLevel);
        }


        public void ReduceHp(int value)
        {
            TakeDamage(value);
            OnHpUpdate(LifeCount);
        }

        public void TryBuild(TowerProperties props, Transform buildSite)
        {
            if (_gold < props.goldCost)
                return;

            ChangeGold(-props.goldCost);

            var tower = Instantiate<Tower>(_towerPrefab, buildSite.position, Quaternion.identity);
            tower.UseProps(props);
            Destroy(buildSite.gameObject);
        }
    }
}
