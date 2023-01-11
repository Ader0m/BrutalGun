using BrutalGun.Utils;
using ModdingUtils.MonoBehaviours;
using ModsPlus;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BrutalGun.Cards
{
    public class Grenade : CustomEffectCard<GrenadeHundler>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "Grenade",
            Description = "Send a cake! Or a grenade! After all, the cake is a lie...",
            ModName = BrutalGunMain.MOD_INITIALS,
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
            cardInfo.categories = new CardCategory[] { MyCategories.Module, MyCategories.Human };
            cardInfo.allowMultiple = true;
        }
    }

    public class GrenadeHundler : CardEffect
    {
        private GameObject _addToProjectile;
        private GameObject _effect;
        private Explosion _explosion;
        private int _countGrenade;


        GrenadeHundler()
        {
            _countGrenade = 1;
            (_addToProjectile, _effect, _explosion) = BrutalTools.LoadExplosionElements("Grenade");          
        }

        public override void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            //setExplosionStats
            _explosion.damage = Mathf.Clamp((gun.damage * 1.5f) * 55, 45, 75);
            _explosion.range = Mathf.Clamp((gun.damage * 15), 12, 20);
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
            if (!gameObject.TryGetComponent<GrenadeEffect>(out _))
                player.gameObject.AddComponent<GrenadeEffect>().Init(obj.ToList(), 1, _countGrenade);
        }

        public override void OnUpgradeCard() => _countGrenade += 1;       
    }

    public class GrenadeEffect : ReversibleEffect
    {
        private List<ObjectsToSpawn> _objectsToSpawn;
        private float  _gravityValue;
        private int _countGrenade;

        /// <summary>
        /// GravityM - The value that should be obtained!
        /// </summary>
        /// <param name="gravityValue"> The value that should be obtained! </param>
        public void Init(List<ObjectsToSpawn> objectsToSpawn, float gravityValue, int countGrenade)
        {
            _objectsToSpawn = objectsToSpawn;
            _gravityValue = gravityValue;
            _countGrenade = countGrenade;
        }

        public override void OnStart()
        {           
            gunStatModifier.damage_add = 0.55f - gun.damage;
            gunStatModifier.numberOfProjectiles_add = 1 - gun.numberOfProjectiles;
            gunStatModifier.bursts_add = 1 - gun.bursts;
            gunStatModifier.destroyBulletAfter_add -= gun.destroyBulletAfter;
            gunStatModifier.gravity_add = _gravityValue - gun.gravity;
            gunStatModifier.spread_add -= gun.spread;

            gunStatModifier.bulletDamageMultiplier_mult = 0.5f;
            gunStatModifier.projectileSpeed_add = Mathf.Clamp(gun.projectileSpeed * 0.3f, 0.5f, 1f) - gun.projectileSpeed;
            gunStatModifier.objectsToSpawn_add = _objectsToSpawn;

            StartCoroutine(Shoot());            
        }

        private IEnumerator Shoot()
        {
            //wait apply modificators
            yield return null;

            gun.enabled = false;

            for (int i = 0; i < _countGrenade; i++)
            {                              
                gun.Attack(0, true, 1, 1, false);

                if (i + 1 == _countGrenade)
                {
                    break;
                }
                yield return new WaitForSeconds(0.15f); //сделать адаптивной
            }

            gun.enabled = true;

            Destroy(this);
        }
    }
}

