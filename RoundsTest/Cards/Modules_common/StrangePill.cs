using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class StrangePill : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Strange Pill",
            Description = "The world seems slower",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module };
            cardInfo.allowMultiple = true;

            gun.attackSpeed = 1.1f;
            statModifiers.movementSpeed = 1.1f;
        }
    }
}
