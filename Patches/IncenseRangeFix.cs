using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;

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

                PatchIncenseFog();
            }

            static void PatchIncenseFog()
            {
                if (Main.Settings.useIncenseRangeFix == false)
                {
                    return;
                }

                string incenseFogFeatureGUID = "4aeb5ae7923dac74d91069f13a7f0a95";
                var incenseFogFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("4aeb5ae7923dac74d91069f13a7f0a95");
                string newIncenseFogDescription = "A 1st-level incense synthesizer can use his ability to create incense fog " +
                    "in a 30-foot area around him to aid his allies (including himself), improving their combat abilities. " +
                    "An affected ally receives a +1 alchemical {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Attack}attack{/g} " +
                    "and weapon {g|Encyclopedia:Damage}damage rolles{/g}.";
                //incenseFogFeature.ComponentsArray.Remove<>;
                string DescriptionKey = "IncenseFogDescription";

                Kingmaker.Localization.LocalizedString newDescription = LocalizationTool.CreateString(DescriptionKey, newIncenseFogDescription);
                FeatureConfigurator.For(incenseFogFeatureGUID).SetDescription(newDescription);

                Kingmaker.Utility.Feet incenseFogRangeIncrease = new(30);
                var incenseFogArea = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityAreaEffect>("4aeb5ae7923dac74d91069f13a7f0a95");
                incenseFogArea.Size = incenseFogRangeIncrease;

                var flameDancerPerformanceArea = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityAreaEffect>("0bd2c3ff0012e6b468497461448174c7");
                Kingmaker.ResourceLinks.PrefabLink incenseFogFx = flameDancerPerformanceArea.Fx;
                incenseFogArea.Fx = incenseFogFx;


                //73d5950937bf0aa428e82c54c968f7e6
                //1cd39502119e97f4caa72e7e53ce5ee9 --- increased range
                //var incenseSynthesizerIncenseFogSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("73d5950937bf0aa428e82c54c968f7e6");
                //incenseSynthesizerIncenseFogSelection.Features = incenseSynthesizerIncenseFogSelection.Features
                //    .Where(c => !(c is null))
                //    .ToArray();
                //incenseSynthesizerIncenseFogSelection.AllFeatures = incenseSynthesizerIncenseFogSelection.AllFeatures
                //    .Where(c => !(c is null))
                //    .ToArray();
                //incenseSynthesizerIncenseFogSelection.ComponentsArray = incenseSynthesizerIncenseFogSelection.Components
                //    .Where(c => !(c is null))
                //    .ToArray();
            }
        }
    }
}
