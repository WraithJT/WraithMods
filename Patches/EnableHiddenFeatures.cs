using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using System;
using System.Linq;

namespace WraithMods.Patches
{
    class EnableHiddenFeatures
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
                    PatchGreenFaith();
                }
                catch (Exception ex)
                {
                    Main.logger.Log(ex.ToString());
                }
            }

            static void PatchGreenFaith()
            {
                if (Main.Settings.useEnableHiddenFeatures == false)
                {
                    return;
                }

                string greenFaithFeatureGUID = "99a7a8f13c1300c42878558fa9471e2f";
                var greenFaithFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(greenFaithFeatureGUID);
                greenFaithFeature.ComponentsArray = greenFaithFeature.ComponentsArray.Where(c => !(c is PrerequisiteNoFeature)).ToArray();

                string backgroundRekarthDLC2GUID = "ab8136b1b9d54982a5c9faf100547483";
                var backgroundRekarthDLC2 = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(backgroundRekarthDLC2GUID);
                backgroundRekarthDLC2.ComponentsArray = backgroundRekarthDLC2.ComponentsArray.Where(c => !(c is PrerequisiteFeature)).ToArray();
                backgroundRekarthDLC2.GetComponents<AddBackgroundWeaponProficiency>().ForEach(c => c.StackBonusType = ModifierDescriptor.Trait);

                string backgroundsWandererSelectionGUID = "0cdd576724fce2240b372455889fac87";
                var backgroundsWandererSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(backgroundsWandererSelectionGUID);
                backgroundsWandererSelection.Features.AddItem<BlueprintFeature>(backgroundRekarthDLC2.ToReference<BlueprintFeatureReference>());
                backgroundsWandererSelection.AllFeatures.AddItem<BlueprintFeature>(backgroundRekarthDLC2.ToReference<BlueprintFeatureReference>());

                //string backgroundsBaseSelectionGUID = "f926dabeee7f8a54db8f2010b323383c";
                //var backgroundsBaseSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(backgroundsBaseSelectionGUID);

                //string backgroundRahadoumFaithlessGUID = "f99465e6886253744aaef25d9b7c90c1";
                //var backgroundRahadoumFaithless = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(backgroundRahadoumFaithlessGUID);
                //string backgroundsRegionalSelectionGUID = "f926dabeee7f8a54db8f2010b323383c";
                //var backgroundsRegionalSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(backgroundsRegionalSelectionGUID);
                ////backgroundsRegionalSelection.Features.AddItem<BlueprintFeature>(backgroundRahadoumFaithless.ToReference<BlueprintFeatureReference>());
                ////backgroundsRegionalSelection.AllFeatures.AddItem<BlueprintFeature>(backgroundRahadoumFaithless.ToReference<BlueprintFeatureReference>());
                ////backgroundsRegionalSelection.m_Features.AppendToArray(backgroundRahadoumFaithless.ToReference<BlueprintFeatureReference>());
                ////backgroundsRegionalSelection.m_AllFeatures.AppendToArray(backgroundRahadoumFaithless.ToReference<BlueprintFeatureReference>());
                //backgroundsRegionalSelection.AddFeatures(backgroundRahadoumFaithless);
                ////backgroundsRegionalSelection.Features.Append<BlueprintFeature>(backgroundRahadoumFaithless);
                ////backgroundsRegionalSelection.AllFeatures.Append<BlueprintFeature>(backgroundRahadoumFaithless);
            }
        }
    }
}