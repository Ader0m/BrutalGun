using ModsPlus;

namespace BrutalGun.Cards
{
    public class AuraGreatCurse : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Aura Great Curse",
            Description = "Resist friend this is not the end",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Speed",
                    amount = "-15%",
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
            cardInfo.categories = new CardCategory[] { MyCategories.Module };
            
            gun.attackSpeedMultiplier = 1.15f;
            statModifiers.movementSpeed = 0.85f;
        }

        public override bool GetEnabled()
        {
            return false;
        }
    }
}
