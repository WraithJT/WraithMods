using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.Abilities;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.Configurators.Buffs;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Localization;
using Kingmaker.Utility;
using Kingmaker.ResourceLinks;
using System;


namespace WraithMods.Patches
{
    class IncenseRangeFix
    {
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
                    PatchIncenseFog();
                }
                catch (Exception ex)
                {
                    Main.logger.Log(ex.ToString());
                }
            }

            static void PatchIncenseFog()
            {
                if (Main.Settings.useIncenseRangeFix == false)
                {
                    return;
                }
                string incenseSynthesizerIncenseFogSelectionGUID = "73d5950937bf0aa428e82c54c968f7e6";
                string incenseFogIncreasedRangeFeatureGUID = "1cd39502119e97f4caa72e7e53ce5ee9";
                FeatureSelectionConfigurator.For(incenseSynthesizerIncenseFogSelectionGUID).RemoveFromFeatures(incenseFogIncreasedRangeFeatureGUID).Configure();

                string incenseFogFeatureGUID = "7614401346b64a8409f7b8c367db488f";
                string incenseFogToggleAbilityGUID = "b62231e54e07068419a420f2988157b3";
                string incenseFogEffectBuffGUID = "d2facca5b5801234b95f0cd75ebac3c1";
                string newIncenseFogDescription = "A 1st-level incense synthesizer can use his ability to create incense fog " +
                    "in a 30-foot area around him to aid his allies (including himself), improving their combat abilities. " +
                    "An affected ally receives a +1 alchemical bonus on attack and weapon damage rolls.";
                string descriptionKey = "NewIncenseFogDescription";
                LocalizedString newDescription = LocalizationTool.CreateString(descriptionKey, newIncenseFogDescription);
                FeatureConfigurator.For(incenseFogFeatureGUID).SetDescription(newDescription).Configure();
                BuffConfigurator.For(incenseFogEffectBuffGUID).SetDescription(newDescription).Configure();
                ActivatableAbilityConfigurator.For(incenseFogToggleAbilityGUID).SetDescription(newDescription).Configure();

                Feet incenseFogRangeIncrease = new(30);
                var incenseFogArea = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityAreaEffect>("4aeb5ae7923dac74d91069f13a7f0a95");
                incenseFogArea.Size = incenseFogRangeIncrease;
                
                //string incenseFogAreaGUID = "4aeb5ae7923dac74d91069f13a7f0a95";
                var flameDancerPerformanceArea = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityAreaEffect>("0bd2c3ff0012e6b468497461448174c7");
                PrefabLink incenseFogFx = flameDancerPerformanceArea.Fx;
                incenseFogArea.Fx = incenseFogFx;
                //AbilityAreaEffectConfigurator.For(incenseFogAreaGUID).SetSize(30);
            }
        }
    }
}