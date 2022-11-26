using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;
using System.Collections;
using System.Collections.Generic;


namespace AccessoriesPlus
{
    public class APHookStatsGI : GlobalItem
    {
        // Items this globalitem modifies
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return AccessoriesPlus.HookStats.ContainsKey(entity.type);
        }

        // Stopping the game crashing for some reasone
        public override bool InstancePerEntity => true;


        // Modify tooltips here
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (AccessoriesPlus.Config.hookStats)
            {
                var stats = AccessoriesPlus.HookStats[item.type];
                var newStats = new List<TooltipLine>()
                {
                    new TooltipLine(Mod, "Reach", stats[0] + " tiles reach"),
                    new TooltipLine(Mod, "Velocity", stats[1] + " velocity"),
                    new TooltipLine(Mod, "NumHooks", stats[2] + " hooks"),
                    new TooltipLine(Mod, "LatchingMode", stats[3] + " latching")
                };

                if (!AccessoriesPlus.InsertTooltips(newStats, "Equipable", tooltips))
                {
                    Mod.Logger.Error($"Item ID {item.type} could not have tooltip modified to include hook stats");
                }
            }
        }
    }
}