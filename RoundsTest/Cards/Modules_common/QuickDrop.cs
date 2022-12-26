using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class QuickDrop : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Quick Drop",
            Description = "Why you save empty mag? Drop they!",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload time",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };

            gun.reloadTime = 0.8f;
        }
    }
}
