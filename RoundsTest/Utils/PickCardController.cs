﻿using BrutalGun.Cards;

namespace BrutalGun.Utils
{
    public static class PickCardController
    {
        public static void InitPlayer(Player player)
        {
            ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(MyCategories.Vampire);
        }

        public static void SetVampirePick(Player player)
        {
            ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(MyCategories.Weapon);
            ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(MyCategories.Human);
            ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(MyCategories.Vampire);
        }
    }
}