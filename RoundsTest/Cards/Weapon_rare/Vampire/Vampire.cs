using UnboundLib.Cards;
using UnityEngine;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using ModsPlus;
using System.Runtime.CompilerServices;
using Photon.Pun.UtilityScripts;
using System.Xml.Linq;
using UnboundLib.GameModes;
using System.Collections;
using System.Reflection;
using System.Numerics;
using ModdingUtils.MonoBehaviours;
using System.Text.RegularExpressions;
using ModdingUtils.Extensions;
using static System.Net.Mime.MediaTypeNames;
using ModdingUtils.RoundsEffects;
using Photon.Pun.Simple;

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
                    stat = "Healing",
                    amount = "+10",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Accuracy",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "After 10 sec",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = false,
                    stat = "Healing",
                    amount = "-10",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

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
                    stat = "Accuracy",
                    amount = "-20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Weapon };

            statModifiers.health = 1.5f;
            statModifiers.movementSpeed = 1.5f;
            statModifiers.numberOfJumps = 3;
        }

        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PickCardController.SetVampirePick(player);
            VampireManager.PlayerStatsDict.TryAdd(player.playerID, new VampireManager.Stats());

            //show stats
            gun.damage = 0.82f;
            gun.attackSpeed = 1f;
            gunAmmo.maxAmmo = 1;
            gunAmmo.reloadTimeMultiplier = 0.01f;

            // hide stats
            gun.dontAllowAutoFire = true;
            gun.projectileSpeed = 0.5f;
            gun.gravity = 1f;
            gun.spread = 0f;

            gun.destroyBulletAfter = 0.1f;
        }
    }

    public class VampireHandler : CardEffect
    {
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
                _enemy.data.healthHandler.TakeDamageOverTime(UnityEngine.Vector2.up * _damage, UnityEngine.Vector2.zero, _duration, 1, Color.red);
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

