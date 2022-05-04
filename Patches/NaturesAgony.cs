using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using System;

namespace WraithMods.Patches
{
    class NaturesAgony
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
                    PatchNaturesAgony();
                }
                catch (Exception ex)
                {
                    Main.logger.Log(ex.ToString());
                }
            }

            static void PatchNaturesAgony()
            {
                if (Main.Settings.useNaturesAgonyFix == false)
                {
                    return;
                }

                string naturesAgonyFeatureGUID = "51fdb667ce364cb43b341edfe0228d29";
                var naturesAgonyFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(naturesAgonyFeatureGUID);
                naturesAgonyFeature.GetComponent<IncreaseSpellDescriptorDC>().BonusDC = 2;
            }
        }
    }
}
