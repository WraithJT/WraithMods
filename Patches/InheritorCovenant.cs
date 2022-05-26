using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UI.GenericSlot;
using System;
using WraithMods.Utilities;

namespace WraithMods.Patches
{
    class InheritorCovenant
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { PatchCovenant(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            static void PatchCovenant()
            {
                if (Main.Settings.useCovenantFix == false)
                {
                    return;
                }

                string Artifact_HolySymbolOfIomedaeBuffGuid = "c8b1c0f5cd21f1d4e892f7440ec28e24";
                string GoodAlignedGuid = "326da486cd9077242a0e25df7eb7cd78";
                string ColdIronWeaponEnchantmentGuid = "e5990dc76d2a613409916071c898eee8";

                BuffConfigurator.For(Artifact_HolySymbolOfIomedaeBuffGuid)
                    .AddBuffEnchantWornItem(
                        allWeapons: true,
                        enchantmentBlueprint: GoodAlignedGuid,
                        slot: EquipSlotBase.SlotType.SecondaryHand)
                    .AddBuffEnchantWornItem(
                        allWeapons: true,
                        enchantmentBlueprint: ColdIronWeaponEnchantmentGuid,
                        slot: EquipSlotBase.SlotType.SecondaryHand)
                    .Configure();
            }
        }
    }
}