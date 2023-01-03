using ModsPlus;
using System.Collections;
using UnboundLib.GameModes;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace BrutalGun.Cards
{
    public class M200 : CustomEffectCard<M200ViewSpreadHandler>
    {
        public override CardDetails Details => new CardDetails
        {
            Title = "M200",
            Description = "Description",
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
                    amount = "110",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "ATKSPD",
                    amount = "1.4",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "5",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },

                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload time",
                    amount = "3s",
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
            gun.damage = 2f;
            gun.attackSpeed = 0.7f;
            gunAmmo.maxAmmo = 5;
            gunAmmo.reloadTime = 3f;

            // hide stats
            gun.dontAllowAutoFire = false;
            gun.projectileSpeed = 4f;
            gun.gravity = 0.65f;
            gun.spread = 0.35f;          
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
            gun.spread = 0;
        }
    }

    public class M200ViewSpreadHandler: CardEffect
    {
        private float _difirent = 0.20f;
        private float _max_spread = 0.55f;
        GameObject upLine;
        GameObject downLine;

        protected override void Start()
        {
            base.Start();

            InitAndInstantiate();

            player.data.weaponHandler.gun.spread = 0;
            StartCoroutine(SpreadCoroutine());
        }

        private void InitAndInstantiate()
        {
            //spawnSpreadLine
            GameObject SpreadObject = new GameObject();
            GameObject upLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject downLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Transform transform = player.gameObject.transform.Find("PlayerSkin").Find("Skin_PlayerOne(Clone)");
            Vector3[] vector = { Vector3.zero, (player.data.weaponHandler.gun.shootPosition.position - transform.position).normalized };

            upLine.transform.localScale = new Vector3(1, 2, 1);
            downLine.transform.localScale = new Vector3(1, 2, 1);

            GameObject Obj = Instantiate(SpreadObject, transform);
            Instantiate(upLine, Obj.transform);
            Instantiate(downLine, Obj.transform);
        }

        public override void OnShoot(GameObject projectile)
        {
            player.data.weaponHandler.gun.spread = _max_spread;
        }

        public override void OnJump()
        {
            player.data.weaponHandler.gun.spread = _max_spread;
        }

        public IEnumerator SpreadCoroutine()
        {
            float spreadCandidat = 0;

            while(true)
            {
                yield return null;

                drawSpreadLine();

                if (player.data.input.direction.magnitude > 0f)
                {
                    spreadCandidat = player.data.weaponHandler.gun.spread + _difirent * TimeHandler.deltaTime;
                    player.data.weaponHandler.gun.spread = Mathf.Clamp(spreadCandidat, 0f, _max_spread);
                }
                else
                {
                    spreadCandidat = player.data.weaponHandler.gun.spread - _difirent * 1.25f * TimeHandler.deltaTime;
                    player.data.weaponHandler.gun.spread = Mathf.Clamp(spreadCandidat, 0f, _max_spread);
                }
            }
        }
        
        public void drawSpreadLine()
        {
            UnityEngine.Debug.Log(player.data.weaponHandler.gun.spread);
            Vector3 vector = (player.data.weaponHandler.gun.shootPosition.position - transform.position).normalized;
            upLine.transform.rotation = Quaternion.Euler(vector);
            downLine.transform.rotation = Quaternion.Euler(-vector);
        }
    }


}
