using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class Nova : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Nova",
            Description = "Powerful and relatively accurate shotgun",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Common,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "DMG",
                    amount = "17x5",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "1,42",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "AMMO",
                    amount = "3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload Time",
                    amount = "1,8s",
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
            gun.damage = 0.31f;
            gun.attackSpeed = 0.7f;
            gunAmmo.maxAmmo = 15;
            gunAmmo.reloadTime = 1.8f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 2f;
            gun.gravity = 0.6f;
            gun.spread = 0.12f;

            //shotgun
            gun.bursts = 1;
            gun.numberOfProjectiles = 5;
            gun.destroyBulletAfter = 0.21f;
        }

        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //show stats
            gun.damage = 1f;
            gun.attackSpeed = 1f;
            gunAmmo.maxAmmo = 3;
            gunAmmo.reloadTime = 1f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 1f;
            gun.gravity = 1f;

            //shotgun
            gun.bursts = 0;
            gun.numberOfProjectiles = 1;
            gun.destroyBulletAfter = 0f;
        }
    }
}
