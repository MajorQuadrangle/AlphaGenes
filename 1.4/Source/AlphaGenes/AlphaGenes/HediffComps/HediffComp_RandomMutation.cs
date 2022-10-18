﻿
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;


namespace AlphaGenes
{
	public class HediffComp_RandomMutation : HediffComp
	{
		private HediffCompProperties_RandomMutation Props => (HediffCompProperties_RandomMutation)props;

		public List<GeneDef> genes = new List<GeneDef>();

		public bool Active = false;

        public override void CompExposeData()
        {
            base.CompExposeData();
			Scribe_Collections.Look(ref this.genes, nameof(this.genes));
			Scribe_Values.Look(ref this.Active, nameof(this.Active));

		}

		public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (!Active && this.parent.pawn.Map!=null) {
				Active = true;
				genes.Clear();
				List<string> geneNamesToDisplay = new List<string>();
				for (int i = 0; i < Props.numberOfGenes; i++)
				{
					GeneDef gene = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.exclusionTags?.Contains("AG_OnlyOnCharacterCreation") == false).RandomElement();
					genes.Add(gene);
					geneNamesToDisplay.Add(gene.LabelCap);
					this.parent.pawn.genes?.AddGene(gene, true);

				}
                Messages.Message(    "AG_RandomGenesGained".Translate(parent.pawn.LabelShortCap, geneNamesToDisplay.ToCommaList()), this.parent.pawn, MessageTypeDefOf.PositiveEvent, true);

			}

            if (Props.recurrent)
            {
                if (this.parent.pawn.IsHashIntervalTick(Props.period)) {

                    if (!genes.NullOrEmpty()) {
						for (int i = 0; i < Props.numberOfGenes; i++)
						{
							if (this.parent.pawn.genes?.GetGene(genes[i]) != null)
							{
								this.parent.pawn.genes?.RemoveGene(this.parent.pawn.genes.GetGene(genes[i]));
							}

						}
						genes.Clear();
					}
					

					Active = false;

				}
            }
			

		}
		

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();

			for (int i = 0; i < Props.numberOfGenes; i++)
			{
				if (this.parent.pawn.genes?.GetGene(genes[i]) != null)
				{
					this.parent.pawn.genes?.RemoveGene(this.parent.pawn.genes.GetGene(genes[i]));
				}

			}
			Active = false;
			genes.Clear();




		}




    }
}