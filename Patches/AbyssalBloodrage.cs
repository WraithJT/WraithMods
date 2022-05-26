using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using WraithMods.Utilities;

namespace WraithMods.Patches
{
    class AbyssalBloodrage
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
                    //PatchAbyssalBloodrage();
                }
                catch (Exception ex)
                {
                    Tools.LogMessage("EXCEPTION: " + ex.ToString());
                }
            }

            static void PatchAbyssalBloodrage()
            {
                if (Main.Settings.useEnableHiddenFeatures == false)
                {
                    return;
                }


            }
        }
    }
}


// remove Primalist Progression prerequisitenofeature