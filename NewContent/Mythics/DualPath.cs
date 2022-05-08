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
        private static readonly string FirstAscensionSelection = "1421e0034a3afac458de08648d06faf0";


        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { PatchDualPath(); }
                catch (Exception ex) { Main.logger.Log(ex.ToString()); }
            }
            public static void PatchDualPath()
            {
                AddPrereqsToFirstAscensions();

                FeatureSelectionConfigurator.New(FeatName, FeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .SetFeatureGroups(FeatureGroup.MythicAbility)
                    .PrerequisiteFeature(FirstAscensionSelection)
                    .PrerequisiteNoFeature(FeatGuid)
                    .Configure();

                BlueprintFeatureSelection blueprintFeatureSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FeatGuid);
                blueprintFeatureSelection.m_Features = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FirstAscensionSelection).m_AllFeatures;
                blueprintFeatureSelection.m_AllFeatures = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FirstAscensionSelection).m_AllFeatures;

                if (Main.Settings.useDualPath == false) { return; }
                FeatureSelectionConfigurator.For(MythicAbilitySelection).AddToFeatures(FeatName).Configure();
            }

            static void AddPrereqsToFirstAscensions()
            {
                string[] ascensionGUIDS = new string[] {
                    "46df0532714a9454eb5fbad64ce6c14f",
                    "b236003be2b9400498b9dd0f07b0c93c",
                    "132afc1a28bd9d442821385f7cbf1c05",
                    "8afd697daf0d47a4883759a6bc1aff88",
                    "a796af14198bf5e45b63f056c32107a2",
                    "11ba180ac736c894e937602e54f7320c"};

                foreach (string s in ascensionGUIDS)
                {
                    var feature = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>(s);
                    PrerequisiteNoFeature prerequisite = new();
                    prerequisite.m_Feature = feature.ToReference<BlueprintFeatureReference>();
                    prerequisite.Group = Prerequisite.GroupType.All;
                    prerequisite.CheckInProgression = false;
                    prerequisite.HideInUI = false;
                    feature.Components.AddItem(prerequisite);
                }
            }
        }
    }
}
