using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class WeaknessPoint : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Weakness Point",
            Description = "Instruction: Shoot well. Don't shoot badly",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "DMG",
                    amount = "+5%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };
            cardInfo.allowMultiple = true;

            gun.bulletDamageMultiplier = 1.05f;
        }
    }
}
