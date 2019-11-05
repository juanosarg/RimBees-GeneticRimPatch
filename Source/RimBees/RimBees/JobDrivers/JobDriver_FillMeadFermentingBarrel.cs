﻿using RimWorld;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using Verse.AI;

namespace RimBees
{
    public class JobDriver_FillMeadFermentingBarrel : JobDriver
    {
        private const TargetIndex BarrelInd = TargetIndex.A;

        private const TargetIndex WortInd = TargetIndex.B;

        private const int Duration = 200;

        protected Building_MeadFermentingBarrel Barrel
        {
            get
            {
                return (Building_MeadFermentingBarrel)this.job.GetTarget(TargetIndex.A).Thing;
            }
        }

        protected Thing Wort
        {
            get
            {
                return this.job.GetTarget(TargetIndex.B).Thing;
            }
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            Pawn pawn = this.pawn;
            LocalTargetInfo target = this.Barrel;
            Job job = this.job;
            bool arg_58_0;
            if (pawn.Reserve(target, job, 1, -1, null, errorOnFailed))
            {
                pawn = this.pawn;
                target = this.Wort;
                job = this.job;
                arg_58_0 = pawn.Reserve(target, job, 1, -1, null, errorOnFailed);
            }
            else
            {
                arg_58_0 = false;
            }
            return arg_58_0;
        }

        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnBurningImmobile(TargetIndex.A);
            base.AddEndCondition(() => (this.Barrel.SpaceLeftForWort > 0) ? JobCondition.Ongoing : JobCondition.Succeeded);
            yield return Toils_General.DoAtomic(delegate
            {
                this.job.count = this.Barrel.SpaceLeftForWort;
            });
            Toil reserveWort = Toils_Reserve.Reserve(TargetIndex.B, 1, -1, null);
            yield return reserveWort;
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.ClosestTouch).FailOnDespawnedNullOrForbidden(TargetIndex.B).FailOnSomeonePhysicallyInteracting(TargetIndex.B);
            yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, true, false).FailOnDestroyedNullOrForbidden(TargetIndex.B);
            yield return Toils_Haul.CheckForGetOpportunityDuplicate(reserveWort, TargetIndex.B, TargetIndex.None, true, null);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_General.Wait(200, TargetIndex.None).FailOnDestroyedNullOrForbidden(TargetIndex.B).FailOnDestroyedNullOrForbidden(TargetIndex.A).FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch).WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            yield return new Toil
            {
                initAction = delegate
                {
                    this.Barrel.AddWort(this.Wort);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
        }
    }
}
