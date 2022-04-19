using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities;
using System;
using BlueprintCore.Blueprints.Configurators.Buffs;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Utils;
using BlueprintCore.Blueprints.Configurators.Abilities;
using Kingmaker.Localization;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Utility;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.EntitySystem;
using BlueprintCore.Blueprints.Configurators;
using Kingmaker.Blueprints.Items.Weapons;

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
                    Main.logger.Log(ex.ToString());
                }
            }

            static void PatchMastodon()
            {
                if (Main.Settings.useTrulyEverlastingJudgment == false)
                {
                    return;
                }

                string animalCompanionUnitMammoth = "e7aa96d15a45238438ae4cfb476f6bb9";
                //string animalCompanionUnitMammoth_Medium = "03142402362d4afca8252fced7e1258c";
                string slam1d6 = "767e6932882a99c4b8ca95c88d823137";
                
                
                //var slam1d6_component = ResourcesLibrary.TryGetBlueprint<BlueprintItemWeapon>(slam1d6);
                //var mastodon = ResourcesLibrary.TryGetBlueprint<BlueprintUnit>("1452fb3e0e3e2f6488bee09050097b6f");
                //UnitConfigurator.For(animalCompanionUnitMammoth).RemoveComponents(slam1d6_component).Configure();
                
                UnitConfigurator.For(animalCompanionUnitMammoth).AddAdditionalLimb(slam1d6).Configure();
                
            }
        }
    }
}

