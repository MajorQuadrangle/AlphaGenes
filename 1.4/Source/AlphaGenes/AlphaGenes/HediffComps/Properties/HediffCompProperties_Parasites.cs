﻿using System;
using Verse;
using System.Collections.Generic;
using AnimalBehaviours;

namespace AlphaGenes
{
    public class HediffCompProperties_Parasites : HediffCompProperties
    {
        
        public float severityToTurn = 0.9f;
       
        public HediffCompProperties_Parasites()
        {
            this.compClass = typeof(HediffComp_Parasites);
        }
    }
}
