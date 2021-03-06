using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Linq;
using WraithMods.Utilities;

namespace WraithMods.Patches
{
    class WildlandSecondSpirit
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
                    PatchWildlandSecondSpirit();
                }
                catch (Exception ex)
                {
                    Tools.LogMessage("EXCEPTION: " + ex.ToString());
                }
            }

            static void PatchWildlandSecondSpirit()
            {
                if (Main.Settings.useWildlandSecondSpirit == false)
                {
                    return;
                }

                //Remove previous Spirit Selection prerequisite
                string secondSpiritGUID = "2faa80662a56ab644aec2f875a68597f";
                var secondSpirit = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(secondSpiritGUID);
                secondSpirit.ComponentsArray = secondSpirit.ComponentsArray.Where(c => !(c is PrerequisiteFeature)).ToArray();

                //Build new prerequisite based off of requiring 1 Shaman Spirit
                string shamanSPiritSelectionGUID = "00c8c566d1825dd4a871250f35285982";
                PrerequisiteFeaturesFromList prerequisiteFeaturesFromList = new();
                prerequisiteFeaturesFromList.name = "$PrerequisiteFeaturesFromList$7AACEA2A-F7C0-4AC0-81CA-A299D46B1D35";
                prerequisiteFeaturesFromList.Group = Prerequisite.GroupType.All;
                prerequisiteFeaturesFromList.CheckInProgression = false;
                prerequisiteFeaturesFromList.HideInUI = false;
                prerequisiteFeaturesFromList.Amount = 1;
                prerequisiteFeaturesFromList.m_Features = ResourcesLibrary
                    .TryGetBlueprint<BlueprintFeatureSelection>(shamanSPiritSelectionGUID)
                    .m_AllFeatures;

                //Add new prerequisite to Second Spirit prerequisite list
                var componentsArray = secondSpirit.ComponentsArray;
                secondSpirit.ComponentsArray = componentsArray.AddItem(prerequisiteFeaturesFromList).ToArray();

                Tools.LogMessage("Reconfigured Second Spirit for Wildland Shaman");

                #region old method
                //string shamanClassGUID = "145f1d3d360a7ad48bd95d392c81b38e";
                //PrerequisiteClassLevel prerequisiteClassLevel = new();
                //var shamanClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(shamanClassGUID);
                //prerequisiteClassLevel.m_CharacterClass = shamanClass.ToReference<BlueprintCharacterClassReference>();
                //prerequisiteClassLevel.Level = 1;

                //string unswornShamanArchetypeGUID = "556590a43467a27459ac1a80324c9f9f";
                //var unswornShamanArchetype = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(unswornShamanArchetypeGUID);
                //PrerequisiteNoArchetype prerequisiteNoArchetype = new();
                //prerequisiteNoArchetype.m_Archetype = unswornShamanArchetype.ToReference<BlueprintArchetypeReference>();
                //prerequisiteNoArchetype.m_CharacterClass = shamanClass.ToReference<BlueprintCharacterClassReference>();

                //secondSpirit.ComponentsArray = componentsArray.AddItem(prerequisiteClassLevel).ToArray();
                //componentsArray = secondSpirit.ComponentsArray;
                //secondSpirit.ComponentsArray = componentsArray.AddItem(prerequisiteNoArchetype).ToArray();
                #endregion
            }
        }
    }
}
