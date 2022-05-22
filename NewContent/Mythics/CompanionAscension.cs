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
using Kingmaker.Blueprints.Classes.Prerequisites;
using System;
using WraithMods.Utilities;
using BlueprintCore.Blueprints.Configurators.UnitLogic;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Customization;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Blueprints.Configurators.EntitySystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Utility;
using System.Linq;
using Kingmaker.EntitySystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace WraithMods.NewContent.Mythics
{
    class CompanionAscension
    {
        private static readonly string CompanionAscensionChoice4Name = "AscensionChoice4";
        private static readonly string CompanionAscensionChoice4GUID = "9C0B58DB-BF81-4FB6-AFD6-AABC187700A0";
        private static readonly string CompanionAscensionChoice4DisplayName = "Companion Ascension";
        private static readonly string CompanionAscensionChoice4DisplayNameKey = "AscensionChoice4Name";
        private static readonly string CompanionAscensionChoice4Description = "Select one extra feat, mythic ability, or mythic feat.";
        private static readonly string CompanionAscensionChoice4DescriptionKey = "AscensionChoice4Description";

        private static readonly string CompanionFirstAscensionName = "CompanionFirstAscension";
        private static readonly string CompanionFirstAscensionGUID = "25383B4C-DBBD-4961-9BE7-67BB4EAB2538";
        private static readonly string CompanionFirstAscensionDisplayName = "Ascension";
        private static readonly string CompanionFirstAscensionDisplayNameKey = "CompanionFirstAscensionName";
        private static readonly string CompanionFirstAscensionDescription = "As the Commander's power grows, so too does the power of {mf|his|her} companions.";
        private static readonly string CompanionFirstAscensionDescriptionKey = "CompanionFirstAscensionDescription";
        //private static readonly string FirstAscensionSelectionGUID = "1421e0034a3afac458de08648d06faf0";
        private static readonly BlueprintFeatureSelection FirstAscensionSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("1421e0034a3afac458de08648d06faf0");
        //private static readonly string RemoveDiseaseGUID = "4093d5a0eb5cae94e909eb1e0e1a6b36";
        private static readonly BlueprintAbility RemoveDisease = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("4093d5a0eb5cae94e909eb1e0e1a6b36");
        private static readonly BlueprintAbility Guidance = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("c3a8f31778c3980498d8f00c980be5f5");

        private static readonly string CompanionAscensionMythicAbilityName = "CompanionAscensionMythicAbility";
        private static readonly string CompanionAscensionMythicAbilityGUID = "9AA7C781-2275-4A7E-AE91-2E51A7816DC3";
        private static readonly string CompanionAscensionMythicAbilityDisplayName = "Mythic Ability";
        private static readonly string CompanionAscensionMythicAbilityDisplayNameKey = "CompanionAscensionMythicAbilityName";
        private static readonly string CompanionAscensionMythicAbilityDescription = "Select one new mythic ability.";
        private static readonly string CompanionAscensionMythicAbilityDescriptionKey = "CompanionAscensionMythicAbilityDescription";

        private static readonly string CompanionAscensionMythicFeatName = "CompanionAscensionMythicFeat";
        private static readonly string CompanionAscensionMythicFeatGUID = "FBD47BD7-F39C-4320-9CE0-1AA415656795";
        private static readonly string CompanionAscensionMythicFeatDisplayName = "Mythic Feat";
        private static readonly string CompanionAscensionMythicFeatDisplayNameKey = "CompanionAscensionMythicFeatName";
        private static readonly string CompanionAscensionMythicFeatDescription = "Select one new mythic feat.";
        private static readonly string CompanionAscensionMythicFeatDescriptionKey = "CompanionAscensionMythicFeatDescription";

        private static readonly string Aeon = "";
        private static readonly string AngelMythicClass = "a5a9fe8f663d701488bd1db8ea40484e";
        private static readonly string Azata = "";
        private static readonly string Demon = "";
        private static readonly string Lich = "";
        private static readonly string Trickster = "";
        private static readonly string Devil = "";
        private static readonly string GoldDragon = "";
        //LegendFeature: 7f99961610390044391f300c7ef5e0c8
        private static readonly string Legend = "";
        private static readonly string Swarm = "";

        private static readonly string ExtraMythicAbilityMythicFeatSelectionGUID = "8a6a511c55e67d04db328cc49aaad2b8";
        private static readonly string ExtraMythicFeatSelectionGUID = "e10c4f18a6c8b4342afe6954bde0587b";
        private static readonly string ExtraMythicFeatMythicAbilitySelectionGUID = "c916448f690d4f4e9d824d6f376e621d";
        private static readonly string BasicFeatSelectionGUID = "247a4068296e8be42890143f451b4b45";
        private static readonly string MythicAbilitySelectionGUID = "ba0e5a900b775be4a99702f1ed08914d";
        private static readonly string MythicFeatSelectionGUID = "9ee0f6745f555484299b0a1563b99d81";

        //private static readonly string MythicFeatSelectionGUID = "9ee0f6745f555484299b0a1563b99d81";
        //private static readonly BlueprintFeature MythicFeatSelectionSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(MythicFeatSelectionGUID);

        private static readonly BlueprintCharacterClass MythicCompanionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");
        private static readonly BlueprintProgression MythicCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");
        private static readonly BlueprintFeature ExtraMythicAbilityMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(ExtraMythicAbilityMythicFeatSelectionGUID);
        private static readonly BlueprintFeature ExtraMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(ExtraMythicFeatSelectionGUID);
        private static readonly BlueprintFeature ExtraMythicFeatMythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(ExtraMythicFeatMythicAbilitySelectionGUID);
        //private static readonly BlueprintFeature BasicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(BasicFeatSelectionGUID);
        private static readonly BlueprintFeatureSelection MythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(MythicAbilitySelectionGUID);
        private static readonly BlueprintFeatureSelection MythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(MythicFeatSelectionGUID);

        private static readonly string AzataSuperpowersGUID = "8a30e92cd04ff5b459ba7cb03584fda0";
        private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(AzataSuperpowersGUID);
        private static readonly string AzataCompanionChoiceName = "AzataCompanionChoice";
        private static readonly string AzataCompanionChoiceGUID = "B2229818-16B7-478F-B423-90D219947162";
        private static readonly string AzataCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string AzataCompanionChoiceDisplayNameKey = "AzataCompanionChoiceName";
        private static readonly string AzataCompanionChoiceDescription = "Select one Azata Superpower.";
        private static readonly string AzataCompanionChoiceDescriptionKey = "AzataCompanionChoiceDescription";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        [HarmonyPriority(Priority.HigherThanNormal)]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //try { PatchCompanionAscension(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }
            public static void PatchCompanionAscension()
            {
                // MR 4 and 8
                // PrerequisitePlayerHasFeature
                // HideNotAvailibleInUI

                //      Ascension 2 (level 8): Give Alignment bypass?
                //      MythicIgnoreAlignmentRestrictions - 24e78475f0a243e1a810452d14d0a1bd

                //      Legend: boost max level somehow?

                //      Lich: DeathOfElementsConsumingElementsResource
                //      add companion class
                //      book merge for arcanes?
                //      
                //      Gold Dragon: Choice of +4 to one ability? Choice of boosting saves?
                //      
                //      Angel: book merge for divines?



                var CompanionFirstAscension = FeatureSelectionConfigurator.New(CompanionFirstAscensionName, CompanionFirstAscensionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionFirstAscensionDisplayNameKey, CompanionFirstAscensionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionFirstAscensionDescriptionKey, CompanionFirstAscensionDescription))
                    .SetIcon(Guidance.Icon)
                    .Configure();
                CompanionFirstAscension.m_AllFeatures = FirstAscensionSelection.m_AllFeatures;

                var CAMythicAbility = FeatureSelectionConfigurator.New(CompanionAscensionMythicAbilityName, CompanionAscensionMythicAbilityGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionMythicAbilityDisplayNameKey, CompanionAscensionMythicAbilityDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionMythicAbilityDescriptionKey, CompanionAscensionMythicAbilityDescription))
                    .SetIcon(RemoveDisease.Icon)
                    .Configure();
                CAMythicAbility.m_AllFeatures = MythicAbilitySelection.m_AllFeatures.Where(c => (
                    c.deserializedGuid != ExtraMythicFeatMythicAbilitySelection.ToReference<BlueprintFeatureReference>().deserializedGuid
                )).ToArray();

                var CAMythicFeat = FeatureSelectionConfigurator.New(CompanionAscensionMythicFeatName, CompanionAscensionMythicFeatGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionMythicFeatDisplayNameKey, CompanionAscensionMythicFeatDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionMythicFeatDescriptionKey, CompanionAscensionMythicFeatDescription))
                    .SetIcon(ExtraMythicFeatSelection.Icon)
                    .Configure();
                CAMythicFeat.m_AllFeatures = MythicFeatSelection.m_AllFeatures.Where(c => (
                    c.deserializedGuid != ExtraMythicAbilityMythicFeatSelection.ToReference<BlueprintFeatureReference>().deserializedGuid &&
                    c.deserializedGuid != ExtraMythicFeatSelection.ToReference<BlueprintFeatureReference>().deserializedGuid
                )).ToArray();

                var AzataCompanionChoice = FeatureSelectionConfigurator.New(AzataCompanionChoiceName, AzataCompanionChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(AzataCompanionChoiceDisplayNameKey, AzataCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(AzataCompanionChoiceDescriptionKey, AzataCompanionChoiceDescription))
                    .Configure();
                AzataCompanionChoice.m_AllFeatures = AzataSuperpowersSelection.m_AllFeatures;

                var CompanionAscensionChoice4 = FeatureSelectionConfigurator.New(CompanionAscensionChoice4Name, CompanionAscensionChoice4GUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionChoice4DisplayNameKey, CompanionAscensionChoice4DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionChoice4DescriptionKey, CompanionAscensionChoice4Description))
                    .SetFeatureGroups(FeatureGroup.MythicAbility, FeatureGroup.MythicFeat, FeatureGroup.Feat, FeatureGroup.MythicAdditionalProgressions)
                    .SetIcon(MythicAbilitySelection.Icon)
                    .AddToFeatures(CompanionAscensionMythicFeatGUID)
                    .AddToFeatures(CompanionAscensionMythicAbilityGUID)
                    .AddToFeatures(BasicFeatSelectionGUID)
                    .AddToFeatures(AzataCompanionChoiceGUID)
                    .Configure();



                //          Dual Path not showing up in Ascension Ability selection

                //configure first ascension selections


                //FeatureSelectionConfigurator.For(CompanionAscensionChoice4GUID)
                //    .AddToFeatures(ExtraMythicAbilityMythicFeatSelectionGUID)
                //    .AddToFeatures(ExtraMythicFeatSelectionGUID)
                //    .AddToFeatures(ExtraMythicFeatMythicAbilitySelectionGUID)
                //    .AddToFeatures(BasicFeatSelectionGUID)
                //    .Configure();

                //LevelEntry companionAscensionChoice8 = new();

                //var companionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(MythicCompanionProgression);
                //companionClass.Progression

                //BlueprintFeatureSelection blueprintFeatureSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FeatGuid);
                //blueprintFeatureSelection.m_Features = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FirstAscensionSelection).m_AllFeatures;
                //blueprintFeatureSelection.m_AllFeatures = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FirstAscensionSelection).m_AllFeatures;

                if (Main.Settings.useCompanionAscension == false) { return; }
                AddCompanionsToFirstAscensions();
                MythicCompanionProgression.LevelEntries.TemporaryContext(le =>
                {
                    le.Where(e => e.Level == 4)
                        .ForEach(e =>
                        {
                            e.m_Features.Remove(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(CompanionFirstAscension.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(CompanionAscensionChoice4.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());
                        });
                });
                MythicCompanionProgression.UIGroups = MythicCompanionProgression.UIGroups.AppendToArray(
                    Tools.CreateUIGroup(
                    MythicAbilitySelection,
                    CompanionAscensionChoice4)
                );
            }

            static void AddCompanionsToFirstAscensions()
            {
                string[] ascensionGUIDS = new string[] {
                    "46df0532714a9454eb5fbad64ce6c14f",     //Aeon
                    "b236003be2b9400498b9dd0f07b0c93c",     //Angel
                    "132afc1a28bd9d442821385f7cbf1c05",     //Azata
                    "8afd697daf0d47a4883759a6bc1aff88",     //Demon
                    "a796af14198bf5e45b63f056c32107a2",     //Lich
                    "11ba180ac736c894e937602e54f7320c"};    //Trickster

                var mythicCompanionClassReference = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();
                BlueprintProgression.ClassWithLevel classWithLevel = new();
                classWithLevel.m_Class = mythicCompanionClassReference;
                classWithLevel.AdditionalLevel = 0;

                foreach (string s in ascensionGUIDS)
                {
                    var feature = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>(s);
                    feature.m_Classes = feature.m_Classes.AppendToArray(classWithLevel);
                    feature.GiveFeaturesForPreviousLevels = true;
                }

                var tricksterFirstAscensionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("918e6e7085d81094790e806a49922694");
                BlueprintComponent[] contextRankConfig = tricksterFirstAscensionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    c.m_Class = c.m_Class.AppendToArray(mythicCompanionClassReference);
                }

                var aeonFirstAscentionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("4db463bcf37d6014eaa23d3219703a9b");
                aeonFirstAscentionResource.m_MaxAmount.m_Class = aeonFirstAscentionResource.m_MaxAmount.m_Class.AppendToArray(mythicCompanionClassReference);

                var angelFirstAscentionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("6da18ecb21a24814eb79ab075a0b6d5e");
                angelFirstAscentionResource.m_MaxAmount.m_ClassDiv = angelFirstAscentionResource.m_MaxAmount.m_ClassDiv.AppendToArray(mythicCompanionClassReference);
                var angelFirstAscentionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("f86857af1c584e248b9284654f31d39c");
                contextRankConfig = angelFirstAscentionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    c.m_Class = c.m_Class.AppendToArray(mythicCompanionClassReference);
                }

                var azataFirstAscensionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("8419c285e922c1044893472bcbd3d3bf");
                azataFirstAscensionResource.m_MaxAmount.m_ClassDiv = azataFirstAscensionResource.m_MaxAmount.m_ClassDiv.AppendToArray(mythicCompanionClassReference);
                var azataFirstAscensionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("5d5f0c9b274bab44eb01272c8fcf251d");
                contextRankConfig = azataFirstAscensionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    c.m_Class = c.m_Class.AppendToArray(mythicCompanionClassReference);
                }

                var lichChannelNegativeResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("e5ef1aae31818f041bccbc9fd37662bf");
                lichChannelNegativeResource.m_MaxAmount.m_Class = lichChannelNegativeResource.m_MaxAmount.m_Class.AppendToArray(mythicCompanionClassReference);
            }
        }
    }
}
