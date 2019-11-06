using Verse;
using System.Collections.Generic;
using System.Linq;
using RimWorld;

namespace RimBeesGeneticPatch
{
    class Thought_Hediff : Thought_Memory
    {

        private bool addHediffOnce = true;
        private System.Random rand = new System.Random();
        public int phase = 1;


       

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref this.addHediffOnce, "addHediffOnce", true, false);
           
        }



        public override void Init()
        {

            base.Init();

            if (addHediffOnce)
            {
                Log.Message(this.def.hediff.ToString());
                Log.Message(this.pawn.ToString());
                Log.Message(this.pawn.health.ToString());


                this.pawn.health.AddHediff(this.def.hediff);
               

                //Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDef.Named(Props.hediffname), false);
                //hediff.Severity = Props.hediffseverity;
                addHediffOnce = false;
            }
        }


    }
}
