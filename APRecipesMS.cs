using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;
using System.Collections;
using System.Collections.Generic;


namespace AccessoriesPlus
{
    public class APRecipesMS : ModSystem
    {
        // Recipe groups
        public override void AddRecipeGroups()
        {
            // Obsidian skull items
            #region Obisidan Skull Items
            // Lucky horseshoes
            RecipeGroup.RegisterGroup("AccessoriesPlus:LuckyHorseshoes", new RecipeGroup(() =>
                "Any Lucky Horseshoe",
                new int[]
                {
                    ItemID.LuckyHorseshoe,
                    ItemID.ObsidianHorseshoe
                }));

            // Obsidian roses
            RecipeGroup.RegisterGroup("AccessoriesPlus:ObsidianRoses", new RecipeGroup(() =>
                "Any Obsidian Rose",
                new int[]
                {
                    ItemID.ObsidianRose,
                    ItemID.ObsidianSkullRose,
                    ItemID.MoltenSkullRose
                }));

            // Lava charms
            RecipeGroup.RegisterGroup("AccessoriesPlus:LavaCharms", new RecipeGroup(() =>
                "Any Lava Charm",
                new int[]
                {
                    ItemID.LavaCharm,
                    ItemID.MoltenCharm
                }));

            // Magma stones
            RecipeGroup.RegisterGroup("AccessoriesPlus:MagmaStones", new RecipeGroup(() =>
                "Any Magma Stone",
                new int[]
                {
                    ItemID.MagmaStone,
                    ItemID.LavaSkull,
                    ItemID.MoltenSkullRose
                }));
            #endregion


            // Double jump balloons
            #region Double Jumps
            // Cloud balloons
            RecipeGroup.RegisterGroup("AccessoriesPlus:CloudBalloons", new RecipeGroup(() =>
                "Any Cloud in a Balloon",
                new int[]
                {
                    ItemID.CloudinaBalloon,
                    ItemID.BlueHorseshoeBalloon
                }));

            // Blizzard balloons
            RecipeGroup.RegisterGroup("AccessoriesPlus:BlizzardBalloons", new RecipeGroup(() =>
                "Any Blizzard in a Balloon",
                new int[]
                {
                    ItemID.BlizzardinaBalloon,
                    ItemID.WhiteHorseshoeBalloon
                }));

            // Sandstorm balloons
            RecipeGroup.RegisterGroup("AccessoriesPlus:SandstormBalloons", new RecipeGroup(() =>
                "Any Sandstorm in a Balloon",
                new int[]
                {
                    ItemID.SandstorminaBalloon,
                    ItemID.YellowHorseshoeBalloon
                }));

            // Fart balloons
            RecipeGroup.RegisterGroup("AccessoriesPlus:FartBalloons", new RecipeGroup(() =>
                "Any Fart in a Balloon",
                new int[]
                {
                    ItemID.FartInABalloon,
                    ItemID.BalloonHorseshoeFart
                }));

            // Tsunami balloons
            RecipeGroup.RegisterGroup("AccessoriesPlus:TsunamiBalloons", new RecipeGroup(() =>
                "Any Sharkron Balloon",
                new int[]
                {
                    ItemID.SharkronBalloon,
                    ItemID.BalloonHorseshoeSharkron
                }));
            #endregion

            // Sprinting boots
            #region Sprinting Boots
            RecipeGroup.RegisterGroup("AccessoriesPlus:SprintingBoots", new RecipeGroup(() =>
                "Any Basic Sprinting Boots",
                new int[]
                {
                    ItemID.HermesBoots,
                    ItemID.SandBoots,
                    ItemID.SailfishBoots,
                    ItemID.FlurryBoots
                }));
            #endregion
        }
    }
}