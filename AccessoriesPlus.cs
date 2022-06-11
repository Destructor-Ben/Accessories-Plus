using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using AccessoriesPlus.Items;
using System.Collections.Generic;
using log4net;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using System;
using MonoMod.Cil;
using Mono.Cecil.Cil;

namespace AccessoriesPlus
{
	public class AccessoriesPlus : Mod
    {
        // Item stats stuff
        public static List<int> hookItemIDS;
        public static List<int> wingItemIDS;

        public static List<string[]> hookItemStats;
        public static List<string[]> wingItemStats;

        // Texture replacement stuff
        private Asset<Texture2D> vanillaBundleOfBalloonsTexture;
        private Asset<Texture2D> vanillaBundleOfBalloonsAccTexture;

        public override void Load()
        {
            // Item stats
            hookItemIDS = new List<int>()
            {
                84,
                1236,
                4759,
                1237,
                1238,
                1239,
                1240,
                4257,
                1241,
                939,
                1273,
                2585,
                2360,
                185,
                1800,
                1915,
                437,
                4980,
                3021,
                3022,
                3023,
                3020,
                2800,
                1829,
                1916,
                3572,
                3623
            };
            wingItemIDS = new List<int>()
            {
                4978,
                493,
                492,
                1162,
                761,
                2494,
                822,
                785,
                748,
                665,
                1583,
                1584,
                1585,
                1586,
                3228,
                3580,
                3582,
                3588,
                3592,
                3924,
                3928,
                4730,
                4746,
                4750,
                4754,
                1165,
                1515,
                749,
                821,
                1866,
                786,
                2770,
                823,
                2280,
                1871,
                1830,
                1797,
                948,
                3883,
                4823,
                2609,
                3470,
                3469,
                3468,
                3471,
                4954
            };

            hookItemStats = new List<string[]>()
            {
                new string[]{ "18.75", "11.5", "1", "Single" },
                new string[]{ "18.75", "10", "1", "Single" },
                new string[]{ "19", "11.5", "1", "Single" },
                new string[]{ "20.625", "10.5", "1", "Single" },
                new string[]{ "22.5", "11", "1", "Single" },
                new string[]{ "24.375", "11.5", "1", "Single" },
                new string[]{ "26.25", "12", "1", "Single" },
                new string[]{ "27.5", "12.5", "1", "Single" },
                new string[]{ "29.125", "12.5", "1", "Single" },
                new string[]{ "31.25", "20", "2", "Individual" }, // Web slinger
                new string[]{ "21.875", "15", "2", "Simultaneous" },
                new string[]{ "18.75", "13", "3", "Simultaneous" },
                new string[]{ "25", "13", "2", "Simultaneous" },
                new string[]{ "28", "13", "3", "Simultaneous" },
                new string[]{ "31.25", "13.5", "1", "Single" },
                new string[]{ "25", "11.5", "1", "Single" },
                new string[]{ "27.5", "14", "2", "Individual" },
                new string[]{ "30", "16", "1", "Single" },
                new string[]{ "30", "16", "3", "Simultaneous" },
                new string[]{ "30", "15", "3", "Simultaneous" },
                new string[]{ "30", "15", "3", "Simultaneous" },
                new string[]{ "30", "15", "3", "Simultaneous" },
                new string[]{ "31.25", "14", "3", "Simultaneous" },
                new string[]{ "34.375", "15.5", "3", "Simultaneous" },
                new string[]{ "34.375", "15.5", "3", "Simultaneous" },
                new string[]{ "34.375", "18", "4", "Simultaneous" },
                new string[]{ "37.5", "16", "2", "Individual" },
            };
            wingItemStats = new List<string[]>()
            {
                new string[]{ "0.42", "18", "15"},
                new string[]{ "1.67", "53", "32"},
                new string[]{ "1.67", "53", "32"},
                new string[]{ "1.67", "52", "32"},
                new string[]{ "2.17", "67", "34"},
                new string[]{ "2.17", "67", "34"},
                new string[]{ "2.17", "67", "34"},
                new string[]{ "2.17", "67", "34"},
                new string[]{ "2.5", "77", "33"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.67", "81", "38"},
                new string[]{ "2.67", "81", "38"},
                new string[]{ "2.67", "81", "38"},
                new string[]{ "2.67", "81", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "3", "107", "38"},
                new string[]{ "3", "107", "38"},
                new string[]{ "3", "107", "38"},
                new string[]{ "3", "107", "38"},
                new string[]{ "2.5", "119", "250"},
                new string[]{ "2.5", "128", "41"},
                new string[]{ "3", "143", "41"},
                new string[]{ "3", "143", "33"},
                new string[]{ "3", "143", "33"},
                new string[]{ "3", "167", "46"},
                new string[]{ "3", "167", "46"},
                new string[]{ "3", "201", "41"}
            };

            // If better web slinger is enabled change its stats
            if (!ModContent.GetInstance<APServerConfig>().betterWebSlinger)
            {
                hookItemStats[IDToIndex(ItemID.WebSlinger)] = new string[] { "22.625", "10", "8", "Simultaneous" };
            }

            // Texture replacement
            // Bundle of balloons
            vanillaBundleOfBalloonsTexture = TextureAssets.Item[ItemID.BundleofBalloons];
            vanillaBundleOfBalloonsAccTexture = TextureAssets.AccBalloon[3];
            if (ModContent.GetInstance<APServerConfig>().betterBundleOfBalloons)
            {
                TextureAssets.Item[ItemID.BundleofBalloons] = ModContent.Request<Texture2D>("AccessoriesPlus/Items/BundleofBalloons");
                TextureAssets.AccBalloon[3] = ModContent.Request<Texture2D>("AccessoriesPlus/Items/BundleofBalloons_Balloon");
            }

            // Hooking
            //On.Terraria.Player.CarpetMovement += HookCarpetMovement;
        }

        public override void Unload()
        {
            // Item stats
            hookItemIDS = null;
            wingItemIDS = null;

            hookItemStats = null;
            wingItemStats = null;

            // Texture replacement
            // Bundle of balloons
            TextureAssets.Item[ItemID.BundleofBalloons] = vanillaBundleOfBalloonsTexture;
            TextureAssets.AccBalloon[3] = vanillaBundleOfBalloonsAccTexture;
            vanillaBundleOfBalloonsTexture = null;
            vanillaBundleOfBalloonsAccTexture = null;
        }


        // Hooks
        /*/ Flying carpet movement
        private void HookCarpetMovement(On.Terraria.Player.orig_CarpetMovement orig, Player self)
        {
            if (ModContent.GetInstance<APServerConfig>().betterFlyingCarpet)
            {
                bool isCarpeting = false;
                // If carpet can activate
                if (self.grappling[0] == -1 && self.carpet && !self.canJumpAgain_Cloud && !self.canJumpAgain_Sandstorm && !self.canJumpAgain_Blizzard && !self.canJumpAgain_Fart && !self.canJumpAgain_Sail && !self.canJumpAgain_Unicorn && !self.canJumpAgain_Santank && !self.canJumpAgain_WallOfFleshGoat && !self.canJumpAgain_Basilisk && self.jump == 0 && self.velocity.Y != 0f && self.rocketTime == 0 && self.wingTime == 0f && !self.mount.Active)
                {
                    // Runs when the player first activates the carpet
                    if (self.controlJump && self.canCarpet)
                    {
                        self.canCarpet = false;
                        self.carpetTime = 300;
                    }

                    // If the carpet is being used
                    if (self.carpetTime > 0 && self.controlJump)
                    {
                        // Movement variables
                        self.fallStart = (int)(self.position.Y / 16f);
                        isCarpeting = true;
                        self.carpetTime--;

                        // Removing gravity
                        float gravity = self.gravity;
                        if (self.gravDir == 1f && self.velocity.Y > -gravity)
                        {
                            self.velocity.Y = -(gravity + 1E-06f);
                        }
                        else if (self.gravDir == -1f && self.velocity.Y < gravity)
                        {
                            self.velocity.Y = gravity + 1E-06f;
                        }

                        // Animation
                        self.carpetFrameCounter += 1f + Math.Abs(self.velocity.X * 0.5f);
                        if (self.carpetFrameCounter > 8f)
                        {
                            self.carpetFrameCounter = 0f;
                            self.carpetFrame++;
                        }
                        if (self.carpetFrame < 0)
                        {
                            self.carpetFrame = 0;
                        }
                        if (self.carpetFrame > 5)
                        {
                            self.carpetFrame = 0;
                        }
                    }
                }

                // Removing slowfall if carpeting, otherwise don't show the carpet
                if (!isCarpeting)
                {
                    self.carpetFrame = -1;
                }
                else
                {
                    self.slowFall = false;
                }
            }
            else
            {
                orig(self);
            }
        }*/


        // Returns true if the item type is found in the hook IDs list
        public static bool IsItemHook(int itemType)
        {
            foreach (int i in hookItemIDS)
            {
                if (i == itemType)
                {
                    return true;
                }
            }
            return false;
        }

        // Returns true if the item type is found in the wing IDs list
        public static bool IsItemWings(int itemType)
        {
            foreach (int i in wingItemIDS)
            {
                if (i == itemType)
                {
                    return true;
                }
            }
            return false;
        }

        // Returns an array of 4 strings that represent the hooks stats
        public static string[] GetHookStats(int itemID)
        {
            return hookItemStats[IDToIndex(itemID)];
        }

        // Returns an array of 4 strings that represent the hooks stats
        public static string[] GetWingStats(int itemID)
        {
            return wingItemStats[IDToIndex(itemID)];
        }

        // Converts an item ID to an index for the stats arrays
        public static int IDToIndex(int id)
        {
            int index = -1;

            if (IsItemHook(id))
            {
                for (int i = 0; i < hookItemIDS.Count; i++)
                {
                    if (hookItemIDS[i] == id)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if (IsItemWings(id))
            {
                for (int i = 0; i < wingItemIDS.Count; i++)
                {
                    if (wingItemIDS[i] == id)
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }


        // Recipes
        public override void AddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                // Ankh shield stuff
                if (ModContent.GetInstance<APServerConfig>().betterAnkhShield)
                {
                    // Ankh charm
                    if (recipe.HasResult(ItemID.AnkhCharm))
                    {
                        RemoveIngredientFromRecipe(recipe, ItemID.Blindfold);
                        recipe.AddIngredient(ModContent.GetInstance<ReflectiveBlindfold>());
                        recipe.AddIngredient(ItemID.HandWarmer);
                    }
                }


                // Terraspark boots stuff
                if (ModContent.GetInstance<APServerConfig>().betterTerrasparkBoots)
                {
                    // Amphibian boots
                    if (recipe.HasResult(ItemID.AmphibianBoots))
                    {
                        recipe.AddIngredient(ItemID.WaterWalkingBoots);
                        RemoveIngredientFromRecipe(recipe, ItemID.SailfishBoots);
                    }

                    // Spectre boots
                    if (recipe.HasResult(ItemID.SpectreBoots))
                    {
                        recipe.AddRecipeGroup("AccessoriesPlus:LuckyHorseshoes");
                    }

                    // Lava waders
                    if (recipe.HasResult(ItemID.LavaWaders))
                    {
                        recipe.DisableRecipe();
                    }

                    // Obsidian amphibian boots
                    if (recipe.HasResult(ItemID.ObsidianWaterWalkingBoots))
                    {
                        RemoveIngredientFromRecipe(recipe, ItemID.WaterWalkingBoots);
                        recipe.AddIngredient(ItemID.AmphibianBoots);
                    }
                }


                // Obsidian skull recipe stuff
                if (ModContent.GetInstance<APServerConfig>().betterObSkullRecipes)
                {
                    // Magma stone
                    if (recipe.HasIngredient(ItemID.MagmaStone))
                    {
                        RemoveIngredientFromRecipe(recipe, ItemID.MagmaStone);
                        recipe.AddRecipeGroup("AccessoriesPlus:MagmaStones");
                    }

                    // Lucky horseshoe
                    if (recipe.HasIngredient(ItemID.LuckyHorseshoe))
                    {
                        RemoveIngredientFromRecipe(recipe, ItemID.LuckyHorseshoe);
                        recipe.AddRecipeGroup("AccessoriesPlus:LuckyHorseshoes");
                    }

                    // Obsidian rose
                    if (recipe.HasIngredient(ItemID.ObsidianRose))
                    {
                        RemoveIngredientFromRecipe(recipe, ItemID.ObsidianRose);
                        recipe.AddRecipeGroup("AccessoriesPlus:ObsidianRoses");
                    }

                    // Lava charm
                    if (recipe.HasIngredient(ItemID.LavaCharm))
                    {
                        RemoveIngredientFromRecipe(recipe, ItemID.LavaCharm);
                        recipe.AddRecipeGroup("AccessoriesPlus:LavaCharms");
                    }
                }


                // Bundle of balloons
                if (ModContent.GetInstance<APServerConfig>().betterBundleOfBalloons)
                {
                    if (recipe.HasResult(ItemID.BundleofBalloons))
                    {
                        recipe.DisableRecipe();
                    }
                }
            }

            // Terraspark boots stuff
            if (ModContent.GetInstance<APServerConfig>().betterTerrasparkBoots)
            {
                // Lava waders recipe
                CreateRecipe(ItemID.LavaWaders)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.ObsidianWaterWalkingBoots)
                .AddRecipeGroup("AccessoriesPlus:LavaCharms")
                .AddRecipeGroup("AccessoriesPlus:ObsidianRoses")
                .Register();

                // Terraspark boots alternate recipe
                CreateRecipe(ItemID.TerrasparkBoots)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.FrostsparkBoots)
                .AddIngredient(ItemID.HellfireTreads)
                .Register();
            }

            // Bundle of balloons stuff
            if (ModContent.GetInstance<APServerConfig>().betterBundleOfBalloons)
            {
                // Bundle of balloons recipe
                CreateRecipe(ItemID.BundleofBalloons)
                .AddTile(TileID.TinkerersWorkbench)
                .AddRecipeGroup("AccessoriesPlus:LuckyHorseshoes")
                .AddRecipeGroup("AccessoriesPlus:CloudBalloons")
                .AddRecipeGroup("AccessoriesPlus:BlizzardBalloons")
                .AddRecipeGroup("AccessoriesPlus:SandstormBalloons")
                .AddRecipeGroup("AccessoriesPlus:FartBalloons")
                .AddRecipeGroup("AccessoriesPlus:TsunamiBalloons")
                .Register();
            }
        }

        // Recipe groups
        public override void AddRecipeGroups()
        {
            // Obsidian skull items
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


            // Double jump balloons
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
        }

        // Function to easily remove ingredients from a recipe
        public static void RemoveIngredientFromRecipe(Recipe recipe, int itemID)
        {
            recipe.TryGetIngredient(itemID, out Item ingredient);
            recipe.RemoveIngredient(ingredient);
        }
    }
}