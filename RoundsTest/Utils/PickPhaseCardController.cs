using System;
using System.Linq;
using UnboundLib;
using BrutalGun.Cards;
using UnityEngine;
using System.Collections.Generic;

namespace BrutalGun.Utils
{
    public static class PickPhaseCardController
    {
        private static int MaxWeapon;

        public static void SetValidation()
        {            
            ModdingUtils.Utils.Cards.instance.AddCardValidationFunction(MaxWeaponRule);
        }

        private static bool MaxWeaponRule(Player player, CardInfo cardInfo)
        {
            if (!(cardInfo.categories.Contains(MyCategories.Weapon)))
            {
                return true;
            }

            MaxWeapon = ((Transform[])CardChoice.instance.GetFieldValue("children")).Length / 2;
            MaxWeapon = MaxWeapon > 2 ? MaxWeapon : 3;
            List<GameObject> cardList = (List<GameObject>)CardChoice.instance.GetFieldValue("spawnedCards");

            if (cardList.Count() > 0 &&
                cardList.Where(cardObj => cardObj.GetComponent<CardInfo>().categories.Contains(MyCategories.Weapon)).Count() >= MaxWeapon)
            {
                return false;
            }

            return true;
        }

        public static void DefaultPlayer(Player player)
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
