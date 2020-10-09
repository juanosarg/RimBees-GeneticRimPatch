using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Text;

namespace RimBeesGeneticPatch
{
    class HediffCompProperties_WhileHavingThoughts : HediffCompProperties
    {

        public List<ThoughtDef> thoughtDefs;

        public HediffCompProperties_WhileHavingThoughts()
        {
            this.compClass = typeof(HediffComp_WhileHavingThoughts);
        }
    }
}
