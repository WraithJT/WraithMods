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
    class ExtraMythicFeat
    {
        private static readonly string ExtraMythicFeatMythicAbilityName = "ExtraMythicFeat";
        private static readonly string ExtraMythicFeatMythicAbilityGuid = "C916448F-690D-4F4E-9D82-4D6F376E621D";
        private static readonly string ExtraMythicFeatMythicAbilityDisplayName = "Extra Mythic Feat";
        private static readonly string ExtraMythicFeatMythicAbilityDisplayNameKey = "ExtraMythicFeatName";
        private static readonly string ExtraMythicFeatMythicAbilityDescription =
            "You gain a bonus mythic feat.";
        private static readonly string ExtraMythicFeatMythicAbilityDescriptionKey = "ExtraMythicFeatDescription";

        private static readonly string MythicAbilitySelection = "ba0e5a900b775be4a99702f1ed08914d";
        private static readonly BlueprintFeature ExtraMythicAbilityMythicFeat = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("8a6a511c55e67d04db328cc49aaad2b8");
        private static readonly BlueprintFeature ExtraFeatMythicFeat = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("e10c4f18a6c8b4342afe6954bde0587b");
        private static readonly string MythicFeatSelection = "9ee0f6745f555484299b0a1563b99d81";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { PatchExtraMythicFeat(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }
            public static void PatchExtraMythicFeat()
            {
                BlueprintFeatureSelection ExtraMythicFeatMythicAbility = FeatureSelectionConfigurator.New(ExtraMythicFeatMythicAbilityName, ExtraMythicFeatMythicAbilityGuid)
                    .SetDisplayName(LocalizationTool.CreateString(ExtraMythicFeatMythicAbilityDisplayNameKey, ExtraMythicFeatMythicAbilityDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(ExtraMythicFeatMythicAbilityDescriptionKey, ExtraMythicFeatMythicAbilityDescription))
                    .SetFeatureGroups(FeatureGroup.MythicAbility)
                    .SetIcon(ExtraMythicAbilityMythicFeat.Icon)
                    .Configure();
                ExtraMythicFeatMythicAbility.m_AllFeatures = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(MythicFeatSelection)
                    .m_AllFeatures.Where(c => (
                        c.deserializedGuid != ExtraMythicAbilityMythicFeat.ToReference<BlueprintFeatureReference>().deserializedGuid &&
                        c.deserializedGuid != ExtraFeatMythicFeat.ToReference<BlueprintFeatureReference>().deserializedGuid
                        )).ToArray();

                if (Main.Settings.useDualPath == false) { return; }
                FeatureSelectionConfigurator.For(MythicAbilitySelection).AddToFeatures(ExtraMythicFeatMythicAbilityName).Configure();
            }
        }
    }
}
