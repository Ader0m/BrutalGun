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
            Description = "A sniper rifle that is too heavy to shoot accurately on the run",
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
            gun.spread = 0.55f;          
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
        private GameObject? _parent;
        private GameObject? _upRotator;
        private GameObject? _downRotator;
        private float _difirent = 0.25f;
        private float _maxSpread;
        private float _spreadCandidat;
        private bool _spreadLoop;
        private bool _afterPick;


        protected override void Start()
        {
            base.Start();
            _maxSpread = gun.spread;
            _spreadLoop = true;
            _afterPick = false;
            InitSpreadLines();
        }

        public override IEnumerator OnBattleStart(IGameModeHandler gameModeHandler)
        {
            if (_afterPick)
            {
                _maxSpread = gun.spread * gun.multiplySpread;            
                _afterPick = false;
                _spreadLoop = true;               
            }           

            yield break;
        }

        public override IEnumerator OnPickPhaseStart(IGameModeHandler gameModeHandler)
        {
            _spreadLoop = false;
            gun.spread = _maxSpread;
            _afterPick = true;

            yield return null;
        }

        private void Update()
        {        
            if (_spreadLoop)
            {
                drawSpread();
                
                if (gun.spread == _maxSpread)
                {
                    UnityEngine.Debug.Log(_maxSpread);
                }
                
                if (player.data.input.direction.magnitude > 0f)
                {
                    _spreadCandidat = gun.spread + _difirent * TimeHandler.deltaTime;
                    gun.spread = Mathf.Clamp(_spreadCandidat, 0f, _maxSpread);
                }
                else
                {
                    _spreadCandidat = gun.spread - _difirent * 1.25f * TimeHandler.deltaTime;
                    gun.spread = Mathf.Clamp(_spreadCandidat, 0f, _maxSpread);
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_parent != null) 
                Destroy(_parent);          
        }

        private void InitSpreadLines()
        {
            _parent = new GameObject("SpreadLineParent");
            _upRotator = new GameObject("UpRotator");
            _downRotator = new GameObject("DownRotator");
            Transform transformParent = gun.gameObject.transform.Find("Spring/Barrel");

            _parent.transform.parent = transformParent;
            _parent.transform.position = Vector3.zero;
            _parent.transform.localPosition = Vector3.zero;

            _upRotator.transform.parent = _parent.transform;
            _upRotator.transform.position = Vector3.zero;
            _upRotator.transform.localPosition = new Vector3(0.4f, 0, 0);

            _downRotator.transform.parent = _parent.transform;
            _downRotator.transform.position = Vector3.zero;
            _downRotator.transform.localPosition = new Vector3(0.3f, 0, 0);
           
            GameObject upLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
            upLine.name = "M200UpSpreadLine";
            upLine.GetComponent<MeshRenderer>().material = BrutalGunMain.Instance.Assets.LoadAsset<Material>("SpreadLine");                       
            upLine.transform.parent = _upRotator.transform;
            upLine.transform.localScale = new Vector3(0.1f, 0.8f, 0.1f);
            upLine.transform.position = Vector3.zero;
            upLine.transform.localPosition = new Vector3(0, -0.3f, 0);

            GameObject downLine = GameObject.CreatePrimitive(PrimitiveType.Cube);
            downLine.name = "M200DownSpreadLine";
            downLine.GetComponent<MeshRenderer>().material = BrutalGunMain.Instance.Assets.LoadAsset<Material>("SpreadLine");
            downLine.transform.parent = _downRotator.transform;
            downLine.transform.localScale = new Vector3(0.1f, 0.8f, 0.1f);
            downLine.transform.position = Vector3.zero;
            downLine.transform.localPosition = new Vector3(0, -0.3f, 0);
        }

        public override void OnShoot(GameObject projectile) => gun.spread = _maxSpread;

        public override void OnJump() => gun.spread = _maxSpread;       

        public void drawSpread()
        {
            float spreadCandidat = (gun.spread / ((1f + gun.projectileSpeed * 0.5f) * 0.5f)) * 0.85f;

            if (_parent != null && _upRotator != null && _downRotator != null)
            {
                _parent.transform.position = gun.transform.Find("Spring/Barrel").position;
                _upRotator.transform.localRotation = Quaternion.Euler(0, 0, 90 + spreadCandidat * 360);
                _downRotator.transform.localRotation = Quaternion.Euler(0, 0, 90 - spreadCandidat * 360);
            }                 
        }
    }   
}
