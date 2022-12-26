using ModsPlus;

namespace BrutalGun.Cards
{
    public class ArmsReinforcement : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Arms Reinforcement",
            Description = "Over time vampires are getting stronger",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Vampire };

            gun.attackSpeedMultiplier = 1.2f;
        }
    }
}
