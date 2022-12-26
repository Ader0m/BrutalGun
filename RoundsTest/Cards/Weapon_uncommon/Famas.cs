using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class Famas : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Famas",
            Description = "3 bullet one time, do 3 hole in enemy a**",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "DMG",
                    amount = "28",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "2.5",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "9",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload time",
                    amount = "1.7s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Weapon };
            cardInfo.allowMultiple = false;
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //show stats
            gun.damage = 0.5f;
            gun.attackSpeed = 0.4f;
            gunAmmo.maxAmmo = 9;
            gunAmmo.reloadTime = 1.7f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 2.5f;
            gun.gravity = 0.6f;
            gun.spread = 0.07f;

            //burst
            gun.timeBetweenBullets = 0.15f;
            gun.bursts = 3;
            gun.numberOfProjectiles = 1;
        }

        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //show stats
            gun.damage = 1f;
            gun.attackSpeed = 1f;
            gunAmmo.maxAmmo = 3;
            gunAmmo.reloadTimeAdd = 1f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 1f;
            gun.gravity = 1f;
            gun.spread = 0f;

            //burst
            gun.bursts = 0;
            gun.numberOfProjectiles = 1;
            gun.timeBetweenBullets = 0f;
        }
    }
}
