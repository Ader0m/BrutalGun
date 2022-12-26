using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class SpiritConnection : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Spirit Connection",
            Description = "You feel the life of your enemy filling you. You want more and more",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "LifeSteal",
                    amount = "+30%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Accuracy",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module };

            statModifiers.lifeSteal = 0.3f;
            gun.multiplySpread = 1.1f;
        }
    }
}
