﻿
using RimWorld;
using Verse;
namespace AlphaGenes
{
	public class ConditionalStatAffecter_NoSunlight : ConditionalStatAffecter
	{
		public override string Label => "AG_StatsReport_NoSunlight".Translate();

		public override bool Applies(StatRequest req)
		{
			if (!ModsConfig.BiotechActive)
			{
				return false;
			}
			if (req.HasThing && req.Thing.Spawned)
			{
				return !req.Thing.Position.InSunlight(req.Thing.Map);
			}
			return false;
		}
	}
}
