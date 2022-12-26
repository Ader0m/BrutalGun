using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class LongBarrel : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Long Barrel",
            Description = "It's bigger, it's longer, oh yeah",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                 new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "ATKSPD",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };

            gun.projectileSpeed = 1.25f;
            gun.attackSpeedMultiplier = 0.85f;
        }
    }
}
