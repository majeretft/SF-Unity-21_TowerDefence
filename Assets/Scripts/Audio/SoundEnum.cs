namespace TowerDefence
{
    public enum Sound
    {
        Enemy_Die = 0,
        Enemy_Hit = 1,
        Level_Music = 2,
        Menu_Music = 3,
        Player_Ability_Fire = 4,
        Player_Ability_Slow = 5,
        Player_Lose = 6,
        Player_Victory = 7,
        Turret_Archer_Attack = 8,
        Turret_Ballista_Attack = 9,
        Turret_Cannon_Attack = 10,
        Turret_Mage_Attack = 11,
        UI_ButtonClick = 12,
        Enemy_Reach_Player = 13,
    }

    public static class SoundExtensions
    {
        public static void Play(this Sound sound)
        {
            SoundPlayer.Instance.Play(sound);
        }
    }
}
