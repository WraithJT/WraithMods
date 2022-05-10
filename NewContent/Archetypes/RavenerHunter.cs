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
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Designers.Mechanics.Facts;

namespace WraithMods.NewContent.Archetypes
{
    class RavenerHunter
    {
        private static readonly string RavenerHunterName = "RavenerHunter";
        private static readonly string RavenerHunterGUID = "4143FA32-E05B-4EF4-A4AF-519022F5E99E";
        private static readonly string RavenerHunterDisplayName = "Ravener Hunter";
        private static readonly string RavenerHunterDisplayNameKey = "RavenerHunterNameKey";
        private static readonly string RavenerHunterDescription =
            "Throughout the Mwangi Expanse, cults of Angazhan pollute the pristine jungle " +
            "with demonic influence and wanton bloodshed. For generations, the catfolk of " +
            "Murraseth have viewed such faiths with loathing and hatred, and they believe it is their " +
            "sacred duty to hunt down the followers of the Ravener King and expel them from the Material Plane.";
        private static readonly string RavenerHunterDescriptionKey = "RavenerHunterDescriptionKey";

        private static readonly string ChargedByNatureName = "ChargedByNature";
        private static readonly string ChargedByNatureGUID = "82696B1A-1BA2-4532-A603-6CF0F85B44F8";
        private static readonly string ChargedByNatureDisplayName = "Charged By Nature";
        private static readonly string ChargedByNatureDisplayNameKey = "ChargedByNatureNameKey";
        private static readonly string ChargedByNatureDescription =
            "Rather than having a deity patron, a ravener hunter is charged by spirits to " +
            "eradicate evil wherever it appears. At 1st level, a ravener hunter chooses an oracle mystery from the " +
            "following list: ancestor, battle, bones, flame, life, nature, stone, waves, or wind. " +
            "She gains one revelation from her chosen mystery. She must meet the revelation’s prerequisites, " +
            "using her inquisitor level as her effective oracle level to determine the revelation’s effects, and she " +
            "never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
            "chosen mystery at 8th level.";
        private static readonly string ChargedByNatureDescriptionKey = "ChargedByNatureDescriptionKey";

