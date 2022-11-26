using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

using System;
using System.Collections;
using System.Collections.Generic;

using log4net;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using Terraria.Audio;

namespace AccessoriesPlus
{
	public class AccessoriesPlus : Mod
    {
        #region Setup

        bool stripRecipeData = true;

        public static APConfig Config { get; private set; }

        public static Dictionary<int, APCustomAccessory> CustomAccessories;

        public static Dictionary<int, string[]> WingStats;
        public static Dictionary<int, string[]> HookStats;

        private Asset<Texture2D> vanillaBundleOfBalloonsTexture;
        private Asset<Texture2D> vanillaBundleOfBalloonsAccTexture;

        public override void Load()
        {
            Config = ModContent.GetInstance<APConfig>();

            CustomAccessories = new();

            WingStats = new();
            HookStats = new();


            // Stripping recipe data
            if (stripRecipeData)
                RecipeDataStripper.StripData(Main.recipe, "recipes.json", this);

            // Wing and hook stats
            AddWingStats();
            AddHookStats();

            // Adding custom accessories
            if (Config.betterTerrasparkBoots)
                ModifyTerrasparkBoots();
            if (Config.betterAnkhShield)
                ModifyAnkhShield();
            if (Config.betterBundleOfBalloons)
                ModifyBalloons();

            // Texture replacement
            vanillaBundleOfBalloonsTexture = TextureAssets.Item[ItemID.BundleofBalloons];
            vanillaBundleOfBalloonsAccTexture = TextureAssets.AccBalloon[3];

            if (Config.betterBundleOfBalloons)
            {
                TextureAssets.Item[ItemID.BundleofBalloons] = ModContent.Request<Texture2D>("AccessoriesPlus/CustomItemTextures/BundleofBalloons");
                TextureAssets.AccBalloon[3] = ModContent.Request<Texture2D>("AccessoriesPlus/CustomItemTextures/BundleofBalloons_Balloon");
            }

            #region Recipe Prefixes
            /*/ Recipe prefixes
            On.Terraria.Main.CraftItem += (orig, r) => {
                if (!Config.betterPrefixes)
                {
                    orig(r);
                    return;
                }

                // Main code
                int stack = Main.mouseItem.stack;
                Main.mouseItem = r.createItem.Clone();
                Main.mouseItem.stack += stack;

                // MODIFIED to include if it doesnt have accessory TODO CHANGE
                if (stack <= 0 && Main.mouseItem.maxStack == 1 && !Main.mouseItem.accessory)
                {
                    Main.mouseItem.Prefix(-1);
                }

                // ADDED the part that gets the prefix of its items =======================================TODO MAKE IT WORK ON ALL ITEMS THAT CAN HAVE PREFIXES
                if (Main.mouseItem.accessory)
                {
                    // Finding the possible prefixes TODO============
                    List<int> possiblePrefixes = new() { 0, 1, 2 };

                    // Giving it a normal prefix if it doesn't have any
                    if (possiblePrefixes.Count == 0)
                    {
                        Main.mouseItem.Prefix(-1);
                    }

                    // Giving it one of the prefixes in the list
                    Main.mouseItem.prefix = possiblePrefixes[Main.rand.Next(0, possiblePrefixes.Count)];
                }

                Main.mouseItem.position.X = Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2) - (float)(Main.mouseItem.width / 2);
                Main.mouseItem.position.Y = Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2) - (float)(Main.mouseItem.height / 2);
                PopupText.NewText(PopupTextContext.ItemCraft, Main.mouseItem, r.createItem.stack);
                r.Create();
                if (Main.mouseItem.type > ItemID.None || r.createItem.type > ItemID.None)
                {
                    ItemLoader.OnCreate(Main.mouseItem, new RecipeCreationContext
                    {
                        recipe = r
                    });
                    RecipeLoader.OnCraft(Main.mouseItem, r);
                    SoundEngine.PlaySound(SoundID.SoundByIndex[7]); // TODO=================================================
                }
            };
            //*/
            #endregion
        }

