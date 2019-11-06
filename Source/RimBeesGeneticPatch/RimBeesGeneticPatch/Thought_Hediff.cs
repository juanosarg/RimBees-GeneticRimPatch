using Verse;
using System.Collections.Generic;
using System.Linq;
using RimWorld;

namespace RimBeesGeneticPatch
{
    class Thought_Hediff : Thought_Memory
    {

       
        public override float MoodOffset()
        {
                       
            this.pawn.health.AddHediff(this.def.hediff);
            return base.MoodOffset();
        }


    }
}
