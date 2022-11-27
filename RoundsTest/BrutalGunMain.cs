using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using BrutalGun.Cards;
using HarmonyLib;
using System;
using UnityEngine.Events;
using UnityEngine;
using UnboundLib.GameModes;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Photon.Pun;

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
        private List<CardInfo> startCardList;
        private CardBarManager cardBarManager;
        private PlayerSettings playerSettings;
        private bool firstPick;

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(_MOD_ID);
            harmony.PatchAll();
        }

        void Start()
        {
            Instance = this;
            firstPick = true;
            startCardList = new List<CardInfo>();
            BuildCards();
            CreateManagers();
            
            
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);
            GameModeManager.AddHook(GameModeHooks.HookPickStart, FirstPickStart);
            GameModeManager.AddHook(GameModeHooks.HookGameEnd, ResetData);
            GameModeManager.AddHook(GameModeHooks.HookInitStart, ResetData);
        }       

        private void CreateManagers()
        {
            cardBarManager = new CardBarManager();
            playerSettings = new PlayerSettings();
        }

        IEnumerator FirstPickStart(IGameModeHandler arg)
        {
            if (firstPick)
            {
                firstPick = false;
                PlayersMass = PlayerManager.instance.players.Where((person) => !ModdingUtils.AIMinion.Extensions.CharacterDataExtension.GetAdditionalData(person.data).isAIMinion).ToArray();
                playerSettings.SetStartStats(startCardList);
            }         

            yield break; 
        }

        IEnumerator PickEnd(IGameModeHandler arg)
        {
            if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode)
            {
                //PLAYERS = PlayerManager.instance.players.Where((person) => !ModdingUtils.AIMinion.Extensions.CharacterDataExtension.GetAdditionalData(person.data).isAIMinion).ToArray();

                yield return cardBarManager.CheckCardBar();
            }
            else
            {
                yield break;
            }            
        }

        IEnumerator ResetData(IGameModeHandler arg)
        {
            cardBarManager.Restore();
            firstPick = true;

            yield break;
        }

        private void BuildCards()
        {
            //Modules_common
            CustomCard.BuildCard<BatteriesEnergizer>();
            CustomCard.BuildCard<IFAK>();
            CustomCard.BuildCard<Laser>();
            CustomCard.BuildCard<QuickDrop>();
            CustomCard.BuildCard<RocketJump>();
            CustomCard.BuildCard<StrangePill>();
            CustomCard.BuildCard<TitaniumParts>();
            //Modules_uncommon
            CustomCard.BuildCard<AmmoXL>();
            CustomCard.BuildCard<BigBullet>();
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
            CustomCard.BuildCard<M4A1>();
            CustomCard.BuildCard<Winchester>();

            //StartCards
            CustomCard.BuildCard<Macarov>(cardInfo => 
            {
                startCardList.Add(cardInfo);
                ModdingUtils.Utils.Cards.instance.AddHiddenCard(cardInfo);
            });

            //CustomCard.BuildCard<Granade>();
        }
    }
}
