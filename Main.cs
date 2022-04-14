using UnityModManagerNet;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace WraithMods
{
    static class Main
    {
        public static Settings Settings;
        public static bool Enabled;

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
        }

        static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }
    }
}

