using System;
using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class Updates : SingletonBase<Updates>
    {
        [Serializable]
        private class UpdateSave
        {
            public UpdateProperties props;
            public int updateLevel = 0;
        }

        public const string FILE_NAME = "update.json";

        [SerializeField]
        private UpdateSave[] _updateData;

        public static void ResetSavedData()
        {
            FileHandler.Reset(FILE_NAME);
        }

        private new void Awake()
        {
            base.Awake();
            Saver<UpdateSave>.TryLoad(FILE_NAME, ref _updateData);
        }

        public static int GetTotalCost()
        {
            var result = 0;

            foreach (var u in Instance._updateData)
            {
                for (var i = 0; i < u.updateLevel; i++)
                {
                    result += u.props.costByLevel[i];
                }
            }

            return result;
        }

        public static void BuyUpdate(UpdateProperties props)
        {
            foreach (var item in Instance._updateData)
            {
                if (item.props == props)
                {
                    item.updateLevel++;
                    Saver<UpdateSave>.TrySave(FILE_NAME, Instance._updateData);
                }
            }
        }

        public static int GetLevel(UpdateProperties props)
        {
            foreach (var item in Instance._updateData)
            {
                if (item.props == props)
                {
                    return item.updateLevel;
                }
            }

            return 0;
        }
    }
}
