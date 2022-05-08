using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.Abilities;
using BlueprintCore.Blueprints.Components;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using System;
using WraithMods.Utilities;

namespace WraithMods.NewContent.Feats
{
    class ChannelSmite
    {
        private static readonly string FeatName = "ChannelSmite";
        private static readonly string FeatGuid = "";
        private static readonly string DisplayName = "Channel Smite";
        private static readonly string DisplayNameKey = "ChannelSmiteName";
        private static readonly string Description =
            "Before you make a melee attack roll, you can choose to spend one use of your channel energy " +
            "ability as a swift action. If you channel positive energy and you hit an undead creature, that " +
            "creature takes an amount of additional damage equal to the damage dealt by your channel positive " +
            "energy ability. If you channel negative energy and you hit a living creature, that creature takes an " +
            "amount of additional damage equal to the damage dealt by your channel negative energy ability. Your " +
            "target can make a Will save, as normal, to halve this additional damage. If your attack misses, the " +
            "channel energy ability is still expended with no effect.";
        private static readonly string DescriptionKey = "ChannelSmiteDescription";

        private static readonly string BasicFeatSelectionGuid = "247a4068-296e-8be4-2890-143f451b4b45";


        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { //PatchChannelSmite();
                      }
                catch (Exception ex) { Main.logger.Log(ex.ToString()); }
            }
            public static void PatchChannelSmite()
            {


                string subtypeDemon = "dc960a234d365cb4f905bdc5937e623a";

                FeatureConfigurator.New(FeatName, FeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .SetFeatureTags(FeatureTag.Attack, FeatureTag.Magic)
                    .SetFeatureGroups(FeatureGroup.Feat)
                    .AddAttackBonusAgainstFactOwner(attackBonus: 2, descriptor: Kingmaker.Enums.ModifierDescriptor.Morale, checkedFact: subtypeDemon)
                    .AddSpellPenetrationBonus(value: 2, descriptor: Kingmaker.Enums.ModifierDescriptor.Morale)
                    .Configure();


                if (Main.Settings.useDemonHunter == false) { return; }
                Tools.AddAsFeat(ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(FeatGuid));
            }
        }
    }
}
