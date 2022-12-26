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
using ModdingUtils.MonoBehaviours;
using System.Collections;

namespace BrutalGun.Cards
{
    public class HeavyPunch : CustomEffectCard<StunEffect>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Heavy Punch",
            Description = "Instead of a lot of quick punches, you punch one",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Knockback",
                    amount = "+300%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Stun",
                    amount = "0.1s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Vampire };

            gun.knockback = 3f;      
        }
    }

    public class StunEffect : CardEffect
    {
        private Player _enemy;
        private float _stanDuration = 0.1f;


        public override void OnUpgradeCard()
        {
            _stanDuration *= 3;
        }

        public override IEnumerator OnBulletHitCoroutine(GameObject projectile, HitInfo hit)
        {
            if (hit.collider.gameObject.TryGetComponent<Player>(out _enemy))
            {
                yield return new WaitForSeconds(0.2f);
                _enemy.data.stunHandler.AddStun(_stanDuration);
            }
        }
    }
}
