using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class AmmoXL : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "AmmoXL",
            Description = "Make you magazine big and fat",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullets",
                    amount = "+8",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "+2.5s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };
            cardInfo.allowMultiple = true;

            gun.reloadTimeAdd = 2.5f;
            gun.ammo = 8;
        }
    }
}
