using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class BatteriesEnergizer : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Batteries Energizer",
            Description = "More powerful batteries for your shi(t)eld",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Block cooldown",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module };
            cardInfo.allowMultiple = true;

            block.cdMultiplier = 0.8f;
        }
    }
}