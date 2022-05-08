using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
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

            //private static readonly BlueprintFeature OracleMystery = BlueprintTools.GetBlueprint<BlueprintFeature>("5531b975dcdf0e24c98f1ff7e017e741");
            //private static readonly BlueprintFeature DemonHunter = BlueprintTools.GetBlueprint<BlueprintFeature>("5531b975dcdf0e24c98f1ff7e017e741");
            //private static readonly BlueprintFeature SoloTactics = BlueprintTools.GetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
            //private static readonly BlueprintFeature TeamworkFeat = BlueprintTools.GetBlueprint<BlueprintFeature>("d87e2f6a9278ac04caeb0f93eff95fcb");
            //private static readonly BlueprintFeature OracleRevelation = BlueprintTools.GetBlueprint<BlueprintFeature>("60008a10ad7ad6543b1f63016741a5d2");
            //private static readonly BlueprintFeature Domain = BlueprintTools.GetBlueprint<BlueprintFeature>("48525e5da45c9c243a343fc6545dbdb9");

            //private static readonly BlueprintCharacterClass InquisitorClass = BlueprintTools.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");

            static void AddRavenerHunter()
            {
                if (Main.Settings.useRavenerHunter == false)
                {
                    return;
                }

                //string oracleClassGUID = "20ce9bf8af32bee4c8557a045ab499b1";
                //string oracleMysterySelectionGUID = "5531b975dcdf0e24c98f1ff7e017e741";
                //string inquisitorClassGUID = "f1a70d9e1b0b41e49874e1fa9052a1ce";

                //BlueprintFeature chargedByNature = CreateBlueprintFeature(
                //    chargedByNatureName,
                //    chargedByNatureDisplayName,
                //    chargedByNatureGUID,
                //    chargedByNatureDescription);
                //chargedByNature.IsClassFeature = true;
                //chargedByNature.m_Icon = null;

                //figure out how to add stuff

                //var RavenerHunterChargedByNatureFeature = Helpers.CreateBlueprint<BlueprintFeature>(TTTContext, "RavenerHunterChargedByNatureFeature", bp =>
                //{
                //    bp.SetName(TTTContext, "Charged by Nature");
                //    bp.SetDescription(TTTContext, "Rather than having a deity patron, a ravener hunter is charged by spirits to " +
                //        "eradicate evil wherever it appears. At 1st level, a ravener hunter chooses an oracle mystery from the " +
                //        "following list: ancestor, battle, bones, flame, life, nature, stone, waves, or wind. " +
                //        "She gains one revelation from her chosen mystery. She must meet the revelation’s prerequisites, " +
                //        "using her inquisitor level as her effective oracle level to determine the revelation’s effects, and she " +
                //        "never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                //        "chosen mystery at 8th level.");
                //    bp.m_Icon = OracleMystery.Icon;
                //    bp.IsClassFeature = true;
                //    bp.AddComponent<AddFacts>(c =>
                //    {
                //        c.m_Facts = new BlueprintUnitFactReference[] { OracleMystery.ToReference<BlueprintUnitFactReference>() };
                //    });
                //});

                //var RavenerHunterRevelation = Helpers.CreateBlueprint<BlueprintFeature>(TTTContext, "RavenerHunterRevelation", bp =>
                //{
                //    bp.SetName(TTTContext, "Revelation");
                //    bp.SetDescription(TTTContext, "At 1st and 8th levels, a ravener hunter uncovers a new secret about " +
                //        "her mystery that grants her powers and abilities. The ravener hunter must select a revelation " +
                //        "from the list of revelations available to her mystery. If a revelation is chosen at a later level, " +
                //        "the oracle gains all of the abilities and bonuses granted by that revelation " +
                //        "based on her current level. Unless otherwise noted, activating the power of a revelation is a " +
                //        "standard action. Unless otherwise noted, the DC " +
                //        "to save against these revelations is equal to 10 + 1/2 the ravener " +
                //        "hunter's level + the ravener hunter's Charisma modifier.");
                //    bp.m_Icon = OracleRevelation.Icon;
                //    bp.IsClassFeature = true;
                //    bp.AddComponent<AddFacts>(c =>
                //    {
                //        c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelation.ToReference<BlueprintUnitFactReference>() };
                //    });
                //});

                //var RavenerHunterDemonHunter = Helpers.CreateBlueprint<BlueprintFeature>(TTTContext, "RavenerHunterDemonHunter", bp =>
                //{
                //    bp.SetName(TTTContext, "Demon Hunter");
                //    bp.SetDescription(TTTContext, "At 3rd level, a ravener hunter gains Demon Hunter as a bonus feat, ignoring " +
                //        "its prerequisites. She also gains a +2 bonus on Knowledge (religion) checks to recognize the worshipers " +
                //        "of any deity with the Demon subdomain, as well as a +2 morale bonus on attack rolls and caster level checks " +
                //        "to overcome spell resistance of creatures that she recognizes as followers of such a deity.");
                //    bp.m_Icon = DemonHunter.Icon;
                //    bp.IsClassFeature = true;
                //    bp.AddComponent<AddFacts>(c =>
                //    {
                //        c.m_Facts = new BlueprintUnitFactReference[] { DemonHunter.ToReference<BlueprintUnitFactReference>() };
                //    });
                //});

                //var RavenerHunterArchetype = Helpers.CreateBlueprint<BlueprintArchetype>(TTTContext, "RavenerHunterArcehtype", bp =>
                //{
                //    bp.SetName(TTTContext, "Ravener Hunter");
                //    bp.SetDescription(TTTContext, "Throughout the Mwangi Expanse, cults of Angazhan pollute the pristine " +
                //        "jungle with demonic influence and wanton bloodshed. For generations, the catfolk of Murraseth have viewed " +
                //        "such faiths with loathing and hatred, and they believe it is their sacred duty to hunt down the followers of " +
                //        "the Ravener King and expel them from the Material Plane.");
                //    bp.RemoveFeatures = new LevelEntry[] {
                //    Helpers.CreateLevelEntry(1, Domain),
                //    Helpers.CreateLevelEntry(3, SoloTactics, TeamworkFeat)
                //};
                //    bp.AddFeatures = new LevelEntry[] {
                //    Helpers.CreateLevelEntry(1, RavenerHunterChargedByNatureFeature, RavenerHunterRevelation),
                //    Helpers.CreateLevelEntry(3, RavenerHunterDemonHunter),
                //    Helpers.CreateLevelEntry(6, SoloTactics),
                //    Helpers.CreateLevelEntry(8, RavenerHunterRevelation),
                //};
                //});

                //InquisitorClass.m_Archetypes = InquisitorClass.m_Archetypes.AppendToArray(RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>());

                //InquisitorClass.Progression.UIGroups = InquisitorClass.Progression.UIGroups.AppendToArray(
                //    Helpers.CreateUIGroup(
                //        RavenerHunterChargedByNatureFeature,
                //        RavenerHunterRevelation,
                //        RavenerHunterRevelation
                //    ),
                //    Helpers.CreateUIGroup(
                //        RavenerHunterDemonHunter
                //    )
                //);

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

            //static BlueprintFeature CreateBlueprintFeature(string name, string displayName, string GUID, string description)
            //{
            //    string displayNameKey = name + "Name";
            //    string descriptionKey = name + "Description";

            //    LocalizedString locDisplayName = LocalizationTool.CreateString(displayNameKey, name);
            //    LocalizedString locDescription = LocalizationTool.CreateString(descriptionKey, description);

            //    BlueprintFeature bp = new();
            //    bp.name = name;
            //    bp.m_DisplayName = locDisplayName;
            //    bp.m_Description = locDescription;

            //    return bp;
            //}
        }
    }
}
