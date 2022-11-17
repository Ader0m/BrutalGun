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

        #region Const

        private const string _MOD_ID = "com.aderom.rounds.RoundsTest";
        private const string _MOD_NAME = "BrutalGun";
        public const string VERSION = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string MOD_INITIALS = "BGun";

        #endregion

        public CardBarManager cardBarManager;

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(_MOD_ID);
            harmony.PatchAll();
        }

        void Start()
        {
            Instance = this;
            BuildCards();
            CreateManagers();
            
            GameModeManager.AddHook(GameModeHooks.HookPickEnd, PickEnd);         
        }       

        private void CreateManagers()
        {
            cardBarManager = new CardBarManager();
        }

        IEnumerator PickEnd(IGameModeHandler arg)
        {           
            yield return cardBarManager.CheckCardBar();
        }

        private void BuildCards()
        {
            //Modules_common
            CustomCard.BuildCard<IFAK>();
            CustomCard.BuildCard<Laser>();
            CustomCard.BuildCard<QuickDrop>();
            CustomCard.BuildCard<TitaniumParts>();
            //Modules_uncommon
            CustomCard.BuildCard<AmmoXL>();
            CustomCard.BuildCard<LightArmor>();
            CustomCard.BuildCard<LightBolt>();
            CustomCard.BuildCard<ScopeX2>();
            CustomCard.BuildCard<SecondArmorPlate>();
            //Modules_rare
            CustomCard.BuildCard<ScopeX8>();
            CustomCard.BuildCard<ArmoredSuit>();
            CustomCard.BuildCard<ExpBullet>();
            //Weapon_common
            CustomCard.BuildCard<FiveSeven>();
            CustomCard.BuildCard<Glock>();
            CustomCard.BuildCard<SawnOff>();
            //Weapon_uncommon
            CustomCard.BuildCard<Mosin>();
            CustomCard.BuildCard<P90>();
            //Weapon_rare
            CustomCard.BuildCard<AK74>();
            CustomCard.BuildCard<M4A1>();
        }
    }
}
