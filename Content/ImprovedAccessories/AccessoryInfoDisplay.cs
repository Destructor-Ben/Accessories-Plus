using AccessoriesPlus.Content.UI;
using Terraria.Map;

namespace AccessoriesPlus.Content.ImprovedAccessories;
internal class AccessoryInfoDisplay : GlobalInfoDisplay
{
    public static List<NPC> LifeformAnalyzerNPCs;
    public static NPC BestNPC;

    private const short GemPriority = 235;

    public override void Load()
    {
        LifeformAnalyzerNPCs = new();
    }

    public override void Unload()
    {
        LifeformAnalyzerNPCs = null;

        // Resetting tileOreFinderPriority values
        Main.tileOreFinderPriority[TileID.Hellstone] = 0;

        Main.tileOreFinderPriority[TileID.ExposedGems] = 0;
        Main.tileOreFinderPriority[TileID.Amethyst] = 0;
        Main.tileOreFinderPriority[TileID.Topaz] = 0;
        Main.tileOreFinderPriority[TileID.Sapphire] = 0;
        Main.tileOreFinderPriority[TileID.Emerald] = 0;
        Main.tileOreFinderPriority[TileID.Ruby] = 0;
        Main.tileOreFinderPriority[TileID.Diamond] = 0;

        Main.tileOreFinderPriority[TileID.TreeAmethyst] = 0;
        Main.tileOreFinderPriority[TileID.TreeTopaz] = 0;
        Main.tileOreFinderPriority[TileID.TreeSapphire] = 0;
        Main.tileOreFinderPriority[TileID.TreeEmerald] = 0;
        Main.tileOreFinderPriority[TileID.TreeRuby] = 0;
        Main.tileOreFinderPriority[TileID.TreeDiamond] = 0;
        Main.tileOreFinderPriority[TileID.TreeAmber] = 0;
    }

    public override void SetStaticDefaults()
    {
        // Metal detector priorities
        if (Config.Instance.ImprovedPDA.TrackHellstone)
        {
            Main.tileOreFinderPriority[TileID.Hellstone] = 450;
        }

        if (Config.Instance.ImprovedPDA.TrackGems)
        {
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
        if (currentDisplay == InfoDisplay.LifeformAnalyzer)
        {
            // Searching for NPCs
            Main.LocalPlayer.accCritterGuideCounter = 15;
            if (Main.GameUpdateCount % 15 == 0)
            {
                BestNPC = null;
                LifeformAnalyzerNPCs.Clear();

                // Finding all rare npcs
                foreach (var npc in Main.npc)
                {
                    bool npcInWhitelist = Config.Instance.ImprovedPDA.UseNPCWhitelist && Config.Instance.ImprovedPDA.NPCWhitelist.Where(n => n.Type == npc.type).Any();
                    bool npcInBlacklist = Config.Instance.ImprovedPDA.UseNPCBlacklist && Config.Instance.ImprovedPDA.NPCBlacklist.Where(n => n.Type == npc.type).Any();
                    if (npc.active && (npc.rarity > 0 || npcInWhitelist) && !npcInBlacklist && npc.Distance(Main.LocalPlayer.Center) <= 1300f)
                    {
                        LifeformAnalyzerNPCs.Add(npc);
                    }
                }

                // Finding rarest npc
                foreach (var npc in LifeformAnalyzerNPCs)
                {
                    if (npc.rarity > (BestNPC?.rarity ?? -1))
                        BestNPC = npc;
                }

                Main.LocalPlayer.accCritterGuideNumber = (byte)(BestNPC?.whoAmI ?? -1);
            }

            // Drawing the arrows
            UIPDA.Instance.DrawLifeformAnalyzerArrows = Config.Instance.ImprovedPDA.LifeformAnalyzerArrows;

            // Changing display value
            if (Config.Instance.ImprovedPDA.LifeformAnalyzerDistanceInfo && (BestNPC?.active ?? false))
            {
                var npc = Main.npc[Main.LocalPlayer.accCritterGuideNumber];
                displayValue = Util.GetTextValue("InfoDisplays.FoundRareCreature", npc.GivenOrTypeName, Util.Round(npc.Distance(Main.LocalPlayer.Center) / 16f));
            }
        }

        // Metal detector
        if (currentDisplay == InfoDisplay.MetalDetector && Main.SceneMetrics.bestOre > 0)
        {
            // Drawing the arrows
            UIPDA.Instance.DrawMetalDetectorArrows = Config.Instance.ImprovedPDA.MetalDetectorArrows;

            // Changing display value
            if (Config.Instance.ImprovedPDA.MetalDetectorDistanceInfo)
            {
                string tileName = GetBestOreTileName(Main.SceneMetrics.bestOre, Main.SceneMetrics.ClosestOrePosition);
                int distance = (int)Util.Round(Main.SceneMetrics.ClosestOrePosition.Value.ToWorldCoordinates().Distance(Main.LocalPlayer.Center) / 16f);
                displayValue = Util.GetTextValue("InfoDisplays.FoundTreasure", tileName, distance);
            }
        }
    }

    public override void ModifyDisplayColor(InfoDisplay currentDisplay, ref Color displayColor)
    {
        // Whitelisted NPCs don't have the colour change
        if (currentDisplay == InfoDisplay.LifeformAnalyzer && (BestNPC?.active ?? false) && !NPCID.Sets.GoldCrittersCollection.Contains(BestNPC.type))
        {
            displayColor = Main.MouseTextColorReal;
            // TODO: whitelisted golden critter's aren't handled because I can'y modify the background color of the text
        }
    }

    public static string GetBestOreTileName(int type, Point? position)
    {
        int baseOption = 0;
        int num10 = type;
        if (position.HasValue)
        {
            var value = position.Value;
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
