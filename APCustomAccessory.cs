using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;
using System.Collections;
using System.Collections.Generic;


namespace AccessoriesPlus
{
    // Class to store information about a custom accessory
    public class APCustomAccessory
    {
        public Action<Item> customDefaults;
        public Action<Item, Player, bool> customEffects;
        public Action<Item, List<TooltipLine>> customTooltip;
        public Action customRecipe;
        public Action<Recipe> customVanillaRecipe;

        public APCustomAccessory(Action<Item> customDefaults, Action<Item, Player, bool> customEffects, Action<Item, List<TooltipLine>> customTooltip, Action customRecipe, Action<Recipe> customVanillaRecipe)
        {
            this.customDefaults = customDefaults;
            this.customEffects = customEffects;
            this.customTooltip = customTooltip;
            this.customRecipe = customRecipe;
            this.customVanillaRecipe = customVanillaRecipe;
    }
    }
}