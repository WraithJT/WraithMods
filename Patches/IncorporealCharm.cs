//using BlueprintCore.Blueprints.Configurators.Classes;
//using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
//using HarmonyLib;
//using Kingmaker.Blueprints.JsonSystem;
//using Kingmaker.UI.GenericSlot;
//using System;
//using WraithMods.Utilities;
//using Kingmaker.EntitySystem.Stats;

//namespace WraithMods.Patches
//{
//    class IncorporealCharm
//    {
//        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
//        static class BlueprintsCache_Init_patch
//        {
//            static bool Initialized;

//            static void Postfix()
//            {
//                if (Initialized) return;
//                Initialized = true;

//                try { PatchIncorporealCharm(); }
//                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
//            }

//            static void PatchIncorporealCharm()
//            {
//                if (Main.Settings.useIncorporealCharmFix == false)
//                {
//                    return;
//                }

//                string IncorporealCharmFeatureGuid = "8ee86ca474114d8d8eb0946a2ff43eb8";

//                FeatureConfigurator.For(IncorporealCharmFeatureGuid)
//                    .AddRecalculateOnStatChange(stat: StatType.Charisma)
//                    .Configure();
//            }
//        }
//    }
//}