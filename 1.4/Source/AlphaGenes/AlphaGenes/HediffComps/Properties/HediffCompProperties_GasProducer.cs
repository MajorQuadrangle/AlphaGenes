﻿

using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Text;

namespace AlphaGenes
{
    public class HediffCompProperties_GasProducer : HediffCompProperties
    {


        public int amount;
        public int timer;
        public GasType gasType;


        public HediffCompProperties_GasProducer()
        {
            this.compClass = typeof(HediffComp_GasProducer);
        }
    }
}
