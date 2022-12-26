using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class Laser : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Laser",
            Description = "Tactical laser. Some help for handless soldier",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Accuracy",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };

            gun.multiplySpread = 0.85f;
        }
    }
}
