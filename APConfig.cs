using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System;
using System.Collections;
using System.Collections.Generic;

using Terraria.ModLoader.Config;
using System.ComponentModel;


namespace AccessoriesPlus
{
    [Label("Accessories+ Config")]
    public class APConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;



        // Stats in tooltips
        [Header("Tooltip Stats")]

        // Wings
        [Label("[i:4978]  Wings")]
        [Tooltip("When enabled, wings will display their stats in their tooltips")]
        [DefaultValue(true)]
        public bool wingStats;

        // Hooks
        [Label("[i:84]  Hooks")]
        [Tooltip("When enabled, hooks will display their stats in their tooltips")]
        [DefaultValue(true)]
        public bool hookStats;



        // Better Accessories
        [Header("Better Accessories (Reload required)")]

        // Terraspark Boots
        [Label("[i:5000]  Terraspark Boots")]
        [Tooltip("When enabled, the Terraspark Boots will have a different crafting tree and will be better")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterTerrasparkBoots;

        // Ankh Shield
        [Label("[i:1613]  Ankh Shield")]
        [Tooltip("When enabled, the Ankh Shield will have a different crafting tree and will be better")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterAnkhShield;

        // Bundle of Balloons
        [Label("[i:1164]  Bundle of Balloons")]
        [Tooltip("When enabled, the Bundle of Balloons will have a different crafting tree and will be better")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterBundleOfBalloons;



        /*/ Misc
        [Header("Miscellaneous (Reload required)")]

        // Better prefixes
        [Label("[i:575]  Better Prefix Crafting")]
        [Tooltip("When enabled, any accessory that is crafted will gain the prefix of the accessory used to craft it\nIf there are multiple available prefixes, one is chosen at random")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool betterPrefixes;

        // Better obtainability
        [Label("[i:575]  Better Accessory Obtainability")]
        [Tooltip("When enabled, all accessories can be obtained in a playthrough and are more common")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool obtainableAccessories;*/
    }
}