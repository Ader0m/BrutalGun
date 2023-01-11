using BrutalGun.Utils;
using BrutalGun.BetterCardControl;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class AuraGreat : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Aura of the Great",
            Description = "Your mere presence affects the minds of the enemy",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Enemy speed",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Enemy ATKSPD",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Vampire };
            cardInfo.allowMultiple = true;
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            BrutalTools.AddCurse(player, CardContainer.GetCard<AuraGreatCurse>());
        }

        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            BrutalTools.RemoveCurse(player, CardContainer.GetCard<AuraGreatCurse>());
        }
    }
}
