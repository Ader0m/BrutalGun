using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class LightArmor : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Light Armor",
            Description = "Trim here, tear off there... Oh, did something fall off?",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Ammo",
                    amount = "-5",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };
            cardInfo.allowMultiple = true;

            statModifiers.movementSpeed = 1.25f;
            gun.ammo = -5;
        }
    }
}
