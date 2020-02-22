using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;

namespace RimBeesGeneticPatch
{
    class HediffComp_WhileHavingThoughts : HediffComp
    {
        public bool flagAmIThinking = false;

        public override void CompExposeData()
        {
            Scribe_Values.Look<bool>(ref this.flagAmIThinking, "flagAmIThinking", false, false);
        }

        public HediffCompProperties_WhileHavingThoughts Props
        {
            get
            {
                return (HediffCompProperties_WhileHavingThoughts)this.props;
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            
            foreach (ThoughtDef thoughtDef in this.Props.thoughtDefs)
            {
                flagAmIThinking = false;
                if (this.Pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(thoughtDef) != null)
                {
                    flagAmIThinking = true;
                    break;
                }
            }

            if (!flagAmIThinking)
            {
                this.Pawn.health.RemoveHediff(this.parent);
            }
        }

    }
}
