using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class RocketJump : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Rocket Jump",
            Description = "Small rockets will be tied to your feet. Cool right?",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Jump hight",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Count jump",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };

            statModifiers.numberOfJumps = 1;
            statModifiers.gravity = 1.2f;
        }
    }
}