        private static readonly string HolyMagicName = "HolyMagic";
        private static readonly string HolyMagicGUID = "9090A30C-F193-4AE3-AC39-F93CBC19534C";
        private static readonly string HolyMagicDisplayName = "Holy Magic";
        private static readonly string HolyMagicDisplayNameKey = "HolyMagicNameKey";
        private static readonly string HolyMagicDescription =
            "At 3rd level, a ravener hunter gains Demon Hunter as a bonus feat, ignoring its " +
            "prerequisites. She also gains a +2 morale bonus on attack rolls and caster level " +
            "checks to overcome spell resistance of worshipers of any deity with the Demon subdomain.";
        private static readonly string HolyMagicDescriptionKey = "HolyMagicDescriptionKey";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { AddRavenerHunter(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            private static readonly BlueprintFeature OracleMystery = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("5531b975dcdf0e24c98f1ff7e017e741");
            private static readonly BlueprintFeature DemonHunter = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("8976620F-6B3D-4B34-A578-4FAD79B9881E");
            private static readonly BlueprintFeature SoloTactics = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
            private static readonly BlueprintFeature TeamworkFeat = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("d87e2f6a9278ac04caeb0f93eff95fcb");
            private static readonly BlueprintFeature OracleRevelation1 = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("60008a10ad7ad6543b1f63016741a5d2");
            private static readonly BlueprintFeature OracleRevelation8 = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("60008a10ad7ad6543b1f63016741a5d2");
            private static readonly BlueprintFeature Domain = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("48525e5da45c9c243a343fc6545dbdb9");

            private static readonly BlueprintCharacterClass InquisitorClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");

            private static readonly string AreshkagalFeature = "d714ecb5d5bb89a42957de0304e459c9";
            private static readonly string BaphometFeature = "bd72ca8ffcfec5745899ac56c93f12c5";
            private static readonly string DeskariFeature = "ddf913858bdf43b4da3b731e082fbcc0";
            private static readonly string KabririFeature = "f12c1ccc9d600c04f8887cd28a8f45a5";

            static void AddRavenerHunter()
            {
                var HolyMagic = FeatureConfigurator.New(HolyMagicName, HolyMagicGUID)
                    .SetDisplayName(LocalizationTool.CreateString(HolyMagicDisplayNameKey, HolyMagicDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(HolyMagicDescriptionKey, HolyMagicDescription))
                    .AddAttackBonusAgainstFactOwner(
                        checkedFact: AreshkagalFeature,
                        attackBonus: 2,
                        descriptor: ModifierDescriptor.UntypedStackable)
                    .AddAttackBonusAgainstFactOwner(
                        checkedFact: BaphometFeature,
                        attackBonus: 2,
                        descriptor: ModifierDescriptor.UntypedStackable)
                    .AddAttackBonusAgainstFactOwner(
                        checkedFact: DeskariFeature,
                        attackBonus: 2,
                        descriptor: ModifierDescriptor.UntypedStackable)
                    .AddAttackBonusAgainstFactOwner(
                        checkedFact: KabririFeature,
                        attackBonus: 2,
                        descriptor: ModifierDescriptor.UntypedStackable)
                    .Configure();

                HolyMagic.AddComponent<AttackBonusAgainstFactOwner>(c =>
                {
                    c.AttackBonus = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Bonus = 2;
                    c.m_CheckedFact = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(BaphometFeature).ToReference<BlueprintUnitFactReference>();
                });

                var ravenerHunterArchetype = ArchetypeConfigurator.New(RavenerHunterName, RavenerHunterGUID)
                    .SetDisplayName(LocalizationTool.CreateString(RavenerHunterDisplayNameKey, RavenerHunterDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(RavenerHunterDescriptionKey, RavenerHunterDescription))
                    .Configure();
                ravenerHunterArchetype.RemoveFeatures = new LevelEntry[]
                {
                    Tools.CreateLevelEntry(1, Domain),
                    Tools.CreateLevelEntry(3, SoloTactics),
                    Tools.CreateLevelEntry(3, TeamworkFeat)
                };
                ravenerHunterArchetype.AddFeatures = new LevelEntry[]
                {
                    Tools.CreateLevelEntry(1, OracleMystery),
                    Tools.CreateLevelEntry(1, OracleRevelation1),
                    Tools.CreateLevelEntry(3, HolyMagic),
                    Tools.CreateLevelEntry(3, DemonHunter),
                    Tools.CreateLevelEntry(6, SoloTactics),
                    Tools.CreateLevelEntry(8, OracleRevelation8)
                };

                BlueprintProgression OracleRevelationBondedMountProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("7d1c29c3101dd7643a625448fbbaa919");
                BlueprintProgression.ClassWithLevel classWithLevel = new();
                classWithLevel.m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                classWithLevel.AdditionalLevel = 0;
                OracleRevelationBondedMountProgression.m_Classes = OracleRevelationBondedMountProgression.m_Classes.AppendToArray(classWithLevel);

                //if (Main.Settings.useRavenerHunter == false) { return; }
                InquisitorClass.m_Archetypes = InquisitorClass.m_Archetypes.AppendToArray(ravenerHunterArchetype.ToReference<BlueprintArchetypeReference>());

                InquisitorClass.Progression.UIGroups = InquisitorClass.Progression.UIGroups.AppendToArray(
                    Tools.CreateUIGroup(
                        OracleMystery,
                        OracleRevelation1,
                        OracleRevelation8)
                    );
                InquisitorClass.Progression.UIGroups = InquisitorClass.Progression.UIGroups.AppendToArray(
                    Tools.CreateUIGroup(
                        HolyMagic,
                        DemonHunter)
                );

                //configure Oracle Revelation scaling
            }
        }
    }
}
