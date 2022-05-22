using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Utility;
using System;
using System.Linq;
using WraithMods.Utilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.Blueprints.Items.Weapons;

namespace WraithMods.NewContent.Feats
{
    class WayOfTheShootingStar
    {
        private static readonly string wayOfTheShootingStarFeatName = "WayOfTheShootingStar";
        private static readonly string wayOfTheShootingStarFeatGuid = "DDB37822-1AA8-49E0-8316-44DFA09FB954";
        private static readonly string wayOfTheShootingStarDisplayName = "Way of the Shooting Star";
        private static readonly string wayOfTheShootingStarDisplayNameKey = "WayOfTheShootingStarName";
        private static readonly string wayOfTheShootingStarDescription =
            "You can add your Charisma bonus to attack rolls and damage rolls when wielding a " +
            "starknife. If you do so, you don’t modify attack rolls and damage rolls with your " +
            "starknife with your Strength modifier, your Dexterity modifier (if you have Weapon Finesse), " +
            "or any other ability score (if you have an ability that allows you to modify attack rolls and " +
            "damage rolls with that ability score).";
        private static readonly string wayOfTheShootingStarDescriptionKey = "WayOfTheShootingStarDescription";

        private static readonly string BardTalentSelection = "94e2cd84bf3a8e04f8609fe502892f4f";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { PatchWayOfTheShootingStar(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }
            public static void PatchWayOfTheShootingStar()
            {
                string starknifeGUID = "5a939137fc039084580725b2b0845c3f";
                BlueprintWeaponTypeReference starknifeType = ResourcesLibrary.TryGetBlueprint<BlueprintWeaponType>(starknifeGUID).ToReference<BlueprintWeaponTypeReference>();
                string desnaGUID = "2c0a3b9971327ba4d9d85354d16998c1";
                string bardGUID = "772c83a25e2268e448e841dcd548235f";

                FeatureConfigurator.New(wayOfTheShootingStarFeatName, wayOfTheShootingStarFeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(wayOfTheShootingStarDisplayNameKey, wayOfTheShootingStarDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(wayOfTheShootingStarDescriptionKey, wayOfTheShootingStarDescription))
                    .AddFeatureTagsComponent(FeatureTag.Attack)
                    .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .AddAttackStatReplacement(
                        replacementStat: StatType.Charisma,
                        subCategory: WeaponSubCategory.Melee,
                        checkWeaponTypes: true,
                        weaponTypes: new() { starknifeType })
                    .AddWeaponTypeDamageStatReplacement(stat: StatType.Charisma, category: WeaponCategory.Starknife)
                    .AddPrerequisiteFeature(desnaGUID)
                    .AddPrerequisiteClassLevel(bardGUID, 2)
                    .AddPrerequisiteAlignment(AlignmentMaskType.ChaoticGood)
                    .AddRecommendationStatComparison(
                        higherStat: StatType.Charisma, 
                        lowerStat: StatType.Strength, 
                        diff: 4)
                    .AddRecommendationHasFeature(desnaGUID)
                    .AddRecommendationWeaponTypeFocus(weaponRangeType: WeaponRangeType.Melee)
                    .Configure();

                if (Main.Settings.useWayOfTheShootingStar == false) { return; }
                FeatureSelectionConfigurator.For(BardTalentSelection).AddToAllFeatures(wayOfTheShootingStarFeatName).Configure();
            }
        }
    }
}
