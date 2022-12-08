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
using System.Collections;

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
        private int _countGrenade = 1;


        GrenadeHundler()
        {
            (GameObject addToProjectile, GameObject effect, Explosion explosion) = BrutalTools.LoadExplosionElements();
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
            player.gameObject.AddComponent<GrenadeEffect>().Init(obj.ToList(), 0.5f, 0.5f, 1, _countGrenade);
        }

        public override void OnUpgradeCard()
        {
            _countGrenade += 1;
        }
    }

    public class GrenadeEffect : ReversibleEffect
    {
        private List<ObjectsToSpawn> _objectsToSpawn;
        private float _projectileSpeedM, _bulletDamageM, _gravityValue;
        private int _countGrenade;

        /// <summary>
        /// GravityM - The value that should be obtained!
        /// </summary>
        /// <param name="gravityValue"> The value that should be obtained! </param>
        public void Init(List<ObjectsToSpawn> objectsToSpawn, float bulletDamageM, float projectileSpeedM, float gravityValue, int countGrenade)
        {
            _objectsToSpawn = objectsToSpawn;
            _bulletDamageM = bulletDamageM;
            _projectileSpeedM = projectileSpeedM;
            _gravityValue = gravityValue;
            _countGrenade = countGrenade;
        }

        public override void OnStart()
        {
            gunStatModifier.bulletDamageMultiplier_mult = _bulletDamageM;
            gunStatModifier.projectileSpeed_mult = _projectileSpeedM;
            gunStatModifier.gravity_add = -(gun.gravity - _gravityValue);
            gunStatModifier.spread_add = -gun.spread;
            gunStatModifier.destroyBulletAfter_mult *= 0;

            gunStatModifier.objectsToSpawn_add = _objectsToSpawn;

            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            //wait apply modificators
            yield return null;


            for (int i = 0; i < _countGrenade; i++)
            {
                gun.Attack(0, true, 1, 1, false);
                yield return new WaitForSeconds(0.15f);
            }
            Destroy(this);
        }
    }
}

