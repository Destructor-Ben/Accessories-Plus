using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace AccessoriesPlus
{
    [Label("Accessories+ Config")]
    public class APServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // Header
        [Header("Configuration")]

        // Web slinger
        [Label("[i:939]  Better Web Slinger")]
        [Tooltip("When enabled, the Web Slinger allows you to swing like Spiderman\nReload Required")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterWebSlinger;

        /*/ Flying carpet
        [Label("[i:939]  Better Flying Carpet")]
        [Tooltip("When enabled, the Flying Carpet acts more like the Celestial Starboard and is considered a wing type accessory\nReload Required")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterFlyingCarpet;*/

        // Hook stats
        [Label("[i:84]  Hook Stats")]
        [Tooltip("When enabled, hooks display their stats in their tooltips")]
        [DefaultValue(true)]
        public bool hookStats;

        // Wing stats
        [Label("[i:4978]  Wing Stats")]
        [Tooltip("When enabled, wings display their stats in their tooltips")]
        [DefaultValue(true)]
        public bool wingsStats;

        // Terraspark boots
        [Label("[i:5000]  Better Terraspark Boots")]
        [Tooltip("When enabled, the Terraspark boots are buffed and have a different crafting tree\nReload Required")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterTerrasparkBoots;

        // Ankh shield
        [Label("[i:1613]  Better Ankh Shield")]
        [Tooltip("When enabled, the Ankh Shield is buffed and has a different crafting tree\nReload Required")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterAnkhShield;

        // Bundle of balloons
        [Label("[i:1164]  Better Bundle of Balloons")]
        [Tooltip("When enabled, the Bundle of Balloons is buffed and has a different crafting tree\nReload Required")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterBundleOfBalloons;

        // Obsidian skull recipes
        [Label("[i:193]  Better Obsidian Skull Recipes")]
        [Tooltip("When enabled, any recipe requiring an item that can be upgraded with an Obsidian Skull can also have the Obsidian Skull upgraded version used in the recipe instead of the original,\nthough this does not give the resulting item the effects of the Obsidian Skull,\ne.g recipes that require the Magma Stone can also use the Magma Skull and Molten Skull Rose as an alternative.\nReload Required")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterObSkullRecipes;
    }
}
