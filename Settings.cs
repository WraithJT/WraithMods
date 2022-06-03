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
        public bool useDemonHunter = true;
        public bool useRavenerHunter = true;
        public bool useBlastingBracers = true;
        public bool useBladedBrush = true;
        public bool useGuidedHand = true;
        public bool useDualPath = true;
        public bool useWayOfTheShootingStar = true;
        public bool useExtraMythicFeat = true;
        public bool useCovenantFix = true;
        public bool usePlaguedBearFix = true;

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