        public override void Unload()
        {
            Config = null;

            CustomAccessories = null;

            WingStats = null;
            HookStats = null;

            TextureAssets.Item[ItemID.BundleofBalloons] = vanillaBundleOfBalloonsTexture;
            TextureAssets.AccBalloon[3] = vanillaBundleOfBalloonsAccTexture;
            vanillaBundleOfBalloonsTexture = null;
            vanillaBundleOfBalloonsAccTexture = null;
        }

        public override void PostSetupContent()
        {
            // Stripping modded recipe data
            if (stripRecipeData)
                RecipeDataStripper.StripData(Main.recipe, "recipes_modded.json", this);
        }

        #endregion


        #region Static Functions
        // Finds the index of the tooltip name
        public static int FindIndexOfTooltipName(string tooltipName, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Name == tooltipName)
                {
                    return i;
                }
            }
            return -1;
        }

        // Inserts a list of tooltip lines after the specified tooltip name - returns true if it succeeds
        public static bool InsertTooltips(List<TooltipLine> tooltipsToInsert, string nameToInsertAfter, List<TooltipLine> tooltips)
        {
            int index = FindIndexOfTooltipName(nameToInsertAfter, tooltips);
            if (index != -1)
            {
                tooltips.InsertRange(index + 1, tooltipsToInsert);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region Content
        // Making terraspark boots better
        void ModifyTerrasparkBoots()
        {
            CustomAccessories.Add(ItemID.TerrasparkBoots, new APCustomAccessory(
                (item) => { },

                (item, player, hideVisual) =>
                {
                    player.noFallDmg = true;
                    player.autoJump = true;
                    player.frogLegJumpBoost = true;
                },

                (item, tooltips) =>
                {
                    List<TooltipLine> newInfo = new() {
                        new TooltipLine(this, "NoFallDmg", "Negates fall damage"),
                        new TooltipLine(this, "FrogLeg", "Increases jump speed and allows auto-jump"),
                        new TooltipLine(this, "FallRes", "Increases fall resistance")
                    };

                    if (!InsertTooltips(newInfo, "Tooltip4", tooltips))
                    {
                        Logger.Error($"Terraspark Boots couldn't have tooltip modified");
                    }
                },

                () =>
                {
                    Recipe.Create(ItemID.TerrasparkBoots)
                    .AddTile(TileID.TinkerersWorkbench)
                    .AddIngredient(ItemID.FrostsparkBoots)
                    .AddIngredient(ItemID.LavaWaders)
                    .AddIngredient(ItemID.AmphibianBoots)
                    .AddIngredient(ItemID.LuckyHorseshoe)
                    .Register();

                    Recipe.Create(ItemID.TerrasparkBoots)
                    .AddTile(TileID.TinkerersWorkbench)
                    .AddIngredient(ItemID.FrostsparkBoots)
                    .AddIngredient(ItemID.HellfireTreads)
                    .AddIngredient(ItemID.AmphibianBoots)
                    .AddIngredient(ItemID.LuckyHorseshoe)
                    .Register();
                },

                (recipe) =>
                {
                    recipe.DisableRecipe();
                }
            ));
        }


        // Making ankh shield better
        void ModifyAnkhShield()
        {
            // Ankh shield
            CustomAccessories.Add(ItemID.AnkhShield, new APCustomAccessory(
                (item) => { },

                (item, player, hideVisual) =>
                {
                    // Hand warmer effects
                    player.buffImmune[46] = true;
                    player.buffImmune[47] = true;

                    // Reflective blindfold effects
                    player.buffImmune[22] = true;
                    player.buffImmune[156] = true;

                    // On fire
                    player.buffImmune[24] = true;
                    // Cursed inferno
                    player.buffImmune[39] = true;
                    // Ichor
                    player.buffImmune[69] = true;
                    // Acid venom
                    player.buffImmune[70] = true;
                },

                (item, tooltips) => { },

                () => { },

                (recipe) => { }
            ));

            // Ankh charm
            CustomAccessories.Add(ItemID.AnkhCharm, new APCustomAccessory(
                (item) => { },

                (item, player, hideVisual) =>
                {
                    // Hand warmer effects
                    player.buffImmune[46] = true;
                    player.buffImmune[47] = true;

                    // Reflective blindfold effects
                    player.buffImmune[22] = true;
                    player.buffImmune[156] = true;
                },

                (item, tooltips) => {},

                () =>
                {
                    Recipe.Create(ItemID.AnkhCharm)
                    .AddTile(TileID.TinkerersWorkbench)
                    .AddIngredient(ItemID.ArmorBracing)
                    .AddIngredient(ItemID.MedicatedBandage)
                    .AddIngredient(ItemID.CountercurseMantra)
                    .AddIngredient(ItemID.ThePlan)
                    .AddIngredient(ItemID.Blindfold)
                    .AddIngredient(ItemID.HandWarmer)
                    .AddIngredient(ItemID.PocketMirror)
                    .Register();
                },

                (recipe) =>
                {
                    recipe.DisableRecipe();
                }
            ));
        }


        // Making bundle of balloons better
        void ModifyBalloons()
        {
            CustomAccessories.Add(ItemID.BundleofBalloons, new APCustomAccessory(
                // Changing defaults
                (item) =>
                {
                    item.SetNameOverride("Bundle of Horseshoe Balloons");
                },

                // Changing effects on player
                (item, player, hideVisual) =>
                {
                    player.hasJumpOption_Fart = true;
                    player.hasJumpOption_Sail = true;
                    player.noFallDmg = true;
                },

                // Changing tooltip
                (item, tooltips) =>
                {
                    int index = FindIndexOfTooltipName("Tooltip0", tooltips);
                    if (index == -1)
                        return;
                    tooltips[index].Text = "Allows the holder to sextuple jump";
                    tooltips.Insert(index + 1, new TooltipLine(this, "TooltipNoFallDmgModded", "Negates fall damage"));
                },

                // Creating new recipe
                () =>
                {
                    Recipe.Create(ItemID.BundleofBalloons)
                    .AddTile(TileID.TinkerersWorkbench)
                    .AddRecipeGroup("AccessoriesPlus:LuckyHorseshoes")
                    .AddRecipeGroup("AccessoriesPlus:CloudBalloons")
                    .AddRecipeGroup("AccessoriesPlus:BlizzardBalloons")
                    .AddRecipeGroup("AccessoriesPlus:SandstormBalloons")
                    .AddRecipeGroup("AccessoriesPlus:FartBalloons")
                    .AddRecipeGroup("AccessoriesPlus:TsunamiBalloons")
                    .Register();
                },

                // Disabling old recipe
                (recipe) =>
                {
                    recipe.DisableRecipe();
                }
            ));
        }


        // Item stats
        // Adds wing stats
        void AddWingStats()
        {
            WingStats = new() {
                { 4978, new string[] { "0.42", "18", "15" } },
                { 493, new string[] { "1.67", "53", "32" } },
                { 492, new string[] { "1.67", "53", "32" } },
                { 1162, new string[] { "1.67", "52", "32" } },
                { 761, new string[] { "2.17", "67", "34" } },
                { 2494, new string[] { "2.17", "67", "34" } },
                { 822, new string[] { "2.17", "67", "34" } },
                { 785, new string[] { "2.17", "67", "34" } },
                { 748, new string[] { "2.5", "77", "33" } },
                { 665, new string[] { "2.5", "77", "36" } },
                { 1583, new string[] { "2.5", "77", "36" } },
                { 1584, new string[] { "2.5", "77", "36" } },
                { 1585, new string[] { "2.5", "77", "36" } },
                { 1586, new string[] { "2.5", "77", "36" } },
                { 3228, new string[] { "2.5", "77", "36" } },
                { 3580, new string[] { "2.5", "77", "36" } },
                { 3582, new string[] { "2.5", "77", "36" } },
                { 3588, new string[] { "2.5", "77", "36" } },
                { 3592, new string[] { "2.5", "77", "36" } },
                { 3924, new string[] { "2.5", "77", "36" } },
                { 3928, new string[] { "2.5", "77", "36" } },
                { 4730, new string[] { "2.5", "77", "36" } },
                { 4746, new string[] { "2.5", "77", "36" } },
                { 4750, new string[] { "2.5", "77", "36" } },
                { 4754, new string[] { "2.5", "77", "36" } },
                { 1165, new string[] { "2.67", "81", "38" } },
                { 1515, new string[] { "2.67", "81", "38" } },
                { 749, new string[] { "2.67", "81", "38" } },
                { 821, new string[] { "2.67", "81", "38" } },
                { 1866, new string[] { "2.83", "94", "38" } },
                { 786, new string[] { "2.83", "94", "38" } },
                { 2770, new string[] { "2.83", "94", "38" } },
                { 823, new string[] { "2.83", "94", "38" } },
                { 2280, new string[] { "2.83", "94", "38" } },
                { 1871, new string[] { "3", "107", "38" } },
                { 1830, new string[] { "3", "107", "38" } },
                { 1797, new string[] { "3", "107", "38" } },
                { 948, new string[] { "3", "107", "38" } },
                { 3883, new string[] { "2.5", "119", "250" } },
                { 4823, new string[] { "2.5", "128", "41" } },
                { 2609, new string[] { "3", "143", "41" } },
                { 3470, new string[] { "3", "143", "33" } },
                { 3469, new string[] { "3", "143", "33" } },
                { 3468, new string[] { "3", "167", "46" } },
                { 3471,  new string[] { "3", "167", "46" } },
                { 4954, new string[]{ "3", "201", "41" } }
            };
        }

        // Adds hook stats
        void AddHookStats()
        {
            HookStats = new() {
                { 84, new string[] { "18.75", "11.5", "1", "Single" } },
                { 1236, new string[] { "18.75", "10", "1", "Single" } },
                { 4759, new string[] { "19", "11.5", "1", "Single" } },
                { 1237, new string[] { "20.625", "10.5", "1", "Single" } },
                { 1238, new string[] { "22.5", "11", "1", "Single" } },
                { 1239, new string[] { "24.375", "11.5", "1", "Single" } },
                { 1240, new string[] { "26.25", "12", "1", "Single" } },
                { 4257, new string[] { "27.5", "12.5", "1", "Single" } },
                { 1241, new string[] { "29.125", "12.5", "1", "Single" } },
                { 939, new string[] { "31.25", "20", "2", "Individual" } }, // Web slinger
                { 1273, new string[] { "21.875", "15", "2", "Simultaneous" } },
                { 2585, new string[] { "18.75", "13", "3", "Simultaneous" } },
                { 2360, new string[] { "25", "13", "2", "Simultaneous" } },
                { 185, new string[] { "28", "13", "3", "Simultaneous" } },
                { 1800, new string[] { "31.25", "13.5", "1", "Single" } },
                { 1915, new string[] { "25", "11.5", "1", "Single" } },
                { 437, new string[] { "27.5", "14", "2", "Individual" } },
                { 4980, new string[] { "30", "16", "1", "Single" } },
                { 3021, new string[] { "30", "16", "3", "Simultaneous" } },
                { 3022, new string[] { "30", "15", "3", "Simultaneous" } },
                { 3023, new string[] { "30", "15", "3", "Simultaneous" } },
                { 3020, new string[] { "30", "15", "3", "Simultaneous" } },
                { 2800, new string[] { "31.25", "14", "3", "Simultaneous" } },
                { 1829, new string[] { "34.375", "15.5", "3", "Simultaneous" } },
                { 1916, new string[] { "34.375", "15.5", "3", "Simultaneous" } },
                { 3572, new string[] { "34.375", "18", "4", "Simultaneous" } },
                { 3623, new string[] { "37.5", "16", "2", "Individual" } }
            };
        }
        #endregion
    }
}