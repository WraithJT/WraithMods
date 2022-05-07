using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using System;

namespace WraithMods.NewContent.Feats
{
    class DemonHunter
    {
        private static readonly string FeatName = "DemonHunter";
        private static readonly string FeatGuid = "8976620F-6B3D-4B34-A578-4FAD79B9881E";
        private static readonly string BasicFeatSelectionGuid = "247a4068-296e-8be4-2890-143f451b4b45";

        private static readonly string DisplayName = "Demon Hunter";
        private static readonly string DisplayNameKey = "DemonHunterName";
        private static readonly string Description =
            "You gain a +2 morale bonus on all attack rolls made against creatures with the demon subtype " +
            "and a +1 morale bonus on caster level checks to penetrate spell resistance.";
        private static readonly string DescriptionKey = "DemonHunterDescription";

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
                    //PatchDemonHunter();
                }
                catch (Exception ex)
                {
                    Main.logger.Log(ex.ToString());
                }
            }
            public static void PatchDemonHunter()
            {
                if (Main.Settings.useDemonHunter == false)
                {
                    return;
                }

                string subtypeDemon = "dc960a234d365cb4f905bdc5937e623a";

                FeatureConfigurator.New(FeatName, FeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .SetFeatureTags(FeatureTag.Attack, FeatureTag.Magic)
                    .SetFeatureGroups(FeatureGroup.Feat)
                    .AddAttackBonusAgainstFactOwner(attackBonus: 2, descriptor: Kingmaker.Enums.ModifierDescriptor.Morale, checkedFact: subtypeDemon)
                    .AddSpellPenetrationBonus(value: 2, descriptor: Kingmaker.Enums.ModifierDescriptor.Morale)
                    .Configure();

                FeatureSelectionConfigurator.For(BasicFeatSelectionGuid).AddToFeatures(FeatName).Configure();

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
