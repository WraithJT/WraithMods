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

namespace WraithMods.NewContent.Mythics
{
    class CompanionAscension
    {
        private static readonly string CompanionAscensionChoice4Name = "Ascension";
        private static readonly string CompanionAscensionChoice4GUID = "9C0B58DB-BF81-4FB6-AFD6-AABC187700A0";
        private static readonly string CompanionAscensionChoice4DisplayName = "Ascension";
        private static readonly string CompanionAscensionChoice4DisplayNameKey = "AscensionName";
        private static readonly string CompanionAscensionChoice4Description =
            "As the Commander's power grows, so too does the power of the companions.";
        private static readonly string CompanionAscensionChoice4DescriptionKey = "AscensionDescription";


        private static readonly string FirstAscensionSelection = "1421e0034a3afac458de08648d06faf0";

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

        private static readonly BlueprintCharacterClass MythicCompanionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");
        private static readonly BlueprintProgression MythicCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");
        private static readonly BlueprintFeature ExtraMythicAbilityMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(ExtraMythicAbilityMythicFeatSelectionGUID);
        private static readonly BlueprintFeature ExtraMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(ExtraMythicFeatSelectionGUID);
        private static readonly BlueprintFeature ExtraMythicFeatMythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(ExtraMythicFeatMythicAbilitySelectionGUID);
        private static readonly BlueprintFeature BasicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(BasicFeatSelectionGUID);

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                try { PatchCompanionAscension(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }
            public static void PatchCompanionAscension()
            {
                // MR 4 and 8
                // PrerequisitePlayerHasFeature
                // HideNotAvailibleInUI

                //DemonHunter.AddPrerequisite<PrerequisiteClassLevel>(c =>
                //{
                //    //c.Level = 3;
                //    c.m_CharacterClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(AngelMythicClass).ToReference<BlueprintCharacterClassReference>();
                //});



                var CompanionAscensionChoice4 = FeatureSelectionConfigurator.New(CompanionAscensionChoice4Name, CompanionAscensionChoice4GUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionChoice4DisplayNameKey, CompanionAscensionChoice4DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionChoice4DescriptionKey, CompanionAscensionChoice4Description))
                    .SetFeatureGroups(FeatureGroup.MythicAbility)
                    .SetHideInUi(true)
                    .Configure();

                FeatureSelectionConfigurator.For(CompanionAscensionChoice4GUID)
                    .AddToFeatures(ExtraMythicAbilityMythicFeatSelectionGUID)
                    .AddToFeatures(ExtraMythicFeatSelectionGUID)
                    .AddToFeatures(ExtraMythicFeatMythicAbilitySelectionGUID)
                    .AddToFeatures(BasicFeatSelectionGUID)
                    .Configure();

                //LevelEntry companionAscensionChoice8 = new();

                //var companionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(MythicCompanionProgression);
                //companionClass.Progression

                //BlueprintFeatureSelection blueprintFeatureSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FeatGuid);
                //blueprintFeatureSelection.m_Features = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FirstAscensionSelection).m_AllFeatures;
                //blueprintFeatureSelection.m_AllFeatures = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(FirstAscensionSelection).m_AllFeatures;

                if (Main.Settings.useCompanionAscension == false) { return; }
                //AddPrereqsToFirstAscensions();
                var companionProgression = MythicCompanionProgression;
                MythicCompanionProgression.LevelEntries.TemporaryContext(le =>
                {
                    le.Where(e => e.Level == 4).ForEach(e => e.m_Features.Add(CompanionAscensionChoice4.ToReference<BlueprintFeatureBaseReference>()));
                });

            }

            static void AddPrereqsToFirstAscensions()
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
                    PrerequisiteNoFeature prerequisite = new();
                    prerequisite.m_Feature = feature.ToReference<BlueprintFeatureReference>();
                    prerequisite.Group = Prerequisite.GroupType.All;
                    prerequisite.CheckInProgression = false;
                    prerequisite.HideInUI = false;
                    feature.Components.AddItem(prerequisite);

                    feature.m_Classes = feature.m_Classes.AppendToArray(classWithLevel);
                }
            }
        }
    }
}
