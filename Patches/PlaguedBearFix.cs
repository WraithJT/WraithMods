using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UI.GenericSlot;
using System;
using WraithMods.Utilities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Blueprints;
using BlueprintCore.Blueprints.Configurators;

namespace WraithMods.Patches
{
    class PlaguedBearFix
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { PatchPlaguedBearFix(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            static void PatchPlaguedBearFix()
            {
                if (Main.Settings.usePlaguedBearFix == false)
                {
                    return;
                }

                //var CR8_PlaguedBear = ResourcesLibrary.TryGetBlueprint<BlueprintUnit>("bda1eb32356d30e418ebe2db715fc8be");
                //var CR7_PlaguedBear_RE_low = ResourcesLibrary.TryGetBlueprint<BlueprintUnit>("e1a79a5237a4409c9272dfaa3d77569a");
                //var CR8_PlaguedBear_RE_high = ResourcesLibrary.TryGetBlueprint<BlueprintUnit>("144fe2a39484421498a612ba952e9c0a");
                string[] plaguedBears = new string[] {
                    "bda1eb32356d30e418ebe2db715fc8be",
                    "e1a79a5237a4409c9272dfaa3d77569a",
                    "144fe2a39484421498a612ba952e9c0a"
                };

                foreach (string s in plaguedBears)
                {
                    UnitConfigurator.For(s).SetCharisma(15).Configure();
                }
            }
        }
    }
}