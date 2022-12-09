﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
                Damage = 10 / Duration;
                Regen = 5;
            }
        }

        public static Dictionary<int, Stats> PlayerStatsDict = new Dictionary<int, Stats>();
    }
}