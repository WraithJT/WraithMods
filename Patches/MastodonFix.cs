using BlueprintCore.Blueprints.Configurators;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using System;
using WraithMods.Utilities;

namespace WraithMods.Patches
{
    class MastodonFix
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
                    PatchMastodon();
                }
                catch (Exception ex)
                {
                    Tools.LogMessage("EXCEPTION: " + ex.ToString());
                }
            }

            static void PatchMastodon()
            {
                if (Main.Settings.useMastodonFix == false)
                {
                    return;
                }

                string animalCompanionUnitMammoth = "e7aa96d15a45238438ae4cfb476f6bb9";
                //string animalCompanionUnitMammoth_Medium = "03142402362d4afca8252fced7e1258c";
                string slam1d6 = "767e6932882a99c4b8ca95c88d823137";

                //var slam1d6 = ResourcesLibrary.TryGetBlueprint<BlueprintItemWeapon>("767e6932882a99c4b8ca95c88d823137");

                //var slam1d6_component = ResourcesLibrary.TryGetBlueprint<BlueprintItemWeapon>(slam1d6);
                //var mastodon = ResourcesLibrary.TryGetBlueprint<BlueprintUnit>("1452fb3e0e3e2f6488bee09050097b6f");
                //UnitConfigurator.For(animalCompanionUnitMammoth).RemoveComponents(slam1d6_component).Configure();

                UnitConfigurator.For(animalCompanionUnitMammoth).AddAdditionalLimb(weapon: slam1d6).Configure();

            }
        }
    }
}

