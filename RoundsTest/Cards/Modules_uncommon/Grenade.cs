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
        public override void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            //getExplosion
            (GameObject A_ExplosionSpark, GameObject explosionCustom, Explosion explosion) = BrutalGunMain.LoadExplosionElements();
            
            //setExplosionStats
            explosion.damage = 10f;
            explosion.range = 1f;
            explosion.force = 40;

            ObjectsToSpawn[] obj = {new ObjectsToSpawn
            {
                AddToProjectile = A_ExplosionSpark,
                effect = explosionCustom,
                normalOffset = 0.1f,
                scaleFromDamage = 1f,
                scaleStackM = 0.7f,
                scaleStacks = true,
                spawnOn = ObjectsToSpawn.SpawnOn.all,
                direction = ObjectsToSpawn.Direction.forward
            } };

            //lounchGrenade
            player.gameObject.AddComponent<GrenadeEffect>().Init(1.5f, 0.5f, 1, obj);

            UnityEngine.Debug.Log("[ExampleEffect] Player use grenade!");
        }
    }

    public class GrenadeEffect: ReversibleEffect
    {
        private ObjectsToSpawn[] _objectsTospawn;
        private float _projectileSpeed, _bulletDamageMultiplyer, _gravityM;

        /// <summary>
        /// GravityM - The value that should be obtained!
        /// </summary>
        /// <param name="bulletDamageMultiplyer"></param>
        /// <param name="projectileSpeed"></param>
        /// <param name="gravityM"> The value that should be obtained! </param>
        /// <param name="objectsToSpawn"></param>
        public void Init(float bulletDamageMultiplyer, float projectileSpeed, float gravityM, ObjectsToSpawn[] objectsToSpawn)
        {
            _bulletDamageMultiplyer = bulletDamageMultiplyer;
            _projectileSpeed = projectileSpeed;
            _gravityM = gravityM;
            _objectsTospawn = objectsToSpawn;
        }

        public override void OnStart()
        {
            gunStatModifier.bulletDamageMultiplier_mult = _bulletDamageMultiplyer;
            gunStatModifier.projectileSpeed_mult = _projectileSpeed;
            gunStatModifier.gravity_add = -(gun.gravity - _gravityM);

            player.data.weaponHandler.gun.objectsToSpawn.Concat(_objectsTospawn).ToArray();                     
        }

        public override void OnUpdate()
        {
            gun.Attack(0, false, 1, 1, false);
            Destroy(this);
        }

        public override void OnOnDestroy() 
        {          
            player.data.weaponHandler.gun.objectsToSpawn.Except(_objectsTospawn).ToArray();
        }
    }
}

