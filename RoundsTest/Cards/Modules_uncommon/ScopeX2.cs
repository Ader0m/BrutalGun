using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class ScopeX2 : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Scope X2",
            Description = "Be ready to shoot him and go quietly",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Accuracy",
                    amount = "+25%",
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
            cardInfo.allowMultiple = true;

            gun.multiplySpread = 0.75f;
            statModifiers.movementSpeed = 0.9f;
        }
    }
}
