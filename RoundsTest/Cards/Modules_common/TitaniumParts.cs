using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class TitaniumParts : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Titanium Parts",
            Description = "Hey, you! You got an inheritance from a secret uncle. We decided you needed it more.",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };
            cardInfo.allowMultiple = true;

            statModifiers.movementSpeed = 1.15f;
        }
    }
}
