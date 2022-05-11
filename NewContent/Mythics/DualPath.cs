using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.Abilities;
using BlueprintCore.Blueprints.Components;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System;
using WraithMods.Utilities;
using System.Linq;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace WraithMods.NewContent.Mythics
{
    class DualPath
    {
        private static readonly string FeatName = "DualPath";
        private static readonly string FeatGuid = "292B23FA-1539-43AB-9773-922611A42547";
        private static readonly string DisplayName = "Dual Path";
        private static readonly string DisplayNameKey = "DualPathName";
        private static readonly string Description =
            "Select a mythic path other than the path you selected at your moment of ascension. " +
            "You gain that path’s 1st- and 2nd-tier abilities.";
        private static readonly string DescriptionKey = "DualPathDescription";

        private static readonly string MythicAbilitySelection = "ba0e5a900b775be4a99702f1ed08914d";
        private static readonly string ExtraMythicAbilityMythicFeat = "8a6a511c55e67d04db328cc49aaad2b8";
        private static readonly string FirstAscensionSelection = "1421e0034a3afac458de08648d06faf0";
        private static readonly string MythicCompanionClass = "530b6a79cb691c24ba99e1577b4beb6d";

        private static readonly string[] FirstAscensionProgressionGUIDS = new string[] {
                    "46df0532714a9454eb5fbad64ce6c14f",     //Aeon
                    "b236003be2b9400498b9dd0f07b0c93c",     //Angel
                    "132afc1a28bd9d442821385f7cbf1c05",     //Azata
                    "8afd697daf0d47a4883759a6bc1aff88",     //Demon
                    "a796af14198bf5e45b63f056c32107a2",     //Lich
                    "11ba180ac736c894e937602e54f7320c"};    //Trickster

        private static readonly BlueprintFeatureReference[] FirstAscensionFeatureGUIDS = new BlueprintFeatureReference[] {
                    ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("11744000c915c6144ac6be7ceca03521").ToReference<BlueprintFeatureReference>(),     //Aeon
                    ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("58abcd052187a264aba9b0ce285c49f4").ToReference<BlueprintFeatureReference>(),     //Angel
                    ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("d2cfbbb941e07b04299b617017e369f1").ToReference<BlueprintFeatureReference>(),     //Azata
                    ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("ac8e25586edaf6348a7cbc35d3aa21f2").ToReference<BlueprintFeatureReference>(),     //Demon
                    ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("4457101b8eca5a546a5a0bdb651bd003").ToReference<BlueprintFeatureReference>(),     //Lich
                    ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("2df27c2ac48abd949bdf891c6adb625e").ToReference<BlueprintFeatureReference>()};    //Trickster

        private static readonly string[] MythicClassGUIDS = new string[] {
            "15a85e67b7d69554cab9ed5830d0268e",     //Aeon
            "a5a9fe8f663d701488bd1db8ea40484e",     //Angel
            "9a3b2c63afa79744cbca46bea0da9a16",     //Azata
            "8e19495ea576a8641964102d177e34b7",     //Demon
            "5d501618a28bdc24c80007a5c937dcb7",     //Lich
            "8df873a8c6e48294abdb78c45834aa0a",     //Trickster
            "211f49705f478b3468db6daa802452a2",     //Devil
            "daf1235b6217787499c14e4e32142523",     //Gold Dragon
            "3d420403f3e7340499931324640efe96",     //Legend
            "5295b8e13c2303f4c88bdb3d7760a757",     //Swarm
        };

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        [HarmonyPriority(Priority.First)]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { PatchDualPath(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }
            public static void PatchDualPath()
            {
                BlueprintFeatureSelection blueprintFeatureSelection = FeatureSelectionConfigurator.New(FeatName, FeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .SetFeatureGroups(FeatureGroup.MythicAbility)
                    .PrerequisiteNoFeature(FeatGuid, Prerequisite.GroupType.All)
                    .Configure();
                blueprintFeatureSelection.m_Features = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FirstAscensionSelection).m_AllFeatures;
                blueprintFeatureSelection.m_AllFeatures = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FirstAscensionSelection).m_AllFeatures;
                blueprintFeatureSelection.AddPrerequisites(Tools.Create<PrerequisiteFeaturesFromList>(c =>
                {
                    c.m_Features = FirstAscensionFeatureGUIDS;
                    c.Group = Prerequisite.GroupType.All;
                }));
                foreach (string s in MythicClassGUIDS)
                {
                    blueprintFeatureSelection.AddPrerequisites(Tools.Create<PrerequisiteClassLevel>(c =>
                    {
                        c.m_CharacterClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(s).ToReference<BlueprintCharacterClassReference>();
                        c.Level = 3;
                        c.Group = Prerequisite.GroupType.Any;
                    }));
                }
                blueprintFeatureSelection.AddPrerequisites(Tools.Create<PrerequisiteClassLevel>(c =>
                {
                    c.m_CharacterClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(MythicCompanionClass).ToReference<BlueprintCharacterClassReference>();
                    c.Level = 8;
                    c.Group = Prerequisite.GroupType.Any;
                }));

                if (Main.Settings.useDualPath == false) { return; }
                AddPrereqsToFirstAscensions();
                FeatureSelectionConfigurator.For(MythicAbilitySelection).AddToFeatures(FeatName).Configure();
                FeatureSelectionConfigurator.For(ExtraMythicAbilityMythicFeat).AddToFeatures(FeatName).Configure();
            }

            static void AddPrereqsToFirstAscensions()
            {
                foreach (string s in FirstAscensionProgressionGUIDS)
                {
                    var feature = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>(s);
                    foreach (string m in MythicClassGUIDS)
                    {
                        BlueprintProgression.ClassWithLevel mythicClassWithlevel = new();
                        mythicClassWithlevel.m_Class = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(m).ToReference<BlueprintCharacterClassReference>();
                        mythicClassWithlevel.AdditionalLevel = 0;
                        feature.m_Classes = feature.m_Classes.AppendToArray(mythicClassWithlevel);
                    }
                    feature.AddPrerequisites(Tools.Create<PrerequisiteNoFeature>(c =>
                    {
                        c.m_Feature = feature.ToReference<BlueprintFeatureReference>();
                        c.Group = Prerequisite.GroupType.All;
                    }));
                    feature.GiveFeaturesForPreviousLevels = true;
                }
            }
        }
    }
}