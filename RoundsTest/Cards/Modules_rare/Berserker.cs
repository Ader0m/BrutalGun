using ModdingUtils.MonoBehaviours;
using ModsPlus;
using System.Collections;
using UnboundLib.GameModes;
using UnityEngine;

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
            cardInfo.allowMultiple = true;
        }
    }

    public class BerserkerHandler : CardEffect
    {
        private float _regenDuration = 10;

        /// <summary>
        /// Set a BerserkerUpEffect effect. Then the BerserkerUpEffect effect class will set the negative effect on its own
        /// </summary>
        /// <param name="gameModeHandler"></param>
        /// <returns></returns>
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            player.gameObject.AddComponent<BerserkerUpEffect>().Initialize(_regenDuration, 10, 1000, 10000);

            yield break;
        }

        public override void OnUpgradeCard()
        {
            _regenDuration += 5;
        }
    }

    public class BerserkerUpEffect : ReversibleEffect
    {
        private float _regenDuration, _regen, _damageDuration, _sumDamage;

        /// <summary>
        /// finally damage = damage / damage duration
        /// </summary>
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

            player.data.healthHandler.TakeDamageOverTime(Vector2.up * _sumDamage, Vector2.zero, _damageDuration, 1f, Color.red);
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

