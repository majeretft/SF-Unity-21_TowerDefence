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

        private static event Action<int> OnGoldUpdate;
        public static event Action<int> OnHpUpdate;

        public static void SubscribeGoldUpdate(Action<int> handler)
        {
            OnGoldUpdate += handler;
            OnGoldUpdate(Instance ? Instance._gold : 0);
        }

        public static void SubscribeHpUpdate(Action<int> handler)
        {
            OnHpUpdate += handler;
            OnHpUpdate(Instance ? Instance.LifeCount : 0);
        }

        public static void UnSubscribeGoldUpdate(Action<int> handler)
        {
            OnGoldUpdate -= handler;
        }

        public static void UnSubscribeHpUpdate(Action<int> handler)
        {
            OnHpUpdate -= handler;
        }

        protected override void Awake()
        {
            base.Awake();

            var level = Updates.GetLevel(_hpUpdate);
            AddHp(LifeCount * level);
        }

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

        public void TryBuild(TowerProperties props, Transform buildSite)
        {
            if (_gold < props.goldCost)
                return;

            ChangeCold(-props.goldCost);

            var tower = Instantiate<Tower>(_towerPrefab, buildSite.position, Quaternion.identity);
            tower.UseProps(props);
            Destroy(buildSite.gameObject);
        }
    }
}
