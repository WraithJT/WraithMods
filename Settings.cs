using UnityModManagerNet;

namespace WraithMods
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool useTrulyEverlastingJudgment = true;
        public bool useIncenseRangeFix = true;

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
