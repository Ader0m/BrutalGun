using ModsPlus;

namespace BrutalGun.Cards
{
    public class DevilMantleCurse : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Devil Mantle Curse",
            Description = "You are the next component for this mantle",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Accuracy",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module };
            gun.multiplySpread = 1.2f;
        }

        public override bool GetEnabled()
        {
            return false;
        }
    }
}
