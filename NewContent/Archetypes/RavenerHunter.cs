using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.UnitLogic;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Customization;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Blueprints.Configurators.EntitySystem;
using System;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Utility;
using System.Linq;
using WraithMods.Utilities;
using Kingmaker.EntitySystem;
using Kingmaker.UnitLogic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace WraithMods.NewContent.Archetypes
{
    class RavenerHunter
    {
        private static readonly string RavenerHunterName = "RavenerHunter";
        private static readonly string RavenerHunterDisplayName = "Ravener Hunter";
        private static readonly string RavenerHunterDisplayNameKey = "RavenerHunterNameKey";
        private static readonly string RavenerHunterDescription = 
            "Throughout the Mwangi Expanse, cults of Angazhan pollute the pristine jungle " +
            "with demonic influence and wanton bloodshed. For generations, the catfolk of " +
            "Murraseth have viewed such faiths with loathing and hatred, and they believe it is their " +
            "sacred duty to hunt down the followers of the Ravener King and expel them from the Material Plane.";
        private static readonly string RavenerHunterDescriptionKey = "RavenerHunterDescriptionKey";
        private static readonly string RavenerHunterGUID = "4143FA32-E05B-4EF4-A4AF-519022F5E99E";

        private static readonly string ChargedByNatureName = "ChargedByNature";
        private static readonly string ChargedByNatureGUID = "F297FD90-0D57-40B9-84B4-C2DD71FAEBFB";
        private static readonly string ChargedByNatureDisplayName = "Charged By Nature";
        private static readonly string ChargedByNatureDisplayNameKey = "ChargedByNatureName";
        private static readonly string ChargedByNatureDescription =
            "Rather than having a deity patron, a ravener hunter is charged by spirits to " +
            "eradicate evil wherever it appears. At 1st level, a ravener hunter chooses an oracle mystery from the " +
            "following list: ancestor, battle, bones, flame, life, nature, stone, waves, or wind. " +
            "She gains one revelation from her chosen mystery. She must meet the revelation’s prerequisites, " +
            "using her inquisitor level as her effective oracle level to determine the revelation’s effects, and she " +
            "never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
            "chosen mystery at 8th level.";
        private static readonly string ChargedByNatureDescriptionKey = "ChargedByNatureDescription";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { AddRavenerHunter(); }
                catch (Exception ex) { Main.logger.Log(ex.ToString()); }
            }

            private static readonly BlueprintFeature OracleMystery = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("5531b975dcdf0e24c98f1ff7e017e741");
            private static readonly BlueprintFeature DemonHunter = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("5531b975dcdf0e24c98f1ff7e017e741");
            private static readonly BlueprintFeature SoloTactics = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
            private static readonly BlueprintFeature TeamworkFeat = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("d87e2f6a9278ac04caeb0f93eff95fcb");
            private static readonly BlueprintFeature OracleRevelation = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("60008a10ad7ad6543b1f63016741a5d2");
            private static readonly BlueprintFeature Domain = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("48525e5da45c9c243a343fc6545dbdb9");

            private static readonly BlueprintCharacterClass InquisitorClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");

            static void AddRavenerHunter()
            {
                ArchetypeConfigurator.New(RavenerHunterName, RavenerHunterGUID)
                    .SetDisplayName(LocalizationTool.CreateString(RavenerHunterDisplayNameKey, RavenerHunterDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(RavenerHunterDescriptionKey, RavenerHunterDescription))
                    .Configure();

                FeatureConfigurator.New(ChargedByNatureName, ChargedByNatureGUID)
                    .SetDisplayName(LocalizationTool.CreateString(ChargedByNatureDisplayNameKey, ChargedByNatureDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(ChargedByNatureDescriptionKey, ChargedByNatureDescription))
                    .SetIcon(OracleMystery.Icon)
                    .Configure();
                var chargedByNatureFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(ChargedByNatureGUID);
                chargedByNatureFeature.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleMystery.ToReference<BlueprintUnitFactReference>()};
                });

                var ravenerHunterArchetype = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(RavenerHunterGUID);
                ravenerHunterArchetype.RemoveFeatures = new LevelEntry[]
                {
                    Tools.CreateLevelEntry(1, Domain)
                };
                ravenerHunterArchetype.AddFeatures = new LevelEntry[]
                {
                    Tools.CreateLevelEntry(1, chargedByNatureFeature)
                };
                

                //if (Main.Settings.useRavenerHunter == false) { return; }
                InquisitorClass.m_Archetypes = InquisitorClass.m_Archetypes.AppendToArray(ravenerHunterArchetype.ToReference<BlueprintArchetypeReference>());

                InquisitorClass.Progression.UIGroups = InquisitorClass.Progression.UIGroups.AppendToArray(
                    Tools.CreateUIGroup(chargedByNatureFeature)
                    );
            }
        }
    }
}
