using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
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
                    //PatchWildlandSecondSpirit();
                }
                catch (Exception ex)
                {
                    Main.logger.Log(ex.ToString());
                }
            }

            static void PatchWildlandSecondSpirit()
            {
                if (Main.Settings.useWildlandSecondSpirit == false)
                {
                    return;
                }

                string secondSpiritGUID = "2faa80662a56ab644aec2f875a68597f";
                var secondSpirit = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(secondSpiritGUID);
                secondSpirit.ComponentsArray = secondSpirit.ComponentsArray.Where(c => !(c is PrerequisiteFeature)).ToArray();
                //PrerequisiteFeature prerequisiteFeature = new();
                //prerequisiteFeature.name = "$PrerequisiteFeature$E9C01E07-6E81-4E6D-8FCF-053C37ACC8BE";
                //prerequisiteFeature.Group = Prerequisite.GroupType.All;
                //prerequisiteFeature.CheckInProgression = false;
                //prerequisiteFeature.HideInUI = false;
                //string animalCompanionSelectionWildlandShamanGUID = "164c875d6b27483faba479c7f5261915";
                //var animalCompanionSelectionWildlandShaman = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(animalCompanionSelectionWildlandShamanGUID);
                //prerequisiteFeature.m_Feature = animalCompanionSelectionWildlandShaman.ToReference<BlueprintFeatureReference>();
                //secondSpirit.Components.Append(prerequisiteFeature);

                string shamanClassGUID = "145f1d3d360a7ad48bd95d392c81b38e";
                PrerequisiteClassLevel prerequisiteClassLevel = new();
                var shamanClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(shamanClassGUID);
                prerequisiteClassLevel.m_CharacterClass = shamanClass.ToReference<BlueprintCharacterClassReference>();
                prerequisiteClassLevel.Level = 1;

                string unswornShamanArchetypeGUID = "556590a43467a27459ac1a80324c9f9f";
                var unswornShamanArchetype = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(unswornShamanArchetypeGUID);
                PrerequisiteNoArchetype prerequisiteNoArchetype = new();
                prerequisiteNoArchetype.m_Archetype = unswornShamanArchetype.ToReference<BlueprintArchetypeReference>();
                prerequisiteNoArchetype.m_CharacterClass = shamanClass.ToReference<BlueprintCharacterClassReference>();
                

                secondSpirit.ComponentsArray.AppendToArray(prerequisiteClassLevel);
                secondSpirit.ComponentsArray.AppendToArray(prerequisiteNoArchetype);
                
                
                //string naturesAgonyFeatureGUID = "51fdb667ce364cb43b341edfe0228d29";
                //var naturesAgonyFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(naturesAgonyFeatureGUID);
                //naturesAgonyFeature.GetComponent<IncreaseSpellDescriptorDC>().BonusDC = 2;
            }
        }
    }
}
