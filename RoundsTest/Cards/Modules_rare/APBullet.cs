using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class APBullet : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Armor-piercing bullet",
            Description = "Shakes the bodies of enemies as well as your hands",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                 new CardInfoStat()
                {
                    positive = true,
                    stat = "DMG",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Accuracy",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };

            gun.bulletDamageMultiplier = 1.20f;
            gun.multiplySpread = 1.2f;
        }
    }
}
