using System;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI.Common;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using UnityEngine;
using UnityEngine.Serialization;

namespace WraithMods.NewContent.Components
{
    [ComponentName("Attack bonus against fact owner")]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowMultipleComponents]
    [TypeId("ed385ee72fa45734fa6314432585d552")]
    public class AttackBonusAgainstDeityWorshipper : UnitFactComponentDelegate<AttackBonusConditional.RuntimeData>, IInitiatorRulebookHandler<RuleAttackRoll>, IRulebookHandler<RuleAttackRoll>, ISubscriber, IInitiatorRulebookSubscriber, IInitiatorRulebookHandler<RuleCalculateAttackBonus>, IRulebookHandler<RuleCalculateAttackBonus>
    {
        public BlueprintUnitFact Deity
        {
            get
            {
                BlueprintUnitFactReference checkedFact = this.m_Deity;
                if (checkedFact == null)
                {
                    return null;
                }
                return checkedFact.Get();
            }
        }

        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
            if ((!this.Not && evt.Weapon != null && evt.Target.Descriptor.HasFact(this.Deity)) || (this.Not && !evt.Target.Descriptor.HasFact(this.Deity)))
            {
                base.Data.AttackBonus = this.AttackBonus * base.Fact.GetRank() + this.Bonus.Calculate(base.Context);
                base.Data.Target = evt.Target;
            }
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
            base.Data.Clear();
        }

        public void OnEventAboutToTrigger(RuleCalculateAttackBonus evt)
        {
            if (base.Data.Target != null && base.Data.Target == evt.Target)
            {
                evt.AddModifier(base.Data.AttackBonus, base.Fact, this.Descriptor);
            }
        }

        public void OnEventDidTrigger(RuleCalculateAttackBonus evt)
        {
        }

        public static BlueprintFeature GetDeity(UnitDescriptor unitDescriptor)
        {
            if (unitDescriptor == null)
            {
                return null;
            }
            foreach (Feature feature in unitDescriptor.Progression.Features.Visible)
            {
                FeatureGroup featureGroup = UIUtilityUnit.GetFeatureGroup(feature.Blueprint);
                if (featureGroup == FeatureGroup.Deities)
                {
                    BlueprintFeatureSelection blueprintFeatureSelection = feature.Blueprint as BlueprintFeatureSelection;
                    if (blueprintFeatureSelection != null)
                    {
                        return unitDescriptor.Progression.GetSelections(blueprintFeatureSelection, 1).FirstOrDefault<BlueprintFeature>();
                    }
                }
            }
            return null;
        }

        public BlueprintUnitFactReference m_Deity;

        public int AttackBonus;

        public ContextValue Bonus;

        public ModifierDescriptor Descriptor;

        public bool Not;
    }
}
