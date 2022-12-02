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

namespace BrutalGun.Cards
{
    public class Grenade : CustomEffectCard<GrenadeEffect>
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

    public class GrenadeEffect : CardEffect
    {
        public override void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            //saveData
            float SaveDamage = gun.damage;
            float SaveProjectileSpeed = gun.projectileSpeed;
            float SaveGravity = gun.gravity;
            ObjectsToSpawn[] objectsToSpawn = player.data.weaponHandler.gun.objectsToSpawn;

            //getExplosion
            GameObject explosiveBullet = (GameObject)Resources.Load("0 cards/Explosive bullet");
            Gun explosiveGun = explosiveBullet.GetComponent<Gun>();


            GameObject A_ExplosionSpark = explosiveGun.objectsToSpawn[0].AddToProjectile;
            GameObject explosionCustom = Instantiate(explosiveGun.objectsToSpawn[0].effect);
            explosionCustom.transform.position = new UnityEngine.Vector3(1000, 0, 0);
            explosionCustom.hideFlags = HideFlags.HideAndDontSave;
            explosionCustom.name = "customExpl";
            DestroyImmediate(explosionCustom.GetComponent<RemoveAfterSeconds>());
            Explosion explosion = explosionCustom.GetComponent<Explosion>();

            //setStats
            gun.projectileSpeed *= 0.5f;
            gun.damage *= 1.5f;
            gun.gravity = 1f;

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

            player.data.weaponHandler.gun.objectsToSpawn.Concat(obj).ToArray();
                

            gun.Attack(0, false, 1, 1, false);

            //resetStats
            gun.damage = SaveDamage;
            gun.projectileSpeed = SaveProjectileSpeed;
            gun.gravity = SaveGravity;
            player.data.weaponHandler.gun.objectsToSpawn.Except(obj).ToArray();

            UnityEngine.Debug.Log("[ExampleEffect] Player use grenade!");
        }
    }
}

