using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics
    {
        [field: SerializeField]
        public int Kills { get; set; }

        [field: SerializeField]
        public int Score { get; set; }

        [field: SerializeField]
        public int Time { get; set; }

        public int TimeRef { get; set; }

        public bool IsTimeRecord { get; set; }

        public void Reset()
        {
            Kills = 0;
            Score = 0;
            Time = 0;
            IsTimeRecord = false;
        }

        public PlayerStatistics Clone()
        {
            return (PlayerStatistics)this.MemberwiseClone();
        }
    }
}
