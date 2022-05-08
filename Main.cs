﻿using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;

namespace WraithMods
{
    static class Main
    {
        public static Settings Settings;
        public static bool Enabled;
        internal static UnityModManager.ModEntry.ModLogger logger;

        //public static ModContextTTTCore TTTContext;
        //static bool Load(UnityModManager.ModEntry modEntry)
        //{
        //    var harmony = new Harmony(modEntry.Info.Id);
        //    TTTContext = new ModContextTTTCore(modEntry);
        //    TTTContext.LoadAllSettings();
        //    TTTContext.ModEntry.OnSaveGUI = OnSaveGUI;
        //    harmony.PatchAll();
        //    PostPatchInitializer.Initialize(TTTContext);
        //    return true;
        //}
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = new Harmony(modEntry.Info.Id);
            logger = modEntry.Logger;
            Settings = Settings.Load<Settings>(modEntry);
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            modEntry.OnSaveGUI = OnSaveGUI;
            harmony.PatchAll();
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }

        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("GAME MUST BE RESTARTED FOR CHANGES TO TAKE EFFECT");
            GUILayout.EndHorizontal();

            AddGUIOption("Truly Everlasting Judgment",
                "Modifies Judgments to be usable out of combat",
                ref Settings.useTrulyEverlastingJudgment);

            AddGUIOption("Incense Fog Changes",
                "Modifies default Incense Fog range to 30 feet and adds effects",
                ref Settings.useIncenseRangeFix);

            AddGUIOption("Mastodon Slam Fix",
                "Readds the Mastodon's secondary Slam attack",
                ref Settings.useMastodonFix);

            AddGUIOption("Hidden Feature Enabler",
                "Enables some default features hidden or disabled in the blueprints " +
                "(Currenly only enables Rekarth's background and the Green Faith deity for player selection)",
                ref Settings.useEnableHiddenFeatures);

            AddGUIOption("Nature's Agony Fix",
                "Fixes Nature's Agony to give +2 DC to sonic spells instead of +1",
                ref Settings.useNaturesAgonyFix);

            AddGUIOption("Wildland Shaman Second Spirit",
                "Allows Wildland Shaman to select Second Spirit Mythic Ability",
                ref Settings.useWildlandSecondSpirit);

            AddGUIOption("Demon Hunter Feat",
                "Adds the Demon Hunter feat",
                ref Settings.useDemonHunter);

            //AddGUIOption("Ravener Hunter Archetype",
            //    "Adds the Ravener Hunter Inquisitor Archetype",
            //    ref Settings.useRavenerHunter);

            AddGUIOption("Bladed Brush Feat",
                "Adds the Bladed Brush feat, and the accompanying Slashing Grace feat",
                ref Settings.useBladedBrush);

            AddGUIOption("Guided Hand Feat",
                "Adds the Guided Hand feat",
                ref Settings.useGuidedHand);

            AddGUIOption("Dual Path Mythic Ability",
                "Adds the Dual Path mythic ability, allowing the main character to select an additional First Ascension ability",
                ref Settings.useDualPath);

            AddGUIOption("Way of the Shooting Star Feat",
                "Adds the Way of the Shooting Star feat",
                ref Settings.useDualPath);

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("Use Everlasting Judgment (modifies Judgments to be usable out of combat)", GUILayout.ExpandWidth(false));
            //GUILayout.Space(100);
            //Settings.useTrulyEverlastingJudgment = GUILayout.Toggle(Settings.useTrulyEverlastingJudgment, $" {Settings.useTrulyEverlastingJudgment}", GUILayout.ExpandWidth(false));
            //GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("Use Incense Range Fix (modifies default Incense Fog range to 30 feet and adds effects)", GUILayout.ExpandWidth(false));
            //GUILayout.Space(100);
            //Settings.useIncenseRangeFix = GUILayout.Toggle(Settings.useIncenseRangeFix, $" {Settings.useIncenseRangeFix}", GUILayout.ExpandWidth(false));
            //GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("Use Mastodon Fix (readds the Mastodon's secondary Slam attack)", GUILayout.ExpandWidth(false));
            //GUILayout.Space(100);
            //Settings.useMastodonFix = GUILayout.Toggle(Settings.useMastodonFix, $" {Settings.useMastodonFix}", GUILayout.ExpandWidth(false));
            //GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("Use Nature's Agony Fix (adjusts Nature's Agony to give +2 DC to sonic spells instead of +1)", GUILayout.ExpandWidth(false));
            //GUILayout.Space(100);
            //Settings.useNaturesAgonyFix = GUILayout.Toggle(Settings.useNaturesAgonyFix, $" {Settings.useNaturesAgonyFix}", GUILayout.ExpandWidth(false));
            //GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("Enable Hidden Features (enables some default features hidden or disabled in the blueprints; currenly only enables Rekarth's background and the Green Faith deity for player selection)", GUILayout.ExpandWidth(false));
            //GUILayout.Space(100);
            //Settings.useEnableHiddenFeatures = GUILayout.Toggle(Settings.useEnableHiddenFeatures, $" {Settings.useEnableHiddenFeatures}", GUILayout.ExpandWidth(false));
            //GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("Enable Wildland Shaman Second Spirit (Allows Wildland Shaman to select Second Spirit Mythic Ability)", GUILayout.ExpandWidth(false));
            //GUILayout.Space(100);
            //Settings.useWildlandSecondSpirit = GUILayout.Toggle(Settings.useWildlandSecondSpirit, $" {Settings.useWildlandSecondSpirit}", GUILayout.ExpandWidth(false));
            //GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Label("testing some layout configurations", GUILayout.ExpandWidth(true));
            //GUILayout.FlexibleSpace();
            //GUILayout.Label("more layout testing happening here, using ExpandWidth", GUILayout.ExpandWidth(false));
            //GUILayout.FlexibleSpace();
            //GUILayout.Toggle(Settings.useWildlandSecondSpirit, $" {Settings.useWildlandSecondSpirit}", GUILayout.ExpandWidth(false));
            //GUILayout.EndHorizontal();
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }

        static void AddGUIOption(string name, string description, ref bool setting)
        {
            GUILayout.BeginVertical();
            GUILayout.Space(5);
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            name = " " + name;
            int len = name.Length;
            do
            {
                name += "\t";
                if (name.Length >= 50) { break; }
                len += 10;
            } while (len < 49);
            name += description;
            setting = GUILayout.Toggle(setting, name, GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }
    }
}

