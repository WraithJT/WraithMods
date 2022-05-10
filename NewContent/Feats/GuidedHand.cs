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

namespace WraithMods.NewContent.Feats
{
    class GuidedHand
    {
        private static readonly string guidedHandFeatName = "GuidedHand";
        private static readonly string guidedHandFeatGuid = "DA092DBD-FB10-4670-A0D8-C5DA3FB442FF";
        private static readonly string guidedHandDisplayName = "Guided Hand";
        private static readonly string guidedHandDisplayNameKey = "GuidedHandName";
        private static readonly string guidedHandDescription =
            "With your deity’s favored weapon, you can use your Wisdom modifier instead of " +
            "your Strength or Dexterity modifier on attack rolls.";
        private static readonly string guidedHandDescriptionKey = "GuidedHandDescription";

        private static readonly string ChannelEnergyFeature = "a79013ff4bcd4864cb669622a29ddafb";
        private static readonly string ChannelNegativeFeature = "3adb2c906e031ee41a01bfc1d5fb7eea";
        private static readonly string WeaponFocusFeature = "1e1f627d26ad36f43bbd26cc2bf8ac7e";

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
                    PatchGuidedHand();
                }
                catch (Exception ex)
                {
                    Tools.LogMessage("EXCEPTION: " + ex.ToString());
                }
            }
            public static void PatchGuidedHand()
            {
                string atheismGUID = "92c0d2da0a836ce418a267093c09ca54";
                string[] ghFeats = CreateGHFeats();

                var GuidedHand = FeatureSelectionConfigurator.New(guidedHandFeatName, guidedHandFeatGuid)
                    .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey, guidedHandDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey, guidedHandDescription))
                    .SetFeatureTags(FeatureTag.Attack)
                    .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .PrerequisiteNoFeature(atheismGUID)
                    .PrerequisiteFeaturesFromList(new string[] { ChannelEnergyFeature, ChannelNegativeFeature }, 1)
                    .AddRecommendationHasFeature(WeaponFocusFeature)
                    .AddRecommendationStatMiminum(StatType.Wisdom, 15)
                    .AddToFeatures(ghFeats)
                    .Configure();

                if (Main.Settings.useGuidedHand == false) { return; }
                Tools.AddAsFeat(ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(guidedHandFeatGuid));
            }
        }

        static string[] CreateGHFeats()
        {
            #region deity GUIDs
            string abadarGUID = "6122dacf418611540a3c91e67197ee4e";
            string asmodeusGUID = "a3a5ccc9c670e6f4ca4a686d23b89900";
            string calistriaGUID = "c7531715a3f046d4da129619be63f44c";
            string caydenGUID = "300e212868bca984687c92bcb66d381b";
            string desnaGUID = "2c0a3b9971327ba4d9d85354d16998c1";
            string erastilGUID = "afc775188deb7a44aa4cbde03512c671";
            string godclawGUID = "583a26e88031d0a4a94c8180105692a5";
            string gorumGUID = "8f49a5d8528a82c44b8c117a89f6b68c";
            string gozrehGUID = "4af983eec2d821b40a3065eb5e8c3a72";
            string gyronnaGUID = "8b535b6842e063d48a571a042c3c6e8f";
            string iomedaeGUID = "88d5da04361b16746bf5b65795e0c38c";
            string iroriGUID = "23a77a5985de08349820429ce1b5a234";
            string lamashtuGUID = "f86bc8fbf13221f4f9041608a1fb8585";
            string nethysGUID = "6262cfce7c31626458325ca0909de997";
            string norgorberGUID = "805b6bdc8c96f4749afc687a003f9628";
            string pharasmaGUID = "458750bc214ab2e44abdeae404ab22e9";
            string rovagugGUID = "04bc2b62273ab744092d992ed72bff41";
            string sarenraeGUID = "c1c4f7f64842e7e48849e5e67be11a1b";
            string shelynGUID = "b382afa31e4287644b77a8b30ed4aa0b";
            string toragGUID = "d2d5c5a58885a6b489727467e13c3337";
            string urgathoaGUID = "812f6c07148088e41a9ac94b56ac2fc8";
            string zonkuthonGUID = "f7eed400baa66a744ad361d4df0e6f1b";
            #endregion

            #region weapon GUIDs
            string lightcrossbowGUID = "d525e7a6d8d5aa648a976ac41194b8d0";
            string lightmaceGUID = "cf3e703db4b81904e982a66d7b8474ea";
            string heavymaceGUID = "d5a167f0f0208dd439ec7481e8989e21";
            string rapierGUID = "2ece38f30500f454b8569136221e55b0";
            string starknifeGUID = "5a939137fc039084580725b2b0845c3f";
            string longbowGUID = "7a1211c05ec2c46428f41e3c0db9423f";
            string flailGUID = "bf1e53f7442ed0c43bf52d3abe55e16a";
            string heavyflailGUID = "8fefb7e0da38b06408f185e29372c703";
            string greatswordGUID = "5f824fbb0766a3543bbd6ae50248688f";
            string tridentGUID = "6ff66364e0a2c89469c2e52ebb46365e";
            string daggerGUID = "07cc1a7fceaee5b42b3e43da960fe76d";
            string longswordGUID = "d56c44bc9eb10204c8b386a02c7eed21";
            string unarmedGUID = "fcca8e6b85d19b14786ba1ab553e23ad";
            string falchionGUID = "6ddc9acbbb6e40746a6a1671df1f7b47";
            string kukriGUID = "006ecd4715809b343b7001e859e3ddb2";
            string quarterstaffGUID = "629736dabac7f9f4a819dc854eaed2d6";
            string shortswordGUID = "a7da36e0e7bb60e42b9f23462ce2f4fc";
            string greataxeGUID = "e8059a8eac62cd74f9171d748a5ae428";
            string scimitarGUID = "d9fbec4637d71bd4ebc977628de3daf3";
            string glaiveGUID = "7a14a1b224cd173449cb7ffc77d5f65c";
            string warhammerGUID = "fac41e149f49cba4a8e06ce39b41a6fa";
            string scytheGUID = "4eacfc7e152930a45a1a16217c35011c";
            #endregion

            #region Deities
            Deity Abadar = new()
            {
                DeityName = "Abadar",
                DeityGUID = abadarGUID,
                FeatName = "GuidedHandAbadar",
                FeatGUID = "1B319167-C481-4DB4-A2C5-21B51E9E9C97",
                WeaponCategory = WeaponSubCategory.Ranged,
                WeaponTypes = new string[] { lightcrossbowGUID }
            };

            Deity Asmodeus = new()
            {
                DeityName = "Asmodeus",
                DeityGUID = asmodeusGUID,
                FeatName = "GuidedHandAsmodeus",
                FeatGUID = "8D46FF0D-36C7-4A45-B3D3-BBABF63090BE",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { lightmaceGUID, heavymaceGUID }
            };

            Deity Calistria = new()
            {
                DeityName = "Calistria",
                DeityGUID = calistriaGUID,
                FeatName = "GuidedHandCalistria",
                FeatGUID = "BEA15A47-96F6-40A6-9CBB-EC5A7726C635",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { rapierGUID }
            };

            Deity CaydenCailean = new()
            {
                DeityName = "Cayden Cailean",
                DeityGUID = caydenGUID,
                FeatName = "GuidedHandCaydenCailean",
                FeatGUID = "D92C55AB-DFBE-46B7-A57C-F6ACC6012796",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { rapierGUID }
            };

            Deity Desna = new()
            {
                DeityName = "Desna",
                DeityGUID = desnaGUID,
                FeatName = "GuidedHandDesna",
                FeatGUID = "4B306421-E777-4FFB-A802-6D74C630401B",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { starknifeGUID }
            };

            Deity Erastil = new()
            {
                DeityName = "Erastil",
                DeityGUID = erastilGUID,
                FeatName = "GuidedHandErastil",
                FeatGUID = "6BF8A9B8-073F-4E43-B8F9-5B95F8C568E3",
                WeaponCategory = WeaponSubCategory.Ranged,
                WeaponTypes = new string[] { longbowGUID }
            };

            Deity Godclaw = new()
            {
                DeityName = "Godclaw",
                DeityGUID = godclawGUID,
                FeatName = "GuidedHandGodclaw",
                FeatGUID = "B39E6150-A432-4D8D-BC0C-2EDCE472E1E0",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { flailGUID, heavyflailGUID }
            };

            Deity Gorum = new()
            {
                DeityName = "Gorum",
                DeityGUID = gorumGUID,
                FeatName = "GuidedHandGorum",
                FeatGUID = "9A7923E4-0D39-4496-9727-94DD37B2D5A9",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { greatswordGUID }
            };

            Deity Gozreh = new()
            {
                DeityName = "Gozreh",
                DeityGUID = gozrehGUID,
                FeatName = "GuidedHandGozreh",
                FeatGUID = "739C0256-7077-4124-9E12-80F36F1F7D5A",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { tridentGUID }
            };

            Deity Gyronna = new()
            {
                DeityName = "Gyronna",
                DeityGUID = gyronnaGUID,
                FeatName = "GuidedHandGyronna",
                FeatGUID = "2AFFA683-D995-4723-8DCE-A95B96DA08EE",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { daggerGUID }
            };

            Deity Iomedae = new()
            {
                DeityName = "Iomedae",
                DeityGUID = iomedaeGUID,
                FeatName = "GuidedHandIomedae",
                FeatGUID = "7A825ADE-2846-4369-B84E-6D2AECA88A52",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { longswordGUID }
            };

            Deity Irori = new()
            {
                DeityName = "Irori",
                DeityGUID = iroriGUID,
                FeatName = "GuidedHandIrori",
                FeatGUID = "D9406D27-6ACD-403B-A612-78F16E646FA7",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { unarmedGUID }
            };

            Deity Lamashtu = new()
            {
                DeityName = "Lamashtu",
                DeityGUID = lamashtuGUID,
                FeatName = "GuidedHandLamashtu",
                FeatGUID = "C34D5DED-7108-447E-8CE0-8D77FD536EF9",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { falchionGUID, kukriGUID }
            };

            Deity Nethys = new()
            {
                DeityName = "Nethys",
                DeityGUID = nethysGUID,
                FeatName = "GuidedHandNethys",
                FeatGUID = "3891DCCA-5A35-4327-A75E-EC8F53DC314D",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { quarterstaffGUID }
            };

            Deity Norgorber = new()
            {
                DeityName = "Norgorber",
                DeityGUID = norgorberGUID,
                FeatName = "GuidedHandNorgorber",
                FeatGUID = "BC31AD4C-873D-4A1B-89CD-8218BE678F9B",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { shortswordGUID }
            };

            Deity Pharasma = new()
            {
                DeityName = "Pharasma",
                DeityGUID = pharasmaGUID,
                FeatName = "GuidedHandPharasma",
                FeatGUID = "72378F40-350C-442B-9BDA-F37FA938B825",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { daggerGUID }
            };

            Deity Rovagug = new()
            {
                DeityName = "Rovagug",
                DeityGUID = rovagugGUID,
                FeatName = "GuidedHandRovagug",
                FeatGUID = "A571C7B2-1B5F-4A00-B52A-E3F3A9380859",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { greataxeGUID }
            };

            Deity Sarenrae = new()
            {
                DeityName = "Sarenrae",
                DeityGUID = sarenraeGUID,
                FeatName = "GuidedHandSarenrae",
                FeatGUID = "870580C6-0592-4370-90FD-265B068465BC",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { scimitarGUID }
            };

            Deity Shelyn = new()
            {
                DeityName = "Shelyn",
                DeityGUID = shelynGUID,
                FeatName = "GuidedHandShelyn",
                FeatGUID = "9C98EEF9-37A4-4FB0-AA10-F99B7D38E9F5",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { glaiveGUID }
            };

            Deity Torag = new()
            {
                DeityName = "Torag",
                DeityGUID = toragGUID,
                FeatName = "GuidedHandTorag",
                FeatGUID = "8A1D98F9-E027-423A-82F2-EE9527F1B1B5",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { warhammerGUID }
            };

            Deity Urgathoa = new()
            {
                DeityName = "Urgathoa",
                DeityGUID = urgathoaGUID,
                FeatName = "GuidedHandUrgathoa",
                FeatGUID = "7914A9D2-7628-430D-9966-849C9F072E4A",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { scytheGUID }
            };

            Deity ZonKuthon = new()
            {
                DeityName = "Zon-Kuthon",
                DeityGUID = zonkuthonGUID,
                FeatName = "GuidedHandZonKuthon",
                FeatGUID = "144B394B-17AD-487A-B493-C212E332842A",
                WeaponCategory = WeaponSubCategory.Melee,
                WeaponTypes = new string[] { flailGUID, heavyflailGUID }
            };
            #endregion

            Deity[] deities = new Deity[] {
                Abadar,
                Asmodeus,
                Calistria,
                CaydenCailean,
                Desna,
                Erastil,
                Godclaw,
                Gorum,
                Gozreh,
                Gyronna,
                Iomedae,
                Irori,
                Lamashtu,
                Nethys,
                Norgorber,
                Pharasma,
                Rovagug,
                Sarenrae,
                Shelyn,
                Torag,
                Urgathoa,
                ZonKuthon};

            foreach (Deity d in deities) { ConfigureGHFeat(d); }

            string[] features = new string[] {
                Abadar.FeatGUID,
                Asmodeus.FeatGUID,
                Calistria.FeatGUID,
                CaydenCailean.FeatGUID,
                Desna.FeatGUID,
                Erastil.FeatGUID,
                Godclaw.FeatGUID,
                Gorum.FeatGUID,
                Gozreh.FeatGUID,
                Gyronna.FeatGUID,
                Iomedae.FeatGUID,
                Irori.FeatGUID,
                Lamashtu.FeatGUID,
                Nethys.FeatGUID,
                Norgorber.FeatGUID,
                Pharasma.FeatGUID,
                Rovagug.FeatGUID,
                Sarenrae.FeatGUID,
                Shelyn.FeatGUID,
                Torag.FeatGUID,
                Urgathoa.FeatGUID,
                ZonKuthon.FeatGUID};

            return features;

            #region old way
            //        FeatureConfigurator.New(guidedHandFeatName_Abadar, guidedHandFeatGuid_Abadar)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Abadar", guidedHandDisplayName + " (Abadar)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Abadar", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Ranged,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { lightcrossbowGUID })
            //            .PrerequisiteFeature(abadarGUID)
            //            .Configure();

            //        FeatureConfigurator.New(guidedHandFeatName_Asmodeus, guidedHandFeatGuid_Asmodeus)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Asmodeus", guidedHandDisplayName + " (Asmodeus)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Asmodeus", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Melee,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { lightmaceGUID, heavymaceGUID })
            //            .PrerequisiteFeature(asmodeusGUID)
            //            .Configure();

            //        FeatureConfigurator.New(guidedHandFeatName_Calistria, guidedHandFeatGuid_Calistria)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Calistria", guidedHandDisplayName + " (Calistria)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Calistria", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Melee,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { rapierGUID })
            //            .PrerequisiteFeature(calistriaGUID)
            //            .Configure();


            //        FeatureConfigurator.New(guidedHandFeatName_Cayden, guidedHandFeatGuid_Cayden)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Cayden", guidedHandDisplayName + " (Cayden Cailean)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Cayden", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Melee,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { rapierGUID })
            //            .PrerequisiteFeature(caydenGUID)
            //            .Configure();

            //        FeatureConfigurator.New(guidedHandFeatName_Desna, guidedHandFeatGuid_Desna)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Desna", guidedHandDisplayName + " (Desna)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Desna", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Melee,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { starknifeGUID })
            //            .PrerequisiteFeature(desnaGUID)
            //            .Configure();


            //        FeatureConfigurator.New(guidedHandFeatName_Erastil, guidedHandFeatGuid_Erastil)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Erastil", guidedHandDisplayName + " (Erastil)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Erastil", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Ranged,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { longbowGUID })
            //            .PrerequisiteFeature(erastilGUID)
            //            .Configure();
            //        #endregion

            //        ConfigureGHFeat(
            //            "Godclaw",
            //            guidedHandFeatName_Godclaw,
            //            guidedHandFeatGuid_Godclaw,
            //            WeaponSubCategory.Melee,
            //            new string[] { flailGUID, heavyflailGUID },
            //            godclawGUID);

            //        FeatureConfigurator.New(guidedHandFeatName_Godclaw, guidedHandFeatGuid_Godclaw)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Godclaw", guidedHandDisplayName + " (Godclaw)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Godclaw", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Melee,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { flailGUID, heavyflailGUID })
            //            .PrerequisiteFeature(godclawGUID)
            //            .Configure();

            //        FeatureConfigurator.New(guidedHandFeatName_Gorum, guidedHandFeatGuid_Gorum)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Gorum", guidedHandDisplayName + " (Gorum)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Gorum", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Melee,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { greatswordGUID })
            //            .PrerequisiteFeature(gorumGUID)
            //            .Configure();

            //        FeatureConfigurator.New(guidedHandFeatName_Gozreh, guidedHandFeatGuid_Gozreh)
            //            .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_Gozreh", guidedHandDisplayName + " (Gozreh)", false))
            //            .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_Gozreh", guidedHandDescription))
            //            .SetFeatureTags(FeatureTag.Attack)
            //            .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            //            .AddAttackStatReplacement(
            //                replacementStat: StatType.Wisdom,
            //                subCategory: WeaponSubCategory.Melee,
            //                checkWeaponTypes: true,
            //                weaponTypes: new string[] { tridentGUID })
            //            .PrerequisiteFeature(gozrehGUID)
            //            .Configure();
            #endregion
        }

        static void ConfigureGHFeat(Deity deity)
        {
            FeatureConfigurator.New(deity.FeatName, deity.FeatGUID)
                .SetDisplayName(LocalizationTool.CreateString(guidedHandDisplayNameKey + "_" + deity.DeityName.Trim(), guidedHandDisplayName + " (" + deity.DeityName + ")", false))
                .SetDescription(LocalizationTool.CreateString(guidedHandDescriptionKey + "_" + deity.DeityName.Trim(), guidedHandDescription))
                .SetFeatureTags(FeatureTag.Attack)
                .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .AddAttackStatReplacement(
                    replacementStat: StatType.Wisdom,
                    subCategory: deity.WeaponCategory,
                    checkWeaponTypes: true,
                    weaponTypes: deity.WeaponTypes)
                    .PrerequisiteFeature(deity.DeityGUID)
                .Configure();
        }

        public class Deity
        {
            public string DeityName;
            public string DeityGUID;
            public string FeatName;
            public string FeatGUID;
            public WeaponSubCategory WeaponCategory;
            public string[] WeaponTypes;
        }
    }
}