using UnboundLib.Cards;
using UnityEngine;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using BrutalGun;
using ModsPlus;
using System.Runtime.CompilerServices;
using Photon.Pun.UtilityScripts;
using System.Xml.Linq;
using UnboundLib.GameModes;
using System.Collections;
using System.Reflection;
using System.Numerics;

namespace BrutalGun.Cards
{
    public class Berserker : CustomEffectCard<BerserkerEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Berserker",
            Description = "A deathbed battle cocktail. Fulfill your goal and die with honor!",
            ModName = BrutalGun.BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.FirepowerYellow,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Healing",
                    amount = "+10",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Accuracy",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "After 10 sec",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Healing",
                    amount = "-10",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Speed",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Accuracy",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module };
        }
    }

    public class BerserkerEffect: CardEffect
    {
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            float spread = gun.spread;
            float speed = characterStats.movementSpeed;
            
            player.gameObject.AddComponent<BerserkerController>().Initialize(10, 10, 1000, 10000);
            yield break;

            //yield return HealingFase();
            //player.data.maxHealth += 1;
            //yield return СonsequenceFase(spread, speed);
            //player.data.maxHealth -= 1;           
        }

        private IEnumerator HealingFase()
        {           
            gun.multiplySpread = 0.8f;
            characterStats.movementSpeed = 1.15f;


            for (int i = 0; i < 7; i++)
            {
                player.data.healthHandler.Heal(10);
                yield return new WaitForSeconds(1f);
            }
        }
        private IEnumerator СonsequenceFase(float spread, float speed)
        {
            gun.multiplySpread = spread;
            characterStats.movementSpeed = speed;

         
            while (characterStats.health >= 0)
            {
                player.data.healthHandler.CallTakeDamage(UnityEngine.Vector2.up * gun.damage, UnityEngine.Vector2.zero);
                yield return new WaitForSeconds(1f);
            }           
        }
    }
}

