using BrutalGun.Utils;
using ModdingUtils.MonoBehaviours;
using ModsPlus;
using System.Collections;
using UnityEngine;

namespace BrutalGun.Cards
{
    public class Vampire : CustomEffectCard<VampireHandler>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Vampire",
            Description = "Do you consider the use of weapons an unworthy occupation for an aristocrat",
            ModName = BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Rare,
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "150",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "150%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Count jump",
                    amount = "3",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "use weapons",
                    amount = "You can't",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "longer human",
                    amount = "You are no",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }         
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Weapon };

            statModifiers.health = 1.5f;
            statModifiers.movementSpeed = 1.5f;
            statModifiers.numberOfJumps = 2;

            gun.projectileColor = new Color(123, 0, 255); //purple
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {            
            //show stats
            gun.damage = 0.82f;
            gun.attackSpeed = 1f;
            gunAmmo.maxAmmo = 1;
            gunAmmo.reloadTime = 0.01f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 1.3f;
            gun.gravity = 1f;
            gun.spread = 0f;

            gun.destroyBulletAfter = 0.035f;
            gun.projectileSize = 1.25f;

            // graphics 
            Transform transformParent = player.gameObject.transform.Find("PlayerSkin/Skin_PlayerOne(Clone)");
            transformParent.gameObject.SetActive(false);
            gun.transform.Find("Spring/Ammo/Canvas").gameObject.SetActive(false);
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

            gun.destroyBulletAfter = 0f;
            gun.projectileSize = 0f;

            // graphics 
            Transform transformParent = player.gameObject.transform.Find("PlayerSkin/Skin_PlayerOne(Clone)");
            transformParent.gameObject.SetActive(true);
            gun.transform.Find("Spring/Ammo/Canvas").gameObject.SetActive(true);
        }
    }

    public class VampireHandler : CardEffect
    {
        protected override void Start()
        {
            base.Start();
            PickPhaseCardController.SetVampirePick(player);
            VampireManager.PlayerStatsDict.TryAdd(player.playerID, new VampireManager.Stats());
            BrutalGunMain.Instance.StartVampireCoroutineRedirect(player);          
        }

        public override void OnShoot(GameObject projectile)
        {           
            projectile.gameObject.AddComponent<HitEnemyEffect>().Init(player);
        }
    }

    public class HitEnemyEffect : RayHitEffect
    {        
        private Player _enemy;
        private Player _player;
        private VampireManager.Stats _vampireStats;
        private float _duration;
        private float _damage;
        private float _regen;

        public void Init(Player player)
        {
            _player = player;
            _vampireStats = VampireManager.PlayerStatsDict[player.playerID];
            _duration = _vampireStats.Duration;
            _damage = _vampireStats.Damage;
            _regen = _vampireStats.Regen;
        }

        public override HasToReturn DoHitEffect(HitInfo hit)
        {           
            if (hit.collider.gameObject.TryGetComponent<Player>(out _enemy)) 
            {
                _player.gameObject.AddComponent<VampireHealEffect>().Init(_regen, _duration);
                _enemy.data.healthHandler.TakeDamageOverTime(UnityEngine.Vector2.up * _damage * _duration, UnityEngine.Vector2.zero, _duration, 1, Color.red);

                return HasToReturn.hasToReturn;
            }

            return HasToReturn.hasToReturn;
        }
    }

    public class VampireHealEffect : ReversibleEffect
    {
        private float _regen;
        private float _duration;

        public void Init(float regen, float duration)
        {
            _regen = regen;
            _duration = duration;
        }

        public override void OnStart()
        {
            player.data.healthHandler.regeneration += _regen;
            Destroy(this, _duration);
        }

        public override void OnOnDestroy()
        {
            player.data.healthHandler.regeneration -= _regen;
        }
    }
}

