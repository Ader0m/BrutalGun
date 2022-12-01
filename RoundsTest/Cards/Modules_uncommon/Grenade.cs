using UnboundLib.Cards;
using UnityEngine;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using BrutalGun;
using ModsPlus;
using System.Runtime.CompilerServices;
using CardsPlusPlugin;
using Photon.Pun.UtilityScripts;
using System.Xml.Linq;

namespace BrutalGun.Cards
{
    public class Grenade : CustomEffectCard<GrenadeEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Granade",
            Description = "Send a cake! Or a grenade! After all, the cake is a lie...",
            ModName = BrutalGun.BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Work with block",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }              
            }            
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module };
        }
    }

    public class GrenadeEffect : CardEffect
    {
        public override void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            float SaveDamage = gun.damage;
            float SaveProjectileSpeed = gun.projectileSpeed;
            float SaveGravity = gun.gravity;
            float SaveExplDamage = gun.explodeNearEnemyDamage;
            float SaveExplRange = gun.explodeNearEnemyRange;
            

            UnityEngine.Debug.Log("save " + SaveDamage);
            UnityEngine.Debug.Log("real " + gun.damage);
            //set
            gun.damage *= 1.3f;
            gun.projectileSpeed *= 0.5f;
            gun.gravity = 1;
            
            
            gun.explodeNearEnemyDamage = gun.damage;
            gun.explodeNearEnemyRange = 10f;

            UnityEngine.Debug.Log("s: save " + SaveDamage);
            UnityEngine.Debug.Log("s: real " + gun.damage);

            gun.Attack(0, false, 1, 1, false);

            //reset
            gun.damage = SaveDamage;
            gun.projectileSpeed = SaveProjectileSpeed;
            gun.gravity = SaveGravity;
            gun.explodeNearEnemyDamage = SaveExplDamage;
            gun.explodeNearEnemyRange = SaveExplRange;

            
            UnityEngine.Debug.Log("[ExampleEffect] Player blocked!");
        }
    }
}

