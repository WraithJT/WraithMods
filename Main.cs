using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;

namespace WraithMods
{
    static class Main
    {
        public static Settings Settings;
        public static bool Enabled;
        internal static UnityModManager.ModEntry.ModLogger logger;
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = new Harmony(modEntry.Info.Id);
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
            GUILayout.Label("Use Everlasting Judgment (modifies Judgments to be usable out of combat)", GUILayout.ExpandWidth(false));
            GUILayout.Space(100);
            Settings.useTrulyEverlastingJudgment = GUILayout.Toggle(Settings.useTrulyEverlastingJudgment, $" {Settings.useTrulyEverlastingJudgment}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Use Incense Range Fix (modifies default Incense Fog range to 30 feet and adds effects)", GUILayout.ExpandWidth(false));
            GUILayout.Space(100);
            Settings.useIncenseRangeFix = GUILayout.Toggle(Settings.useIncenseRangeFix, $" {Settings.useIncenseRangeFix}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Use Mastodon Fix (readds the Mastodon's secondary Slam attack)", GUILayout.ExpandWidth(false));
            GUILayout.Space(100);
            Settings.useMastodonFix = GUILayout.Toggle(Settings.useMastodonFix, $" {Settings.useMastodonFix}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Use Nature's Agony Fix (adjusts Nature's Agony to give +2 DC to sonic spells instead of +1)", GUILayout.ExpandWidth(false));
            GUILayout.Space(100);
            Settings.useNaturesAgonyFix = GUILayout.Toggle(Settings.useNaturesAgonyFix, $" {Settings.useNaturesAgonyFix}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Enable Hidden Features (enables some default features hidden or disabled in the blueprints; currenly only enables Rekarth's background and the Green Faith deity for player selection)", GUILayout.ExpandWidth(false));
            GUILayout.Space(100);
            Settings.useEnableHiddenFeatures = GUILayout.Toggle(Settings.useEnableHiddenFeatures, $" {Settings.useEnableHiddenFeatures}", GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }
    }
}

