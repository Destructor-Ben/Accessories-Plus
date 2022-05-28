using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AccessoriesPlus.Items;

namespace AccessoriesPlus
{
	public class APGlobalItem : GlobalItem
	{
        // Changing vanilla items basic stats
        public override void SetDefaults(Item item)
        {
            // Web slinger
            if (item.type == ItemID.WebSlinger)
            {
                item.shoot = ModContent.ProjectileType<Projectiles.WebSlingerHook>();
                item.shootSpeed = 20f;
            }
        }

        // Changing vanilla recipes
        public override void AddRecipes()
        {
            for (int i = 0; i < Main.recipe.Length; i++)
            {
                Recipe recipe = Main.recipe[i];

                // Ankh charm
                if (recipe.HasResult(ItemID.AnkhCharm))
                {
                    recipe.requiredItem[4] = new Item(ModContent.GetInstance<MagicMitten>().Type);
                    recipe.AddIngredient(ModContent.GetInstance<ReflectiveBlindfold>());
                }

                // Terraspark boots
                if (recipe.HasResult(ItemID.TerrasparkBoots))
                {
                    recipe.requiredItem[1] = new Item(ItemID.HellfireTreads);
                }

                // Lava waders
                if (recipe.HasResult(ItemID.LavaWaders))
                {
                    recipe.RemoveRecipe();
                }
            }

            // Lava waders recipe
            Mod.CreateRecipe(ItemID.LavaWaders)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.MoltenCharm)
                .AddIngredient(ItemID.ObsidianRose)
                .AddIngredient(ItemID.ObsidianWaterWalkingBoots)
                .Register();
        }

        // Changing vanilla accessories effects
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            // Ankh shield / charm
            if (item.type == ItemID.AnkhCharm || item.type == ItemID.AnkhShield)
            {
                // Magic mitten effects
                player.buffImmune[46] = true;
                player.buffImmune[47] = true;
                player.pStone = true;

                // Reflective blindfold effects
                player.buffImmune[22] = true;
                player.buffImmune[156] = true;
            }

            // Ankh shield special buffs
            if (item.type == ItemID.AnkhShield)
            {
                // On fire
                player.buffImmune[24] = true;
                // Cursed infernoand wa
                player.buffImmune[39] = true;
                // Ichor
                player.buffImmune[69] = true;
                // Acid venom
                player.buffImmune[70] = true;
            }


            // Amphibian boots
            if (item.type == ItemID.AmphibianBoots)
            {
                player.waterWalk2 = true;
            }
        }

        // Changing vanilla items tooltips
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Hooks
            if (AccessoriesPlus.IsItemHook(item.type))
            {
                if (item.type != ItemID.WebSlinger)
                {
                    string[] stats = AccessoriesPlus.GetHookStats(item.type);

                    tooltips.Add(new TooltipLine(Mod, "Reach", stats[0] + " tiles reach"));
                    tooltips.Add(new TooltipLine(Mod, "Velocity", stats[1] + " velocity"));
                    tooltips.Add(new TooltipLine(Mod, "HooksNum", stats[2] + (int.Parse(stats[2]) == 1 ? " hook" : " hooks")));
                    tooltips.Add(new TooltipLine(Mod, "LatchingMode", stats[3] + " hook latching"));
                }
                else
                {
                    tooltips.Add(new TooltipLine(Mod, "ModdedDescription", "Allows you to shoot webs like Spiderman"));
                }
            }
            // Wings
            if (AccessoriesPlus.IsItemWings(item.type))
            {
                string[] stats = AccessoriesPlus.GetWingStats(item.type);

                tooltips.Add(new TooltipLine(Mod, "FlightTime", stats[0] + " seconds flight time"));
                tooltips.Add(new TooltipLine(Mod, "FlightHeight", stats[1] + " tiles flight height"));
                tooltips.Add(new TooltipLine(Mod, "MaxHorizontalSpeed", stats[2] + " mph maximum horizontal speed"));
            }
            
            // Ankh shield/charm
            if (item.type == ItemID.AnkhCharm || item.type == ItemID.AnkhShield)
            {
                tooltips.Add(new TooltipLine(Mod, "HealingBuffLine", "Reduces the cooldown of healing potions by 25%"));
            }
        }
    }
}