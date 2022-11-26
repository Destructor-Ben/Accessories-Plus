using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;
using System.Collections;
using System.Collections.Generic;


namespace AccessoriesPlus
{
    public class APCustomAcessoryMS : ModSystem
    {
        // Add recipes here
        public override void AddRecipes()
        {
            foreach (var accessory in AccessoriesPlus.CustomAccessories)
            {
                accessory.Value.customRecipe();
            }
        }


        // Remove recipes and modify recipes here
        public override void PostAddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                if (AccessoriesPlus.CustomAccessories.ContainsKey(recipe.createItem.type) && recipe.Mod != Mod)
                {
                    AccessoriesPlus.CustomAccessories[recipe.createItem.type].customVanillaRecipe(recipe);
                }
            }
        }
    }
}