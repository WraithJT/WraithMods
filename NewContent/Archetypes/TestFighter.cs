using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.UnitLogic;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Customization;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Blueprints.Configurators.EntitySystem;
using System;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Utility;
using System.Linq;
using WraithMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Utility;
using System;
using System.Linq;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using System;
using System.Linq;

namespace WraithMods.NewContent.Archetypes
{
    class TestFighter
    {
        private static readonly string chargedByNatureName = "ChargedByNature";
        private static readonly string chargedByNatureGUID = "9A39752F-CECF-470D-BCF3-90768FEAF91E";
        private static readonly string chargedByNatureDisplayName = "Demon Hunter";
        private static readonly string chargedByNatureDisplayNameKey = "DemonHunterName";
        private static readonly string chargedByNatureDescription =
            "Rather than having a deity patron, a ravener hunter is charged by spirits to " +
                        "eradicate evil wherever it appears. At 1st level, a ravener hunter chooses an oracle mystery from the " +
                        "following list: ancestor, battle, bones, flame, life, nature, stone, waves, or wind. " +
                        "She gains one revelation from her chosen mystery. She must meet the revelation’s prerequisites, " +
                        "using her inquisitor level as her effective oracle level to determine the revelation’s effects, and she " +
                        "never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                        "chosen mystery at 8th level.";
        private static readonly string chargedByNatureDescriptionKey = "DemonHunterDescription";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try
                {
                    //AddTestFighter();
                }
                catch (Exception ex)
                {
                    Tools.LogMessage("EXCEPTION: " + ex.ToString());
                }
            }

            //private static readonly BlueprintFeature OracleMystery = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("5531b975dcdf0e24c98f1ff7e017e741");
            //private static readonly string OracleMysterySelection = "5531b975dcdf0e24c98f1ff7e017e741";
            //private static readonly BlueprintFeature DemonHunter = BlueprintTools.GetBlueprint<BlueprintFeature>("5531b975dcdf0e24c98f1ff7e017e741");
            //private static readonly BlueprintFeature SoloTactics = BlueprintTools.GetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
            //private static readonly BlueprintFeature TeamworkFeat = BlueprintTools.GetBlueprint<BlueprintFeature>("d87e2f6a9278ac04caeb0f93eff95fcb");
            //private static readonly BlueprintFeature OracleRevelation = BlueprintTools.GetBlueprint<BlueprintFeature>("60008a10ad7ad6543b1f63016741a5d2");
            //private static readonly BlueprintFeature Domain = BlueprintTools.GetBlueprint<BlueprintFeature>("48525e5da45c9c243a343fc6545dbdb9");

            private readonly static BlueprintCharacterClass InquisitorClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");

            static void AddTestFighter()
            {
                //if (Main.Settings.useRavenerHunter == false)
                //{
                //    return;
                //}
                //string oracleClassGUID = "20ce9bf8af32bee4c8557a045ab499b1";
                //string oracleMysterySelectionGUID = "5531b975dcdf0e24c98f1ff7e017e741";
                //string inquisitorClassGUID = "f1a70d9e1b0b41e49874e1fa9052a1ce";

                //BlueprintFeature chargedByNature = CreateBlueprintFeature(
                //    chargedByNatureName,
                //    chargedByNatureDisplayName,
                //    chargedByNatureGUID,
                //    chargedByNatureDescription);
                //chargedByNature.IsClassFeature = true;
                //chargedByNature.m_Icon = null;

                //figure out how to add stuff

                //BlueprintFeature RavenerHunterChargedByNatureFeature = new();

                //var RavenerHunterChargedByNatureFeature = new BlueprintFeature( bp =>
                //{
                //FeatureConfigurator.New(chargedByNatureName, chargedByNatureGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(chargedByNatureDisplayNameKey, chargedByNatureDisplayName,false))
                //    .SetDescription(LocalizationTool.CreateString(chargedByNatureDescriptionKey, chargedByNatureDescription))
                //    .SetIcon(OracleMystery.m_Icon)
                //    .SetIsClassFeature(true)
                //    .feat(OracleMystery)
                //    .Configure();

                //FeatureSelectionConfigurator.New(chargedByNatureName, chargedByNatureGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(chargedByNatureDisplayNameKey, chargedByNatureDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(chargedByNatureDescriptionKey, chargedByNatureDescription))
                //    .SetIcon(OracleMystery.m_Icon)
                //    .SetIsClassFeature(true)
                //    .AddToFeatures(OracleMysterySelection)

                //    .Configure();

                //ArchetypeConfigurator.New("name", "guid")
                //    .Configure();

                //LevelEntry levelEntry = new();

                string name = "Test Fighter";
                string nameKey = "TestFighterNameKey";
                string desc = "Some description here";
                string descKey = "TFDescKey";
                string guid = "3E677E18-9D5D-45B6-B499-1A8B93333E85";
                BlueprintFeatureSelection fighterFeat = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("41c8486641f7d6d4283ca9dae4147a9f");
                BlueprintFeature dodge = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("97e216dbb46ae3c4faef90cf6bbe6fd5");

                ArchetypeConfigurator.New(name, guid)
                    .SetDisplayName(LocalizationTool.CreateString(nameKey, name, false))
                    .SetDescription(LocalizationTool.CreateString(descKey, desc))
                    .Configure();

                BlueprintGuid blueprintGuid = new(System.Guid.Parse("3E677E18-9D5D-45B6-B499-1A8B93333E85"));
                var testFighterArch = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(guid);
                //testFighterArch.AssetGuid = blueprintGuid;
                //testFighterArch.LocalizedName = LocalizationTool.CreateString(nameKey, name, false);
                //testFighterArch.LocalizedDescription = LocalizationTool.CreateString(descKey, desc);
                testFighterArch.RemoveFeatures = new LevelEntry[]
                {
                    Tools.CreateLevelEntry(2, fighterFeat)
                };
                testFighterArch.AddFeatures = new LevelEntry[]
                {
                    Tools.CreateLevelEntry(2, dodge)
                };

                InquisitorClass.m_Archetypes = InquisitorClass.m_Archetypes.AppendToArray(testFighterArch.ToReference<BlueprintArchetypeReference>());

            }
        }
    }
}
