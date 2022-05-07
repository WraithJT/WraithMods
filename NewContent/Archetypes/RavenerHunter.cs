using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Localization;
using System;

namespace WraithMods.NewContent.Archetypes
{
    class RavenerHunter
    {
        private static readonly string chargedByNatureName = "ChargedByNature";
        private static readonly string chargedByNatureGUID = "9A39752F-CECF-470D-BCF3-90768FEAF91E";
        private static readonly string chargedByNatureDisplayName = "Demon Hunter";
        private static readonly string chargedByNatureDisplayNameKey = "DemonHunterName";
        private static readonly string chargedByNatureDescription =
            "You gain a +2 morale bonus on all attack rolls made against creatures with the demon subtype " +
            "and a +1 morale bonus on caster level checks to penetrate spell resistance.";
        private static readonly string chargedByNatureDescriptionKey = "DemonHunterDescription";

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
                    //AddRavenerHunter();
                }
                catch (Exception ex)
                {
                    Main.logger.Log(ex.ToString());
                }
            }

            static void AddRavenerHunter()
            {
                if (Main.Settings.useRavenerHunter == false)
                {
                    return;
                }

                string oracleClassGUID = "20ce9bf8af32bee4c8557a045ab499b1";
                string oracleMysterySelectionGUID = "5531b975dcdf0e24c98f1ff7e017e741";
                string inquisitorClassGUID = "f1a70d9e1b0b41e49874e1fa9052a1ce";

                BlueprintFeature chargedByNature = CreateBlueprintFeature(
                    chargedByNatureName,
                    chargedByNatureDisplayName,
                    chargedByNatureGUID,
                    chargedByNatureDescription);
                chargedByNature.IsClassFeature = true;
                chargedByNature.m_Icon = null;

                //figure out how to add stuff
                BlueprintArchetype ravenerHunter = new();
                ravenerHunter.AddFeatures = new LevelEntry[]
                {

                };
                ravenerHunter.RemoveFeatures = new LevelEntry[]
                {

                };


                //BlueprintFeature chargedByNature = new BlueprintFeature();
                //chargedByNature.name = "";
                //LocalizedString description = LocalizationTool.CreateString(chargedByNatureDescriptionKey, chargedByNatureDescription);
                //FeatureConfigurator.New(chargedByNatureName, chargedByNatureGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(chargedByNatureDisplayNameKey, chargedByNatureDisplayName))
                //    .SetDescription(LocalizationTool.CreateString(chargedByNatureDescriptionKey, chargedByNatureDescription))
                //    .addlev;


                //string secondSpiritGUID = "2faa80662a56ab644aec2f875a68597f";
                //var secondSpirit = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(secondSpiritGUID);
                //secondSpirit.ComponentsArray = secondSpirit.ComponentsArray.Where(c => !(c is PrerequisiteFeature)).ToArray();

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

                //var componentsArray = secondSpirit.ComponentsArray;
                //secondSpirit.ComponentsArray = componentsArray.AddItem(prerequisiteClassLevel).ToArray();
                //componentsArray = secondSpirit.ComponentsArray;
                //secondSpirit.ComponentsArray = componentsArray.AddItem(prerequisiteNoArchetype).ToArray();


            }

            static BlueprintFeature CreateBlueprintFeature(string name, string displayName, string GUID, string description)
            {
                string displayNameKey = name + "Name";
                string descriptionKey = name + "Description";

                LocalizedString locDisplayName = LocalizationTool.CreateString(displayNameKey, name);
                LocalizedString locDescription = LocalizationTool.CreateString(descriptionKey, description);

                BlueprintFeature bp = new();
                bp.name = name;
                bp.m_DisplayName = locDisplayName;
                bp.m_Description = locDescription;

                return bp;
            }
        }
    }
}
