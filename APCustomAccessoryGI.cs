using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;
using System.Collections;
using System.Collections.Generic;


namespace AccessoriesPlus
{
    public class APCustomAccessoryGI : GlobalItem
    {
        // Items this globalitem modifies
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return AccessoriesPlus.CustomAccessories.ContainsKey(entity.type);
        }

        // Stopping the game crashing for some reason
        public override bool InstancePerEntity => true;


        // Modify items here
        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);
            AccessoriesPlus.CustomAccessories[item.type].customDefaults(item);
        }


        // Modify items effects on player here
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            AccessoriesPlus.CustomAccessories[item.type].customEffects(item, player, hideVisual);
        }


        // Modify tooltips here
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            AccessoriesPlus.CustomAccessories[item.type].customTooltip(item, tooltips);
        }
    }
}