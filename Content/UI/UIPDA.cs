using AccessoriesPlus.Content.ImprovedAccessories;
using System.Reflection;
using Terraria.UI;
using Terraria.UI.Chat;

namespace AccessoriesPlus.Content.UI;
internal class UIPDA : UIState
{
    public bool DrawLifeformAnalyzer = false;
    public bool DrawMetalDetector = false;

    // This isn't safe, but shouldn't matter so long as beehives aren't detected by the metal detector
    private static MethodInfo ItemDropQueryMethod = typeof(WorldGen).GetMethod("KillTile_GetItemDrops", BindingFlags.NonPublic | BindingFlags.Static);

    private static int GetPrimaryTileDrop(int x, int y)
    {
        object[] parameters = new object[] { x, y, Main.tile[x, y], null, null, null, null, false };
        ItemDropQueryMethod.Invoke(null, parameters);
        return (int)parameters[3];
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        // Lifeform analyzer
        if (DrawLifeformAnalyzer)
        {
            foreach (var npc in Main.npc)
            {
                if (npc.active && npc.rarity > 0 && npc.Distance(Main.LocalPlayer.Center) <= 1300 && npc.Distance(Main.LocalPlayer.Center) >= 150f)
                    DrawNPCArrow(spriteBatch, npc);
            }
        }

        // Metal detector
        if (DrawMetalDetector)
        {
            var trackedTiles = new List<(Tile, Point)>();

            // Looping over the tiles
            for (int x = Main.LocalPlayer.Center.ToTileCoordinates().X - Main.buffScanAreaWidth / 2; x < Main.LocalPlayer.Center.ToTileCoordinates().X + Main.buffScanAreaWidth / 2; x++)
            {
                for (int y = Main.LocalPlayer.Center.ToTileCoordinates().Y - Main.buffScanAreaHeight / 2; y < Main.LocalPlayer.Center.ToTileCoordinates().Y + Main.buffScanAreaHeight / 2; y++)
                {
                    // Bounds check
                    if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
                        continue;

                    var tile = Main.tile[x, y];
                    if (SceneMetrics.IsValidForOreFinder(tile) && new Point(x, y).ToWorldCoordinates().Distance(Main.LocalPlayer.Center) >= 150f)
                    {
                        // Adding the tile if there isn't one already
                        if (!trackedTiles.Where(t => t.Item1.TileType == tile.TileType).Any())
                        {
                            trackedTiles.Add((tile, new Point(x, y)));
                            continue;
                        }

                        // Removing the exitsing tile if this one is closer
                        var tilesToRemove = new List<(Tile, Point)>();
                        var tilesToAdd = new List<(Tile, Point)>();

                        foreach (var trackedTile in trackedTiles)
                        {
                            if (trackedTile.Item1.TileType == tile.TileType && trackedTile.Item2.ToWorldCoordinates().Distance(Main.LocalPlayer.Center) >= new Point(x, y).ToWorldCoordinates().Distance(Main.LocalPlayer.Center))
                            {
                                tilesToRemove.Add(trackedTile);
                                tilesToAdd.Add((tile, new Point(x, y)));
                            }
                        }

                        // Actually doing the removing and adding
                        foreach (var tileToAdd in tilesToAdd)
                            trackedTiles.Add(tileToAdd);

                        foreach (var tileToRemove in tilesToRemove)
                            trackedTiles.Remove(tileToRemove);
                    }
                }
            }

            // Drawing
            foreach (var tile in trackedTiles)
                DrawTileArrow(spriteBatch, tile.Item1, tile.Item2.X, tile.Item2.Y);
        }

        // Resetting variables
        DrawLifeformAnalyzer = false;
        DrawMetalDetector = false;
    }

    // Calculating the position and rotation for an arrow
    private static (Vector2, float) GetArrowPositionAndRotation(Vector2 target)
    {
        // Getting position and rotation
        var position = target - Main.LocalPlayer.Center;
        float rotation = position.ToRotation();

        // Changing distance from player
        position.Normalize();
        position *= 100f;

        // Moving to the center of the screen
        position += Main.LocalPlayer.Center - Main.screenPosition;

        return (position, rotation);
    }

    // Draws the arrow and background
    private static void DrawArrow(SpriteBatch sb, Vector2 postition, float rotation)
    {
        // TODO
    }

    // Draws the text for the name and distance
    private static void DrawText(SpriteBatch sb, Vector2 position, string text, Vector2 targetPos)
    {
        var font = FontAssets.MouseText.Value;

        // Name
        ChatManager.DrawColorCodedStringWithShadow(sb, font, text, position + new Vector2(0f, 30f), Color.White, 0f, ChatManager.GetStringSize(font, text, Vector2.One) / 2f, Vector2.One);

        // Distance
        int distance = (int)Util.RoundToNearest(targetPos.Distance(Main.LocalPlayer.Center) / 16f);
        string distanceText = Util.GetTextValue("InfoDisplays.TilesDistance", distance);
        ChatManager.DrawColorCodedStringWithShadow(sb, font, distanceText, position + new Vector2(0f, 50f), Color.White, 0f, ChatManager.GetStringSize(font, distanceText, Vector2.One) / 2f, Vector2.One);
    }

    // Draws an arrow to the npc
    private static void DrawNPCArrow(SpriteBatch sb, NPC target)
    {
        // Getting position
        (var position, float rotation) = GetArrowPositionAndRotation(target.Center);

        // Drawing the background and arrow
        DrawArrow(sb, position, rotation);

        // Drawing an NPC
        var npc = new NPC();
        npc.SetDefaults(target.type);
        npc.IsABestiaryIconDummy = true;
        npc.Center = position;
        Main.instance.DrawNPCDirect(sb, npc, false, Vector2.Zero);

        // Drawing the name and distance
        DrawText(sb, position, npc.GivenOrTypeName, npc.Center);
    }

    // Draws an arrow to the tile
    private static void DrawTileArrow(SpriteBatch sb, Tile target, int x, int y)
    {
        // Getting position
        (var position, float rotation) = GetArrowPositionAndRotation(new Point(x, y).ToWorldCoordinates());

        // Drawing the background and arrow
        DrawArrow(sb, position, rotation);

        // Drawing the tile
        int itemID = GetPrimaryTileDrop(x, y);
        if (itemID != -1)
        {
            var tex = TextureAssets.Item[itemID];
            sb.Draw(tex.Value, position, null, Color.White, 0f, tex.Size() / 2, 1f, SpriteEffects.None, 0f);
        }

        // Drawing the name and distance
        string name = AccessoryInfoDisplay.GetBestOreTileName(target.TileType, new Point(x, y));
        DrawText(sb, position, name, new Vector2(x * 16f, y * 16f));
    }
}