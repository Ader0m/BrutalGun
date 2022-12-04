using UnboundLib.Cards;
using UnityEngine;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using ModsPlus;
using System.Runtime.CompilerServices;
using Photon.Pun.UtilityScripts;
using System.Xml.Linq;
using UnboundLib.GameModes;
using System.Collections;
using System.Reflection;
using System.Numerics;
using ModdingUtils.MonoBehaviours;
using System.Text.RegularExpressions;
using ModdingUtils.Extensions;

namespace BrutalGun.Cards.Modules_rare
{
    public class Berserker : CustomEffectCard<BerserkerHandler>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Berserker",
            Description = "A deathbed battle cocktail. Fulfill your goal and die with honor!",
            ModName = BrutalGunMain.MOD_INITIALS,
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

    public class BerserkerHandler : CardEffect
    {

        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            float spread = gun.spread;
            float speed = characterStats.movementSpeed;

            player.gameObject.AddComponent<BerserkerUpEffect>().Initialize(10, 10, 1000, 10000);
            yield break;
        }
    }

    public class BerserkerUpEffect : ReversibleEffect
    {
        private float _regenDuration, _regen, _damageDuration, _sumDamage;

        /// <summary>
        /// finally damage = damage / damage duration
        /// </summary>
        /// <param name="regenDuration"></param>
        /// <param name="regen"></param>
        /// <param name="damageDuration"></param>
        /// <param name="damage"></param>
        public void Initialize(float regenDuration, float regen, float damageDuration, float damage)
        {
            _regenDuration = regenDuration;
            _regen = regen;
            _damageDuration = damageDuration;
            _sumDamage = damage;
        }

        public override void OnStart()
        {
            player.data.healthHandler.regeneration += _regen;
            gunStatModifier.spread_mult = 0.8f;
            characterStatModifiersModifier.movementSpeed_mult = 1.2f;
            Destroy(this, _regenDuration);
        }

        public override void OnOnDestroy()
        {
            player.data.healthHandler.regeneration -= _regen;
            player.gameObject.AddComponent<BerserkerDownEffect>().Initialize(_damageDuration, _sumDamage);
        }
    }

    public class BerserkerDownEffect : ReversibleEffect
    {
        private float _damageDuration, _sumDamage;

        public void Initialize(float damageDuration, float damage)
        {
            _damageDuration = damageDuration;
            _sumDamage = damage;
        }

        public override void OnStart()
        {
            gunStatModifier.spread_mult = 1.2f;
            characterStatModifiersModifier.movementSpeed_mult = 0.8f;

            player.data.healthHandler.TakeDamageOverTime(UnityEngine.Vector2.up * _sumDamage, UnityEngine.Vector2.zero, _damageDuration, 1f, Color.red);
        }

        public override void OnUpdate()
        {
            if (player.data.dead)
            {
                Destroy(this);
            }
        }
    }
}

