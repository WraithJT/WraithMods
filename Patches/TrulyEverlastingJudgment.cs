using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities;
using System;
using BlueprintCore.Blueprints.Configurators.Buffs;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Utils;
using BlueprintCore.Blueprints.Configurators.Abilities;
using Kingmaker.Localization;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Utility;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;

namespace WraithMods.Patches
{
    class TrulyEverlastingJudgment
    {
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
                    PatchJudgments();
                }
                catch (Exception ex)
                {
                    Main.logger.Log(ex.ToString());
                }
            }

            static void PatchJudgments()
            {
                if (Main.Settings.useTrulyEverlastingJudgment == false)
                {
                    return;
                }

                

                //var everlastingJudgment = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("4a6dc772c9a7fe742a65820007107f03");

                //if (!unit.Descriptor.HasFact(everlastingJudgment))
                //{
                //    return;
                //}

                var justiceJudgmentAbility = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("ddffa896d4605a44f95baa6d0d350828");
                justiceJudgmentAbility.OnlyInCombat = false;
                justiceJudgmentAbility.DeactivateIfCombatEnded = false;
                justiceJudgmentAbility.DeactivateImmediately = true;
                justiceJudgmentAbility.ActivationType = AbilityActivationType.Immediately;

                var purityJudgmentAbility = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("15d3b21354c105b498a0b1b293280ded"); ;
                purityJudgmentAbility.OnlyInCombat = false;
                purityJudgmentAbility.DeactivateIfCombatEnded = false;
                purityJudgmentAbility.DeactivateImmediately = true;
                purityJudgmentAbility.ActivationType = AbilityActivationType.Immediately;

                var healingJudgmentAbility = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("00b6d36e31548dc4ab0ac9d15e64a980"); ;
                healingJudgmentAbility.OnlyInCombat = false;
                healingJudgmentAbility.DeactivateIfCombatEnded = false;
                healingJudgmentAbility.DeactivateImmediately = true;
                healingJudgmentAbility.ActivationType = AbilityActivationType.Immediately;

                var piercingJudgmentAbility = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("487af80cdfbaad74b8c2fd644c538233"); ;
                piercingJudgmentAbility.OnlyInCombat = false;
                piercingJudgmentAbility.DeactivateIfCombatEnded = false;
                piercingJudgmentAbility.DeactivateImmediately = true;
                piercingJudgmentAbility.ActivationType = AbilityActivationType.Immediately;

                var protectionJudgmentAbility = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("1bb08308c9f6a5e4697887cd438b7221"); ;
                protectionJudgmentAbility.OnlyInCombat = false;
                protectionJudgmentAbility.DeactivateIfCombatEnded = false;
                protectionJudgmentAbility.DeactivateImmediately = true;
                protectionJudgmentAbility.ActivationType = AbilityActivationType.Immediately;

                var resistanceJudgmentAbility = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("ad61fc9dec7822b45891c78c9da5d5da"); ;
                resistanceJudgmentAbility.OnlyInCombat = false;
                resistanceJudgmentAbility.DeactivateIfCombatEnded = false;
                resistanceJudgmentAbility.DeactivateImmediately = true;
                resistanceJudgmentAbility.ActivationType = AbilityActivationType.Immediately;

                var destructionJudgmentAbility = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("4ebaf39efb8ffb64baf92784808dc49c"); ;
                destructionJudgmentAbility.OnlyInCombat = false;
                destructionJudgmentAbility.DeactivateIfCombatEnded = false;
                destructionJudgmentAbility.DeactivateImmediately = true;
                destructionJudgmentAbility.ActivationType = AbilityActivationType.Immediately;

                var smitingJudgmentAbilityMagic = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("d7e61eb9f0cec5e49bd1b0c428fa98e4"); ;
                smitingJudgmentAbilityMagic.OnlyInCombat = false;
                smitingJudgmentAbilityMagic.DeactivateIfCombatEnded = false;
                smitingJudgmentAbilityMagic.DeactivateImmediately = true;
                smitingJudgmentAbilityMagic.ActivationType = AbilityActivationType.Immediately;

                var smitingJudgmentAbilityAlignment = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("72fe16312b4479145afc6cc6c87cd08f"); ;
                smitingJudgmentAbilityAlignment.OnlyInCombat = false;
                smitingJudgmentAbilityAlignment.DeactivateIfCombatEnded = false;
                smitingJudgmentAbilityAlignment.DeactivateImmediately = true;
                smitingJudgmentAbilityAlignment.ActivationType = AbilityActivationType.Immediately;

                var smitingJudgmentAbilityAdamantite = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("2c448ab4135c7c741b6f0f223901f9fa"); ;
                smitingJudgmentAbilityAdamantite.OnlyInCombat = false;
                smitingJudgmentAbilityAdamantite.DeactivateIfCombatEnded = false;
                smitingJudgmentAbilityAdamantite.DeactivateImmediately = true;
                smitingJudgmentAbilityAdamantite.ActivationType = AbilityActivationType.Immediately;

                var resiliencyJudgmentAbilityLaw = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("3bb25de520e78744ea3ac406b918ab3e"); ;
                resiliencyJudgmentAbilityLaw.OnlyInCombat = false;
                resiliencyJudgmentAbilityLaw.DeactivateIfCombatEnded = false;
                resiliencyJudgmentAbilityLaw.DeactivateImmediately = true;
                resiliencyJudgmentAbilityLaw.ActivationType = AbilityActivationType.Immediately;

                var resiliencyJudgmentAbilityEvil = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("1f7e326d3a88fd84985a60e416388c27"); ;
                resiliencyJudgmentAbilityEvil.OnlyInCombat = false;
                resiliencyJudgmentAbilityEvil.DeactivateIfCombatEnded = false;
                resiliencyJudgmentAbilityEvil.DeactivateImmediately = true;
                resiliencyJudgmentAbilityEvil.ActivationType = AbilityActivationType.Immediately;

                var resiliencyJudgmentAbilityGood = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("d47ecfecae485ac41b54fe4e8027797c"); ;
                resiliencyJudgmentAbilityGood.OnlyInCombat = false;
                resiliencyJudgmentAbilityGood.DeactivateIfCombatEnded = false;
                resiliencyJudgmentAbilityGood.DeactivateImmediately = true;
                resiliencyJudgmentAbilityGood.ActivationType = AbilityActivationType.Immediately;

                var resiliencyJudgmentAbilityChaos = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("1452fb3e0e3e2f6488bee09050097b6f"); ;
                resiliencyJudgmentAbilityChaos.OnlyInCombat = false;
                resiliencyJudgmentAbilityChaos.DeactivateIfCombatEnded = false;
                resiliencyJudgmentAbilityChaos.DeactivateImmediately = true;
                resiliencyJudgmentAbilityChaos.ActivationType = AbilityActivationType.Immediately;

                var resiliencyJudgmentAbilityMagic = ResourcesLibrary.TryGetBlueprint<BlueprintActivatableAbility>("bfec9cee57e7a7d47a8641c5c9d43c41"); ;
                resiliencyJudgmentAbilityMagic.OnlyInCombat = false;
                resiliencyJudgmentAbilityMagic.DeactivateIfCombatEnded = false;
                resiliencyJudgmentAbilityMagic.DeactivateImmediately = true;
                resiliencyJudgmentAbilityMagic.ActivationType = AbilityActivationType.Immediately;
            }
        }
    }
}
