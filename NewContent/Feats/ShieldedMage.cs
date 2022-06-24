using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using System;
using WraithMods.Utilities;

namespace WraithMods.NewContent.Feats
{
    class ShieldedMage
    {
        private static readonly string FeatName = "ShieldedMage";
        private static readonly string FeatGuid = "0DFAA699-C94C-430A-A00E-04DE6723B609";
        private static readonly string DisplayName = "Shielded Mage";
        private static readonly string DisplayNameKey = "ShieldedMageName";
        private static readonly string Description =
            "You reduce the arcane spell failure of any shield you use by 15% (to a minimum of 0%).";
        private static readonly string DescriptionKey = "ShieldedMageDescription";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchShieldedMage();
                //try { PatchShieldedMage(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }
            public static void PatchShieldedMage()
            {
                string ShieldFocus = "ac57069b6bf8c904086171683992a92a";
                string FighterClass = "48ac8db94d5de7645906c7d0ad3bcfbd";
                var ShieldedMageFeat  = FeatureConfigurator.New(FeatName, FeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddFeatureTagsComponent(FeatureTag.Attack)
                    .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .AddArcaneSpellFailureIncrease(bonus: -15, toShield: true)
                    .AddPrerequisiteFeature(ShieldFocus, group: Prerequisite.GroupType.All)
                    .AddPrerequisiteStatValue(stat: StatType.BaseAttackBonus, value: 3, group: Prerequisite.GroupType.Any)
                    .AddPrerequisiteClassLevel(characterClass: FighterClass, level: 1, group: Prerequisite.GroupType.Any)
                    .Configure();

                //ShieldedMageFeat.AddPrerequisite<PrerequisiteStatValue>(c =>
                //{
                //    c.Stat = StatType.SkillLoreReligion;
                //    c.Value = 6;
                //});



                if (Main.Settings.useShieldedMage == false) { return; }
                Tools.AddAsFeat(ShieldedMageFeat);
                //FeatureSelectionConfigurator.For(BasicFeatSelectionGuid).AddToFeatures(FeatName).Configure();

                #region testing non BlueprintCore
                //BlueprintFeature demonHunter = new();
                //demonHunter.m_DisplayName = LocalizationTool.CreateString(DisplayNameKey, DisplayName);
                //demonHunter.m_Description = LocalizationTool.CreateString(DescriptionKey, Description);

                //AttackBonusAgainstFactOwner ab = new AttackBonusAgainstFactOwner();
                //ab.AttackBonus = 2;
                //ab.m_CheckedFact = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(subtypeDemon).ToReference<BlueprintUnitFactReference>();
                //SpellPenetrationBonus sp = new();
                //sp.Value = 2;
                //sp.m_RequiredFact = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(subtypeDemon).ToReference<BlueprintUnitFactReference>();
                //sp.CheckFact = true;

                //demonHunter.AddComponents(ab, sp);


                //var featSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(BasicFeatSelectionGuid);
                //var featuresArray = featSelection.m_Features;
                //var allFeaturesArray = featSelection.AllFeatures;
                //featSelection.m_Features = featuresArray.AddItem<BlueprintFeatureReference>(demonHunter.ToReference<BlueprintFeatureReference>());

                //featSelection.Features.AddItem(demonHunter);
                //featSelection.AllFeatures.AddItem(demonHunter);
                #endregion
            }
        }
    }
}
