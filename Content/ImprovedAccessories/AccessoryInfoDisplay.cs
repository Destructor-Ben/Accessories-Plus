using Terraria.Map;

namespace AccessoriesPlus.Content.ImprovedAccessories;
internal class AccessoryInfoDisplay : GlobalInfoDisplay
{
    public static List<NPC> LifeformAnalyzerNPCs;
    public static NPC BestNPC;

    private static Dictionary<int, (short, bool)> ModifiedSpelunk;
    // TODO: increasing priorities for gems
    private const short GemPriority = 235;
    private const short HellstonePriority = 450;

    public override void Load()
    {
        LifeformAnalyzerNPCs = new();
        ModifiedSpelunk = new();
    }

    public override void Unload()
    {
        // Resetting tileOreFinderPriority values
        if (ModifiedSpelunk is not null)
            UnloadSpelunk();

        LifeformAnalyzerNPCs = null;
        ModifiedSpelunk = null;
    }

    // Automatically unloads the spelunkeable tiles that I have modified
    private static void UnloadSpelunk()
    {
        foreach (var tile in ModifiedSpelunk)
        {
            int type = tile.Key;
            short priority = tile.Value.Item1;
            bool spelunkable = tile.Value.Item2;

            Main.tileOreFinderPriority[type] = priority;
            Main.tileSpelunker[type] = spelunkable;
        }
    }

    public override void SetStaticDefaults()
    {
        // Metal detector priorities
        if (PDAConfig.Instance.TrackHellstone)
            ModifyTileSpelunk(TileID.Hellstone, HellstonePriority);

        if (PDAConfig.Instance.TrackGems)
        {
            ModifyTileSpelunk(TileID.ExposedGems, GemPriority);

            ModifyTileSpelunk(TileID.Amethyst, GemPriority);
            ModifyTileSpelunk(TileID.Topaz, GemPriority);
            ModifyTileSpelunk(TileID.Sapphire, GemPriority);
            ModifyTileSpelunk(TileID.Emerald, GemPriority);
            ModifyTileSpelunk(TileID.Ruby, GemPriority);
            ModifyTileSpelunk(TileID.Diamond, GemPriority);
            ModifyTileSpelunk(TileID.AmberStoneBlock, GemPriority);

            ModifyTileSpelunk(TileID.TreeAmethyst, GemPriority);
            ModifyTileSpelunk(TileID.TreeTopaz, GemPriority);
            ModifyTileSpelunk(TileID.TreeSapphire, GemPriority);
            ModifyTileSpelunk(TileID.TreeEmerald, GemPriority);
            ModifyTileSpelunk(TileID.TreeRuby, GemPriority);
            ModifyTileSpelunk(TileID.TreeDiamond, GemPriority);
            ModifyTileSpelunk(TileID.TreeAmber, GemPriority);
        }
    }

    private static void ModifyTileSpelunk(int type, short priority)
    {
        // Storing original values
        short metalDetectorValue = Main.tileOreFinderPriority[type];
        bool isSpelunkable = Main.tileSpelunker[type];
        ModifiedSpelunk.Add(type, (metalDetectorValue, isSpelunkable));

        // Modifying values
        Main.tileOreFinderPriority[type] = priority;
        Main.tileSpelunker[type] = true;
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
                    bool npcInWhitelist = PDAConfig.Instance.UseNPCWhitelist && PDAConfig.Instance.NPCWhitelist.Where(n => n.Type == npc.type).Any();
                    bool npcInBlacklist = PDAConfig.Instance.UseNPCBlacklist && PDAConfig.Instance.NPCBlacklist.Where(n => n.Type == npc.type).Any();
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

            // Changing display value
            if (PDAConfig.Instance.LifeformAnalyzerDistanceInfo && (BestNPC?.active ?? false))
            {
                var npc = Main.npc[Main.LocalPlayer.accCritterGuideNumber];
                displayValue = Util.GetTextValue("InfoDisplays.FoundRareCreature", npc.GivenOrTypeName, (int)Util.Round(npc.Distance(Main.LocalPlayer.Center) / 16f));
            }
        }

        // Metal detector
        if (currentDisplay == InfoDisplay.MetalDetector && Main.SceneMetrics.bestOre > 0)
        {
            // Changing display value
            if (PDAConfig.Instance.MetalDetectorDistanceInfo)
            {
                string tileName = GetBestOreTileName(Main.SceneMetrics.bestOre, Main.SceneMetrics.ClosestOrePosition);
                int distance = (int)Util.Round(Main.SceneMetrics.ClosestOrePosition.Value.ToWorldCoordinates().Distance(Main.LocalPlayer.Center) / 16f);
                displayValue = Util.GetTextValue("InfoDisplays.FoundTreasure", tileName, distance);
            }
        }
    }

    public override void ModifyDisplayColor(InfoDisplay currentDisplay, ref Color displayColor)
    {
        // Whitelisted NPCs don't have the colour change so I need to do it myself
        if (currentDisplay == InfoDisplay.LifeformAnalyzer && (BestNPC?.active ?? false) && !NPCID.Sets.GoldCrittersCollection.Contains(BestNPC.type))
        {
            displayColor = Main.MouseTextColorReal;
            // TODO: whitelisted golden critter's aren't handled because I can't modify the background color of the text
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

        if (TileID.Sets.CountsAsGemTree[num10])
            name = Util.GetTextValue("InfoDisplays.GemTree");

        return name;
    }
}
