using UnboundLib.Cards;
using UnityEngine;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using BrutalGun;
using ModsPlus;
using System.Runtime.CompilerServices;
using Photon.Pun.UtilityScripts;
using System.Xml.Linq;
using HarmonyLib;
using static ObjectsToSpawn;
using System.Numerics;
using ModdingUtils.MonoBehaviours;
using System.Reflection.Emit;

namespace BrutalGun.Cards
{
    public class Grenade : CustomEffectCard<GrenadeHundler>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Granade",
            Description = "Send a cake! Or a grenade! After all, the cake is a lie...",
            ModName = BrutalGun.BrutalGunMain.MOD_INITIALS,
            OwnerOnly = false,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.ColdBlue,
            Stats = new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Work with block",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            }
        };

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { MyCategories.Module };                    
        }
    }

    public class GrenadeHundler : CardEffect
    {
        private GameObject _addToProjectile;
        private GameObject _effect;
        private Explosion _explosion;

        GrenadeHundler()
        {
            (GameObject addToProjectile, GameObject effect, Explosion explosion) = BrutalGunMain.LoadExplosionElements();
            _addToProjectile = addToProjectile;
            _effect = effect;
            _explosion = explosion;
        }

        public override void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {           
            //setExplosionStats
            _explosion.damage = Mathf.Clamp((gun.damage * 1.5f) * 55, 40, 75);
            _explosion.range = Mathf.Clamp((gun.damage * 15), 10, 15);
            _explosion.force = 2000;

            UnityEngine.Debug.Log((gun.damage * 1.5f) * 55 + " " + _explosion.damage);
            UnityEngine.Debug.Log((gun.damage * 15) + " " + _explosion.range);

            ObjectsToSpawn[] obj = { new ObjectsToSpawn
            {
                AddToProjectile = _addToProjectile,
                direction = ObjectsToSpawn.Direction.forward,
                effect = _effect,
                normalOffset = 0.1f,
                scaleFromDamage = 1f,
                scaleStackM = 1f,
                scaleStacks = true,
                spawnAsChild = false,
                spawnOn = ObjectsToSpawn.SpawnOn.all,
                stacks = 0,
                stickToAllTargets = false,
                stickToBigTargets = false,
                zeroZ = false
            } };

            //launch grenade
            player.gameObject.AddComponent<GrenadeEffect>().Init(0.5f, 0.5f, 1, obj.ToList());
        }
    }

    public class GrenadeEffect: ReversibleEffect
    {
        private List<ObjectsToSpawn> _objectsToSpawn;
        private float _projectileSpeedM, _bulletDamageM, _gravityValue;

        /// <summary>
        /// GravityM - The value that should be obtained!
        /// </summary>
        /// <param name="bulletDamageM"></param>
        /// <param name="projectileSpeedM"></param>
        /// <param name="gravityValue"> The value that should be obtained! </param>
        /// <param name="objectsToSpawn"></param>
        public void Init(float bulletDamageM, float projectileSpeedM, float gravityValue, List<ObjectsToSpawn> objectsToSpawn)
        {
            _bulletDamageM = bulletDamageM;
            _projectileSpeedM = projectileSpeedM;
            _gravityValue = gravityValue;
            _objectsToSpawn = objectsToSpawn;
        }

        public override void OnStart()
        {
            gunStatModifier.bulletDamageMultiplier_mult = _bulletDamageM;
            gunStatModifier.projectileSpeed_mult = _projectileSpeedM;
            gunStatModifier.gravity_add = -(gun.gravity - _gravityValue);
            gunStatModifier.spread_add = -gun.spread;

            gunStatModifier.objectsToSpawn_add = _objectsToSpawn;
        }

        public override void OnUpdate()
        {
            gun.Attack(0, false, 1, 1, false);
            Destroy(this);
        }
    }
}

