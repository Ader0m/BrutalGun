using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using RoundsTest.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using UnityEngine.Events;
using UnityEngine;
using UnboundLib.GameModes;
using System.Collections.Generic;
using System.Collections;

namespace RoundsTest
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(_MOD_ID, _MOD_NAME, VERSION)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]

    public class RoundsTest : BaseUnityPlugin
    {
        #region Singletone

        public static RoundsTest Instance { get; private set; }

        #endregion

        private const string _MOD_ID = "com.aderom.rounds.RoundsTest";
        private const string _MOD_NAME = "RoundsTest";
        public const string VERSION = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string MOD_INITIALS = "RT";

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

            GameModeManager.AddHook("PlayerPickStart", new Func<IGameModeHandler, IEnumerator>(this.PlayerPickStart));
            GameModeManager.AddHook("PlayerPickEnd", new Func<IGameModeHandler, IEnumerator>(this.PlayerPickEnd));
            GameModeManager.AddHook("GameStart", new Func<IGameModeHandler, IEnumerator>(this.GameStart));
            GameModeManager.AddHook("PickEnd", new Func<IGameModeHandler, IEnumerator>(this.PickEnd));
            GameModeManager.AddHook("PickStart", new Func<IGameModeHandler, IEnumerator>(this.PickStart));
            GameModeManager.AddHook("GameEnd", new Func<IGameModeHandler, IEnumerator>(this.GameEnd));
        }

        private IEnumerator GameStart(IGameModeHandler arg)
        {
            GameObject obj = new GameObject();
            obj.name = "123";
            MonoBehaviour.Instantiate(obj, this.GetComponentInParent<Transform>());

            CreateManagers();

            yield break;
        }

        private void CreateManagers()
        {
            cardBarManager = new CardBarManager();
        }

        private IEnumerator GameEnd(IGameModeHandler arg)
        {
            // прописать деструкторы
            throw new NotImplementedException();
        }

        private IEnumerator PickStart(IGameModeHandler arg)
        {
            // как-нибудь сделать чтобы падал хотябы один мод или тренировка
            throw new NotImplementedException();
        }

        private IEnumerator PickEnd(IGameModeHandler arg)
        {
            // запустить корректировку картбара
            throw new NotImplementedException();
        }

        private IEnumerator PlayerPickEnd(IGameModeHandler arg)
        {
            //узнать что это
            throw new NotImplementedException();
        }

        private IEnumerator PlayerPickStart(IGameModeHandler arg)
        {
            //узнать что это
            throw new NotImplementedException();
        }

        private void BuildCards()
        {
            //Modules
            CustomCard.BuildCard<AmmoXL>();
            //Pistol_common
            CustomCard.BuildCard<Glock>();
            //Rifle_rare
            CustomCard.BuildCard<AK74>();
            CustomCard.BuildCard<M4A1>();
        }
    }
}
