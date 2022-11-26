using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;
using System.Collections;
using System.Collections.Generic;


namespace AccessoriesPlus
{
    public class APWingStatsGI : GlobalItem
    {
        // Items this globalitem modifies
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return AccessoriesPlus.WingStats.ContainsKey(entity.type);
        }

        // Stopping the game crashing for some reasone
        public override bool InstancePerEntity => true;


        // Modify tooltips here
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (AccessoriesPlus.Config.wingStats)
            {
                var stats = AccessoriesPlus.WingStats[item.type];
                var newStats = new List<TooltipLine>()
                {
                    new TooltipLine(Mod, "NegateFallDmg", "Negates fall damage"),
                    new TooltipLine(Mod, "FlightTime", stats[0] + " seconds flight time"),
                    new TooltipLine(Mod, "FlightHeight", stats[1] + " tiles flight height"),
                    new TooltipLine(Mod, "MaxHorizontalSpeed", stats[2] + " mph maximum horizontal speed")
            };

                if (!AccessoriesPlus.InsertTooltips(newStats, "Equipable", tooltips))
                {
                    Mod.Logger.Error($"Item ID {item.type} could not have tooltip modified to include wing stats");
                }
            }
        }
    }
}