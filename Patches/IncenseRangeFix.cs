using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.EntitySystem.Entities;

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

                var mindFogArea = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityAreaEffect>("0bd2c3ff0012e6b468497461448174c7");
                Kingmaker.ResourceLinks.PrefabLink fogFx = mindFogArea.Fx;

                Kingmaker.Utility.Feet fogIncrease = new(30);
                var incenseFogArea = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityAreaEffect>("4aeb5ae7923dac74d91069f13a7f0a95");
                incenseFogArea.Size = fogIncrease;
                incenseFogArea.Fx = fogFx;

                
            }
        }
    }
}
