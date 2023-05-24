using AccessoriesPlus.Content.UI;
using Terraria.Map;

namespace AccessoriesPlus.Content.ImprovedAccessories;
internal class AccessoryInfoDisplay : GlobalInfoDisplay
{
    private short GemPriority => 235;

    public override void SetStaticDefaults()
    {
        // Metal detector priorities
        if (Config.Instance.ImprovedPDA)
        {
            Main.tileOreFinderPriority[TileID.Hellstone] = 450;

            Main.tileOreFinderPriority[TileID.ExposedGems] = GemPriority;
            Main.tileOreFinderPriority[TileID.Amethyst] = GemPriority;
            Main.tileOreFinderPriority[TileID.Topaz] = GemPriority;
            Main.tileOreFinderPriority[TileID.Sapphire] = GemPriority;
            Main.tileOreFinderPriority[TileID.Emerald] = GemPriority;
            Main.tileOreFinderPriority[TileID.Ruby] = GemPriority;
            Main.tileOreFinderPriority[TileID.Diamond] = GemPriority;

            Main.tileOreFinderPriority[TileID.TreeAmethyst] = GemPriority;
            Main.tileOreFinderPriority[TileID.TreeTopaz] = GemPriority;
            Main.tileOreFinderPriority[TileID.TreeSapphire] = GemPriority;
            Main.tileOreFinderPriority[TileID.TreeEmerald] = GemPriority;
            Main.tileOreFinderPriority[TileID.TreeRuby] = GemPriority;
            Main.tileOreFinderPriority[TileID.TreeDiamond] = GemPriority;
            Main.tileOreFinderPriority[TileID.TreeAmber] = GemPriority;
        }
    }

    public override void ModifyDisplayValue(InfoDisplay currentDisplay, ref string displayValue)
    {
        // Lifeform analyzer
        int rareNPC = Main.LocalPlayer.accCritterGuideNumber;
        if (Config.Instance.ImprovedPDA && currentDisplay == InfoDisplay.LifeformAnalyzer && rareNPC >= 0 && rareNPC < 200 && Main.npc[rareNPC].active && Main.npc[rareNPC].rarity > 0)
        {
            // Drawing the arrows
            UISystem.Instance.PDAState.DrawLifeformAnalyzer = true;

            // Changing display value
            var npc = Main.npc[Main.LocalPlayer.accCritterGuideNumber];
            displayValue = Util.GetTextValue("InfoDisplays.FoundRareCreature", npc.GivenOrTypeName, Util.RoundToNearest(npc.Distance(Main.LocalPlayer.Center) / 16f, 1f));
        }

        // Metal detector
        if (Config.Instance.ImprovedPDA && currentDisplay == InfoDisplay.MetalDetector && Main.SceneMetrics.bestOre > 0)
        {
            // Drawing the arrows
            UISystem.Instance.PDAState.DrawMetalDetector = true;

            // Changing display value
            string tileName = GetBestOreTileName();
            int distance = (int)(Main.SceneMetrics.ClosestOrePosition.Value.ToWorldCoordinates().Distance(Main.LocalPlayer.Center) / 16f);
            displayValue = Util.GetTextValue("InfoDisplays.FoundTreasure", tileName, distance);
        }
    }

    private static string GetBestOreTileName()
    {
        int baseOption = 0;
        int num10 = Main.SceneMetrics.bestOre;
        if (Main.SceneMetrics.ClosestOrePosition.HasValue)
        {
            var value = Main.SceneMetrics.ClosestOrePosition.Value;
            var tileSafely = Framing.GetTileSafely(value);
            if (tileSafely.HasTile)
            {
                MapHelper.GetTileBaseOption(value.X, value.Y, tileSafely.TileType, tileSafely, ref baseOption);
                num10 = tileSafely.TileType;
                if (TileID.Sets.BasicChest[num10] || TileID.Sets.BasicChestFake[num10])
                    baseOption = 0;
            }
        }

        string name = Lang.GetMapObjectName(MapHelper.TileToLookup(num10, baseOption));

        if (string.IsNullOrEmpty(name) && num10 == TileID.Hellstone)
            name = Lang.GetItemNameValue(ItemID.Hellstone);

        return name;
    }
}
