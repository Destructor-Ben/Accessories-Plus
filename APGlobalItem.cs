using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AccessoriesPlus
{
	public class APGlobalItem : GlobalItem
	{
        // Changing vanilla items basic stats
        public override void SetDefaults(Item item)
        {
            // Web slinger
            if (ModContent.GetInstance<APServerConfig>().betterWebSlinger)
            {
                if (item.type == ItemID.WebSlinger)
                {
                    item.shoot = ModContent.ProjectileType<Projectiles.WebSlingerHook>();
                    item.shootSpeed = 20f;
                }
            }

            // Terraspark boots stuff
            if (ModContent.GetInstance<APServerConfig>().betterTerrasparkBoots)
            {
                // Obsidian amphibian boots
                if (item.type == ItemID.ObsidianWaterWalkingBoots)
                {
                    item.SetNameOverride("Obsidian Amphibian Boots");
                }

                // Amphibian boots
                if (item.type == ItemID.AmphibianBoots)
                {
                    item.rare = ItemRarityID.LightRed;
                }
            }

            // Bundle of balloons
            if (ModContent.GetInstance<APServerConfig>().betterBundleOfBalloons)
            {
                if (item.type == ItemID.BundleofBalloons)
                {
                    item.SetNameOverride("Bundle of Horseshoe Balloons");
                }
            }
            
        }

        // Changing vanilla accessories effects
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            // Ankh shield stuff
            if (ModContent.GetInstance<APServerConfig>().betterAnkhShield)
            {
                // Ankh shield / charm
                if (item.type == ItemID.AnkhCharm || item.type == ItemID.AnkhShield)
                {
                    // Hand warmer effects
                    player.buffImmune[46] = true;
                    player.buffImmune[47] = true;

                    // Reflective blindfold effects
                    player.buffImmune[22] = true;
                    player.buffImmune[156] = true;
                }

                // Ankh shield special buffs
                if (item.type == ItemID.AnkhShield)
                {
                    // On fire
                    player.buffImmune[24] = true;
                    // Cursed inferno
                    player.buffImmune[39] = true;
                    // Ichor
                    player.buffImmune[69] = true;
                    // Acid venom
                    player.buffImmune[70] = true;
                }
            }

            // Terraspark boots
            if (ModContent.GetInstance<APServerConfig>().betterTerrasparkBoots)
            {
                // Amphibian boots
                if (item.type == ItemID.AmphibianBoots)
                {
                    // Allow walking on water instead of sprinting
                    player.waterWalk2 = true;
                    player.accRunSpeed = player.maxRunSpeed;
                }

                // Spectre boots and its upgrades
                if (item.type == ItemID.SpectreBoots || item.type == ItemID.LightningBoots || item.type == ItemID.FrostsparkBoots || item.type == ItemID.TerrasparkBoots || item.type == ItemID.FairyBoots)
                {
                    player.noFallDmg = true;
                }

                // Obsidian amphibian boots and its upgrades
                if (item.type == ItemID.ObsidianWaterWalkingBoots || item.type == ItemID.LavaWaders || item.type == ItemID.HellfireTreads || item.type == ItemID.TerrasparkBoots)
                {
                    player.autoJump = true;
                    player.frogLegJumpBoost = true;
                }
            }

            // Bundle of balloons
            if (ModContent.GetInstance<APServerConfig>().betterBundleOfBalloons)
            {
                if (item.type == ItemID.BundleofBalloons)
                {
                    player.hasJumpOption_Fart = true;
                    player.hasJumpOption_Sail = true;
                    player.noFallDmg = true;
                }
            }


        }

        // Changing vanilla items tooltips
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Hooks
            if (ModContent.GetInstance<APServerConfig>().hookStats)
            {
                if (AccessoriesPlus.IsItemHook(item.type))
                {
                    int index = FindIndexOfTooltipName("Equipable", tooltips);
                    if (index != -1)
                    {
                        index++;
                        string[] stats = AccessoriesPlus.GetHookStats(item.type);

                        tooltips.Insert(index, new TooltipLine(Mod, "Reach", stats[0] + " tiles reach"));
                        tooltips.Insert(index + 1, new TooltipLine(Mod, "Velocity", stats[1] + " velocity"));
                        tooltips.Insert(index + 2, new TooltipLine(Mod, "NumHooks", stats[2] + (int.Parse(stats[2]) == 1 ? " hook" : " hooks")));
                        tooltips.Insert(index + 3, new TooltipLine(Mod, "LatchingMode", stats[3] + " hook latching"));

                        // Additional tooltip for web slinger
                        if (ModContent.GetInstance<APServerConfig>().betterWebSlinger && item.type == ItemID.WebSlinger)
                            tooltips.Insert(index + 4, new TooltipLine(Mod, "ModdedDescription", "Allows you to sling webs like Spiderman"));

                    }
                }
            }

            // Wings
            if (ModContent.GetInstance<APServerConfig>().wingsStats)
            {
                if (AccessoriesPlus.IsItemWings(item.type))
                {
                    int index = FindIndexOfTooltipName("Tooltip0", tooltips);
                    if (index != -1)
                    {
                        string[] stats = AccessoriesPlus.GetWingStats(item.type);

                        tooltips.Insert(index + 1, new TooltipLine(Mod, "NegateFallDmg", "Negates fall damage"));
                        tooltips.Insert(index, new TooltipLine(Mod, "FlightTime", stats[0] + " seconds flight time"));
                        tooltips.Insert(index + 1, new TooltipLine(Mod, "FlightHeight", stats[1] + " tiles flight height"));
                        tooltips.Insert(index + 2, new TooltipLine(Mod, "MaxHorizontalSpeed", stats[2] + " mph maximum horizontal speed"));
                    }
                }
            }


            // Terraspark boots stuff
            if (ModContent.GetInstance<APServerConfig>().betterTerrasparkBoots)
            {
                // Amphibian boots
                if (item.type == ItemID.AmphibianBoots)
                {
                    int index = FindIndexOfTooltipName("Tooltip0", tooltips);
                    if (index != -1)
                    {
                        // Replace the sprinting with walking in the tooltip
                        tooltips[index].Text = "Provides the ability to walk on water and honey";
                    }
                }

                // Spectre boots and its upgrades
                if (item.type == ItemID.SpectreBoots || item.type == ItemID.LightningBoots || item.type == ItemID.FrostsparkBoots || item.type == ItemID.TerrasparkBoots || item.type == ItemID.FairyBoots)
                {
                    int index = FindIndexOfTooltipName("Tooltip0", tooltips);
                    if (index != -1)
                    {
                        tooltips.Insert(index + 1, new TooltipLine(Mod, "NegateFallDmg", "Negates fall damage"));
                    }
                }

                // Obsidian amphibian boots and its upgrades
                if (item.type == ItemID.ObsidianWaterWalkingBoots || item.type == ItemID.LavaWaders || item.type == ItemID.HellfireTreads || item.type == ItemID.TerrasparkBoots)
                {
                    int index = FindIndexOfTooltipName("Tooltip0", tooltips);
                    if (index != -1)
                    {
                        tooltips.Insert(index + 1, new TooltipLine(Mod, "NegateFallDmg", "Increases jump speed and allows auto-jump"));
                        if (item.type != ItemID.TerrasparkBoots)
                            tooltips.Insert(index + 2, new TooltipLine(Mod, "NegateFallDmg", "Increases fall resistance"));
                    }
                }
            }

            // Bundle of balloons
            if (ModContent.GetInstance<APServerConfig>().betterBundleOfBalloons)
            {
                if (item.type == ItemID.BundleofBalloons)
                {
                    int index = FindIndexOfTooltipName("Tooltip0", tooltips);
                    if (index != -1)
                    {
                        tooltips[index].Text = "Allows the holder to sextuple jump";
                        tooltips.Insert(index + 1, new TooltipLine(Mod, "TooltipNoFallDmgModded", "Negates fall damage"));
                    }
                }
            }
        }

        // Find the index of the tooltip name
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
    }
}