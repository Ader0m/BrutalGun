using BepInEx;
using BrutalGun.Cards;
using BrutalGun.Cards.Modules_rare;
using BrutalGun.Cards.VimpireCard.Rare;
using BrutalGun.Utils;
using HarmonyLib;
using ModsPlus;
using Photon.Pun;
using System;
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
        public CardBarController CardBarController;
        private List<CardInfo> _startCardList;
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

            //StartCoroutine(debug());
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
            if (CardBarController.CardBarLengthDict != null)
                CardBarController.Restore();
            if (VampireManager.PlayerStatsDict != null)
                VampireManager.Restore();
            
            _firstPick = true;

            yield break;
        }

        private void BuildCards()
        {
            //Modules_common           
            CardContainer.RegisterCard<BatteriesEnergizer>();
            CardContainer.RegisterCard<ExtendedMag>();
            CardContainer.RegisterCard<IFAK>();
            CardContainer.RegisterCard<Laser>();
            CardContainer.RegisterCard<QuickDrop>();
            CardContainer.RegisterCard<RocketJump>();
            CardContainer.RegisterCard<StrangePill>();
            CardContainer.RegisterCard<TitaniumParts>();
            //Modules_uncommon
            CardContainer.RegisterCard<AmmoXL>();
            CardContainer.RegisterCard<BigBullet>();
            CardContainer.RegisterCard<Grenade>();
            CardContainer.RegisterCard<HummerBullet>();
            CardContainer.RegisterCard<LightArmor>();
            CardContainer.RegisterCard<LightBolt>();
            CardContainer.RegisterCard<LongBarrel>();
            CardContainer.RegisterCard<ScopeX2>();
            CardContainer.RegisterCard<SecondArmorPlate>();
            CardContainer.RegisterCard<SpiritConnection>();
            //Modules_rare
            CardContainer.RegisterCard<ScopeX8>();
            CardContainer.RegisterCard<ArmoredSuit>();
            CardContainer.RegisterCard<Berserker>();
            CardContainer.RegisterCard<APBullet>();
            //Weapon_common
            CardContainer.RegisterCard<FiveSeven>();
            CardContainer.RegisterCard<Glock>();
            CardContainer.RegisterCard<SawnOff>();
            //Weapon_uncommon
            CardContainer.RegisterCard<Famas>();
            CardContainer.RegisterCard<Mosin>();
            CardContainer.RegisterCard<Nova>();
            CardContainer.RegisterCard<P90>();
            //Weapon_rare
            CardContainer.RegisterCard<AK74>();
            CardContainer.RegisterCard<DesertEagle>();
            CardContainer.RegisterCard<M200>();
            CardContainer.RegisterCard<Vampire>();
            CardContainer.RegisterCard<Winchester>();

            //VimpireCard
            //Common
            CardContainer.RegisterCard<ArmsReinforcement>();
            CardContainer.RegisterCard<BatWatching>();
            CardContainer.RegisterCard<BodyReinforcement>();
            CardContainer.RegisterCard<LegsReinforcement>();
            CardContainer.RegisterCard<TasteBlood>();
            //Uncommon
            CardContainer.RegisterCard<AuraGreat>();
            CardContainer.RegisterCard<HeavyPunch>();
            CardContainer.RegisterCard<LongPunch>();
            //Rare
            CardContainer.RegisterCard<Dash>();
            CardContainer.RegisterCard<DevilMantle>();
            CardContainer.RegisterCard<SteelClaws>();

            //SupportCard
            CardContainer.RegisterCard<DevilMantleCurse>();

            CardContainer.RegisterCard<AuraGreatCurse>();

            //StartCards
            CustomCard.BuildCard<Macarov>(cardInfo =>
            {
                _startCardList.Add(cardInfo);
                ModdingUtils.Utils.Cards.instance.AddHiddenCard(cardInfo);
            });
        }

        public void StartVampireCoroutineRedirect(Player player)
        {
            StartCoroutine(CardBarController.AdaptateHumanToVampire(player));
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
    }
}
