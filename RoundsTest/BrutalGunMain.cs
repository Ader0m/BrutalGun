using BepInEx;
using BrutalGun.Cards;
using BrutalGun.Cards.Modules_rare;
using BrutalGun.Cards.VimpireCard.Rare;
using HarmonyLib;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;

namespace BrutalGun
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    //[BepInDependency("com.willis.rounds.cardsplus", BepInDependency.DependencyFlags.HardDependency)]

    // Declares our mod to Bepin
    [BepInPlugin(_MOD_ID, _MOD_NAME, VERSION)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]

    public class BrutalGunMain : BaseUnityPlugin
    {
        #region Singletone

        public static BrutalGunMain Instance { get; private set; }

        #endregion

        #region Default Const

        private const string _MOD_ID = "com.aderom.rounds.RoundsTest";
        private const string _MOD_NAME = "BrutalGun";
        public const string VERSION = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string MOD_INITIALS = "BGun";

        #endregion

        public Player[] PlayersMass;
        private List<CardInfo> _startCardList;
        public CardBarController CardBarController;
        private bool _firstPick;

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(_MOD_ID);
            harmony.PatchAll();
        }

        void Start()
        {
            Instance = this;
            _firstPick = true;
            _startCardList = new List<CardInfo>();
            BuildCards();
            CreateManagers();
            
            
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
            GameModeManager.AddHook(GameModeHooks.HookPickStart, FirstPickStart);
            GameModeManager.AddHook(GameModeHooks.HookGameEnd, ResetData);
            GameModeManager.AddHook(GameModeHooks.HookInitStart, ResetData);

            StartCoroutine(debug());
        }       

        public IEnumerator debug()
        {
            while (true)
            {
                try
                {
                    foreach (Player player in PlayersMass)
                    {
                        UnityEngine.Debug.Log("player " + player.playerID + " " + player.data.health);
                    }
                }
                catch
                {
                    UnityEngine.Debug.Log("error");
                }

                yield return new WaitForSeconds(1f);
            }                      
        }
        private void CreateManagers()
        {
            CardBarController = new CardBarController();

        }

        IEnumerator FirstPickStart(IGameModeHandler arg)
        {
            if (_firstPick)
            {
                _firstPick = false;
                PlayersMass = PlayerManager.instance.players.Where((person) => !ModdingUtils.AIMinion.Extensions.CharacterDataExtension.GetAdditionalData(person.data).isAIMinion).ToArray();
                PlayerSettings.SetStartStats(_startCardList);
                foreach (Player player in PlayersMass)
                {
                    PickCardController.InitPlayer(player);
                }
            }         

            yield break; 
        }

        IEnumerator PickEnd(IGameModeHandler arg)
        {
            if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode)
            {
                foreach (Player player in PlayersMass)
                {
                    yield return CardBarController.CheckCorrectCardBar(player);
                }             
            }
            else
            {
                yield break;
            }            
        }

        IEnumerator ResetData(IGameModeHandler arg)
        {
            CardBarController.Restore();
            VampireManager.Restore();
            _firstPick = true;

            yield break;
        }

        private void BuildCards()
        {
            //Modules_common
            CustomCard.BuildCard<BatteriesEnergizer>();
            CustomCard.BuildCard<ExtendedMag>();
            CustomCard.BuildCard<IFAK>();
            CustomCard.BuildCard<Laser>();
            CustomCard.BuildCard<QuickDrop>();
            CustomCard.BuildCard<RocketJump>();
            CustomCard.BuildCard<StrangePill>();
            CustomCard.BuildCard<TitaniumParts>();
            //Modules_uncommon
            CustomCard.BuildCard<AmmoXL>();
            CustomCard.BuildCard<BigBullet>();
            CustomCard.BuildCard<Grenade>();
            CustomCard.BuildCard<HummerBullet>();
            CustomCard.BuildCard<LightArmor>();
            CustomCard.BuildCard<LightBolt>();
            CustomCard.BuildCard<LongBarrel>();
            CustomCard.BuildCard<ScopeX2>();
            CustomCard.BuildCard<SecondArmorPlate>();
            CustomCard.BuildCard<SpiritConnection>();
            //Modules_rare
            CustomCard.BuildCard<ScopeX8>();
            CustomCard.BuildCard<ArmoredSuit>();
            CustomCard.BuildCard<Berserker>();
            CustomCard.BuildCard<APBullet>();
            //Weapon_common
            CustomCard.BuildCard<FiveSeven>();
            CustomCard.BuildCard<Glock>();
            CustomCard.BuildCard<SawnOff>();
            //Weapon_uncommon
            CustomCard.BuildCard<Famas>();
            CustomCard.BuildCard<Mosin>();
            CustomCard.BuildCard<Nova>();
            CustomCard.BuildCard<P90>();
            //Weapon_rare
            CustomCard.BuildCard<AK74>();
            CustomCard.BuildCard<DesertEagle>();
            CustomCard.BuildCard<Vampire>();
            CustomCard.BuildCard<Winchester>();

            //VimpireCard
            //Common
            CustomCard.BuildCard<ArmsReinforcement>();
            CustomCard.BuildCard<BatWatching>();
            CustomCard.BuildCard<BodyReinforcement>();
            CustomCard.BuildCard<LegsReinforcement>();
            CustomCard.BuildCard<TasteBlood>();
            //Uncommon
            CustomCard.BuildCard<AuraGreat>();
            CustomCard.BuildCard<HeavyPunch>();
            CustomCard.BuildCard<LongPunch>();
            //Rare
            CustomCard.BuildCard<Dash>();
            CustomCard.BuildCard<DevilMantle>();
            CustomCard.BuildCard<SteelClaws>();
         
            //SupportCard
            CustomCard.BuildCard<DevilMantleCurse>(cardInfo =>
            {
                SupportCardContainer.DevilMantleCurse = cardInfo;
                ModdingUtils.Utils.Cards.instance.AddHiddenCard(cardInfo);
            });

            CustomCard.BuildCard<AuraGreatCurse>(cardInfo =>
            {
                SupportCardContainer.AuraGreatCurse = cardInfo;
                ModdingUtils.Utils.Cards.instance.AddHiddenCard(cardInfo);
            });

            //StartCards
            CustomCard.BuildCard<Macarov>(cardInfo =>
            {
                _startCardList.Add(cardInfo);
                ModdingUtils.Utils.Cards.instance.AddHiddenCard(cardInfo);
            });
        }
    }
}
