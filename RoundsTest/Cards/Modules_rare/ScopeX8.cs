using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class ScopeX8 : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Scope X8",
            Description = "Check the Wind. Iron. The natal chart. Well done, you can shoot",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Accuracy",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "ATKSPD",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Speed",
                    amount = "-10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };

            gun.attackSpeedMultiplier = 1.2f;
            gun.multiplySpread = 0.5f;
            statModifiers.movementSpeed = 0.9f;
        }
    }
}
