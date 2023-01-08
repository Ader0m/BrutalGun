using BrutalGun;
using ModsPlus;

namespace BrutalGun.Cards
{
    public class SawnOff : SimpleCard
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "SawnOff",
            Description = "Very powerful short barrel",
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
                    amount = "50x3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "20",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "AMMO",
                    amount = "2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload Time",
                    amount = "1.2s",
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
            gun.damage = 0.91f;
            gun.attackSpeed = 0.05f;
            gunAmmo.maxAmmo = 6;
            gun.reloadTime = 1.2f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 1.2f;
            gun.gravity = 0.6f;
            gun.spread = 0.2f;

            //shotgun
            gun.bursts = 1;
            gun.numberOfProjectiles = 3;
            gun.destroyBulletAfter = 0.25f;
            gun.damageAfterDistanceMultiplier = 0.4f;
        }

        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //show stats
            gun.damage = 1f;
            gun.attackSpeed = 1f;
            gunAmmo.maxAmmo = 3;
            gun.reloadTime = 1f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 1f;
            gun.gravity = 1f;
            gun.spread = 0f;

            //shotgun
            gun.bursts = 0;
            gun.numberOfProjectiles = 1;
            gun.destroyBulletAfter = 0f;
            gun.damageAfterDistanceMultiplier = 0f;
        }
    }
}
