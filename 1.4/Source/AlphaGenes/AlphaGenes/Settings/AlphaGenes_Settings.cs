﻿using RimWorld;
using UnityEngine;
using Verse;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AlphaGenes
{
    public class AlphaGenes_Settings : ModSettings

    {

        public const float AG_QuestRateBase = 1;
        public float AG_QuestRate = AG_QuestRateBase;
        public bool AG_DisableQuests = false;
        public bool AG_DisableMutationsMessage = false;
        public bool AG_GeneRemovalComa = true;


        private static Vector2 scrollPosition = Vector2.zero;

        public override void ExposeData()
        {
            base.ExposeData();
  
            Scribe_Values.Look(ref AG_QuestRate, "AG_QuestRate", AG_QuestRateBase);
            Scribe_Values.Look(ref AG_DisableQuests, "AG_DisableQuests", false);
            Scribe_Values.Look(ref AG_DisableMutationsMessage, "AG_DisableMutationsMessage", false);
            Scribe_Values.Look(ref AG_GeneRemovalComa, "AG_GeneRemovalComa", true);


        }
        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            var scrollContainer = inRect.ContractedBy(10);
            scrollContainer.height -= listingStandard.CurHeight;
            scrollContainer.y += listingStandard.CurHeight;
            Widgets.DrawBoxSolid(scrollContainer, Color.grey);
            var innerContainer = scrollContainer.ContractedBy(1);
            Widgets.DrawBoxSolid(innerContainer, new ColorInt(42, 43, 44).ToColor);
            var frameRect = innerContainer.ContractedBy(5);
            frameRect.y += 15;
            frameRect.height -= 15;
            var contentRect = frameRect;
            contentRect.x = 0;
            contentRect.y = 0;
            contentRect.width -= 20;

            contentRect.height = 950f;

            Widgets.BeginScrollView(frameRect, ref scrollPosition, contentRect, true);
            listingStandard.Begin(contentRect.AtZero());

            var QuestRateLabel = listingStandard.LabelPlusButton("AG_QuestRate".Translate() + ": " + AG_QuestRate, "AG_QuestRateTooltip".Translate());
            AG_QuestRate = (float)Math.Round(listingStandard.Slider(AG_QuestRate, 0.1f, 5f), 1);
            if (listingStandard.Settings_Button("AG_Reset".Translate(), new Rect(0f, QuestRateLabel.position.y + 35, 180f, 29f)))
            {
                AG_QuestRate = AG_QuestRateBase;
            }
            listingStandard.CheckboxLabeled("AG_DisableQuests".Translate(), ref AG_DisableQuests, "AG_DisableQuests_Description".Translate());
            listingStandard.CheckboxLabeled("AG_DisableMutationsMessage".Translate(), ref AG_DisableMutationsMessage, "AG_DisableMutationsMessage_Description".Translate());
            listingStandard.CheckboxLabeled("AG_GeneRemovalComa".Translate(), ref AG_GeneRemovalComa, "AG_GeneRemovalComa_Description".Translate());

            listingStandard.End();
            Widgets.EndScrollView();

            base.Write();

        }

    }


}
