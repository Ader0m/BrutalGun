using BrutalGun;
using ModsPlus;
using UnityEngine;

namespace BrutalGun.Cards
{
    public class DesertEagle : CustomEffectCard<DesertEagleController>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Desert Eagle",
            Description = "There's something wrong with this gun",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "DMG",
                    amount = "40",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "2,8",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "AMMO",
                    amount = "5",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload Time",
                    amount = "2s",
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
            gun.damage = 0.72f;
            gun.attackSpeed = 0.35f;
            gunAmmo.maxAmmo = 5;
            gunAmmo.reloadTime = 2f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 4f;
            gun.gravity = 0f;
            gun.spread = 0f;
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
            gun.spread = 0f;
        }
    }

    public class DesertEagleController: CardEffect
    {
        public override void OnBlockRecharge()
        {
            player.data.weaponHandler.gun.spread = 0f;
            player.data.weaponHandler.gun.gravity = 0f;
            player.data.weaponHandler.gun.projectileSpeed = 4f;
        }

        public override void OnBlock(BlockTrigger.BlockTriggerType blockTriggerType)
        {
            player.data.weaponHandler.gun.spread = 0.4f;
            player.data.weaponHandler.gun.gravity = 1f;
            player.data.weaponHandler.gun.projectileSpeed = 2f;
        }

        public override void OnShoot(GameObject projectile)
        {
            player.data.block.DoBlockAtPosition(true);
        }
    }
}
