using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System.Linq;

namespace WraithMods.InfoForMe
{
    internal static class Guids
    {
        // BlueprintWeaponEnchantment
        internal static string Furious = "b606a3f5daa76cc40add055613970d2a";

        // BlueprintBuff
        internal static string BloodragerStandartRageBuff = "5eac31e457999334b98f98b60fc73b2f";
    }

    internal static class Patches
    {
        public static void PatchFuriousForBloodrage()
        {
            var furious = ResourcesLibrary.TryGetBlueprint<BlueprintWeaponEnchantment>(Guids.Furious);

            var bloodrageBuff = ResourcesLibrary.TryGetBlueprint<BlueprintBuff>(Guids.BloodragerStandartRageBuff);

            var conditionsArray = furious.GetComponent<WeaponConditionalEnhancementBonus>().Conditions.Conditions;

            furious.GetComponent<WeaponConditionalEnhancementBonus>().Conditions.Conditions = conditionsArray.AddItem(new ContextConditionHasBuff() { m_Buff = bloodrageBuff.ToReference<BlueprintBuffReference>() }).ToArray();
        }

    }
}

//credit to Microsoftenator from Wrath Discord