using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using RoundsTest.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace RoundsTest
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]

    public class RoundsTest : BaseUnityPlugin
    {
        #region Singletone

        public static RoundsTest Instance { get; private set; }

        #endregion

        private const string ModId = "com.aderom.rounds.RoundsTest";
        private const string ModName = "RoundsTest";
        public const string Version = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "RT";

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            Instance = this;
            CustomCard.BuildCard<AmmoXL>();
        }
    }
}
