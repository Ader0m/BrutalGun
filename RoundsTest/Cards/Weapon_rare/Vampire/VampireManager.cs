using System.Collections.Generic;

namespace BrutalGun.Cards
{
    public static class VampireManager
    {
        public class Stats
        {
            public float Duration;
            public float Damage;
            public float Regen;

            public Stats() 
            {
                Duration = 2;
                Damage = 5;
                Regen = 5;
            }
        }

        public static Dictionary<int, Stats> PlayerStatsDict = new Dictionary<int, Stats>();

        public static void Restore()
        {
            PlayerStatsDict.Clear();
        }
    }
}
