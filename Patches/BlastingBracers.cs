using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Localization;
using System;
using System.Linq;

namespace WraithMods.Patches
{
    class BlastingBracers
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
                    //PatchBlastingBracers();
                }
                catch (Exception ex)
                {
                    Main.logger.Log(ex.ToString());
                }
            }

            static void PatchBlastingBracers()
            {
                if (Main.Settings.useBlastingBracers == false)
                {
                    return;
                }

                //TO DO: build something new and replace references to original blaster bracers.
                //ideas: ray type at-will item, kineticist booster of some kind

                string simpleEnergyBlasterBracerItemGUID = "fe2acd72f8e79884f8b693b1e020a20c";
                var simpleEnergyBlasterBracerItem = ResourcesLibrary.TryGetBlueprint<BlueprintItemWeapon>(simpleEnergyBlasterBracerItemGUID);



                string wristGUID = "3869C795-6BDF-4BF0-8A95-19C7A41FCC0D";
                BlueprintGuid blueprintGuid = new(System.Guid.Parse(wristGUID));
                string displayName = "Bracers of Blasting";
                string displayNameKey = "";
                string descriptionKey = "newBracersOfBlasting";
                string description = "";

                string divineDismissalGUID = "c896a13e9c2efd149814b93083d0c1b4";
                var divineDismissal = ResourcesLibrary.TryGetBlueprint<BlueprintItemWeapon>(divineDismissalGUID);
                BlueprintItemEquipmentWrist wrist = new BlueprintItemEquipmentWrist();
                wrist.AssetGuid = blueprintGuid;
                LocalizedString displayNameText = LocalizationTool.CreateString(displayNameKey, displayName);
                wrist.m_DisplayNameText = displayNameText;
                LocalizedString descriptionText = LocalizationTool.CreateString(descriptionKey, description);
                wrist.m_DescriptionText = descriptionText;
                //BlueprintWeaponEnchantment[] blueprintWeaponEnchantments = divineDismissal.m_Enchantments.ToArray();
                foreach (BlueprintWeaponEnchantmentReference e in divineDismissal.m_Enchantments.ToArray())
                {

                }
                wrist.m_Enchantments = divineDismissal.m_Enchantments.ToArray();


                string electricBlastAbilityGUID = "24f26ac07d21a0e4492899085d1302f6";

                //string naturesAgonyFeatureGUID = "51fdb667ce364cb43b341edfe0228d29";
                //var naturesAgonyFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(naturesAgonyFeatureGUID);
                //naturesAgonyFeature.GetComponent<IncreaseSpellDescriptorDC>().BonusDC = 2;
            }
        }
    }
}
