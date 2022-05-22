using BlueprintCore.Blueprints.Configurators.Classes;
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
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Blueprints.Items.Weapons;

namespace WraithMods.NewContent.Feats
{
    class BladedBrush
    {
        private static readonly string bladedBrushFeatName = "BladedBrush";
        private static readonly string bladedBrushFeatGuid = "839728EE-82DB-4090-A0C6-C58131C405F8";
        private static readonly string bladedBrushDisplayName = "Bladed Brush";
        private static readonly string bladedBrushDisplayNameKey = "BladedBrushName";
        private static readonly string bladedBrushDescription =
            "You can use the Weapon Finesse feat to apply your Dexterity modifier instead of your " +
            "Strength modifier to attack rolls with a glaive sized for you, even though it isn’t a light weapon. " +
            "When wielding a glaive, you can treat it as a one-handed piercing or slashing melee weapon and as if " +
            "you were not making attacks with your off-hand for all feats and class abilities that require such a " +
            "weapon (such as a duelist’s precise strike).";
        private static readonly string bladedBrushDescriptionKey = "BladedBrushDescription";

        private static readonly string sgbladedBrushFeatName = "SlashingGrace_bb";
        private static readonly string sgbladedBrushFeatGuid = "92CBD999-28C5-4BD2-BF28-650F2344254D";
        private static readonly string sgbladedBrushDisplayName = "Slashing Grace (Bladed Brush)";
        private static readonly string sgbladedBrushDisplayNameKey = "SlashingGraceBBName";
        private static readonly string sgbladedBrushDescription =
            "You can stab your enemies with your glaive.\nBenefit: When wielding a glaive, you can treat it " +
            "as a one-handed piercing melee weapon for all feats and class abilities that require such a " +
            "weapon(such as a duelist's precise strike) and you can add your Dexterity modifier instead of your " +
            "Strength modifier to the glaive's damage. The glaive must be one appropriate for your size.";
        private static readonly string sgbladedBrushDescriptionKey = "SlashingGraceBBDescription";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        [HarmonyPriority(Priority.First)]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try
                {
                    PatchBladedBrush();
                }
                catch (Exception ex)
                {
                    Tools.LogMessage("EXCEPTION: " + ex.ToString());
                }
            }
            public static void PatchBladedBrush()
            {
                string glaiveGUID = "7a14a1b224cd173449cb7ffc77d5f65c";
                BlueprintWeaponTypeReference glaiveType = ResourcesLibrary.TryGetBlueprint<BlueprintWeaponType>(glaiveGUID).ToReference<BlueprintWeaponTypeReference>();
                string shelynGUID = "b382afa31e4287644b77a8b30ed4aa0b";
                string weaponFocusGUID = "1e1f627d26ad36f43bbd26cc2bf8ac7e";

                FeatureConfigurator.New(bladedBrushFeatName, bladedBrushFeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(bladedBrushDisplayNameKey, bladedBrushDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(bladedBrushDescriptionKey, bladedBrushDescription))
                    .AddFeatureTagsComponent(FeatureTag.Attack)
                    .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .AddAttackStatReplacement(
                        replacementStat: StatType.Dexterity,
                        subCategory: WeaponSubCategory.Melee,
                        checkWeaponTypes: true,
                        weaponTypes: new() { glaiveType })
                    .AddPrerequisiteFeature(shelynGUID)
                    .AddPrerequisiteParametrizedWeaponFeature(weaponFocusGUID, WeaponCategory.Glaive)
                    .AddRecommendationStatComparison(
                        higherStat: StatType.Dexterity, 
                        lowerStat: StatType.Strength, 
                        diff: 4)
                    .AddRecommendationHasFeature(shelynGUID)
                    .AddRecommendationWeaponTypeFocus(weaponRangeType: WeaponRangeType.Melee)
                    .Configure();
                
                var sgBladedBrush = FeatureConfigurator.New(sgbladedBrushFeatName, sgbladedBrushFeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(sgbladedBrushDisplayNameKey, sgbladedBrushDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(sgbladedBrushDescriptionKey, sgbladedBrushDescription))
                    .AddFeatureTagsComponent(FeatureTag.Attack)
                    .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .AddWeaponTypeDamageStatReplacement(
                        stat: StatType.Dexterity, 
                        category: WeaponCategory.Glaive, 
                        twoHandedBonus: true)
                    .AddDamageGrace()
                    .AddPrerequisiteFeature(bladedBrushFeatGuid)
                    .AddRecommendationHasFeature(bladedBrushFeatGuid)
                    .AddRecommendationStatComparison(
                        higherStat: StatType.Dexterity, 
                        lowerStat: StatType.Strength, 
                        diff: 4)
                    .Configure();

                if (Main.Settings.useBladedBrush == false) { return; }
                Tools.AddAsFeat(ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(bladedBrushFeatGuid));
                Tools.AddAsFeat(ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(sgbladedBrushFeatGuid));
            }
        }
    }
}
