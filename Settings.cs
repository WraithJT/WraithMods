using UnityModManagerNet;

namespace WraithMods
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool useTrulyEverlastingJudgment = true;
        public bool useIncenseRangeFix = true;
        public bool useMastodonFix = true;
        public bool useNaturesAgonyFix = true;
        public bool useEnableHiddenFeatures = true;
        public bool useWildlandSecondSpirit = true;

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
