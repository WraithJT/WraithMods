using UnityModManagerNet;
using HarmonyLib;

namespace WraithMods
{
    public class Main
    {
        static class Main
        {
            public static bool Enabled;

            static bool Load(UnityModManager.ModEntry modEntry)
            {
                var harmony = new Harmony(modEntry.Info.Id);
                modEntry.OnToggle = OnToggle;
                harmony.PatchAll();
                return true;
            }

            static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
            {
                Enabled = value;
                return true;

            }
        }
    }
}
