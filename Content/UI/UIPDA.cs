using AccessoriesPlus.Content.ImprovedAccessories;
using Terraria.UI;
using Terraria.UI.Chat;
using TerraUtil.UI;

namespace AccessoriesPlus.Content.UI;
internal class UIPDA : Interface
{
    public static UIPDA Instance => ModContent.GetInstance<UIPDA>();

    public bool DrawLifeformAnalyzerArrows = false;
    public bool DrawMetalDetectorArrows = false;

    private const float MaxArrowDistanceFromPlayer = 300f;

    /*
    // This isn't safe, but shouldn't matter so long as beehives and other items aren't detected by the metal detector
    private static MethodInfo ItemDropQueryMethod = typeof(WorldGen).GetMethod("KillTile_GetItemDrops", BindingFlags.NonPublic | BindingFlags.Static);

    private static int GetPrimaryTileDrop(int x, int y)
    {
        object[] parameters = new object[] { x, y, Main.tile[x, y], null, null, null, null, true };
        ItemDropQueryMethod.Invoke(null, parameters);
        return (int)parameters[3];
    }
    */

    public override int GetLayerInsertIndex(List<GameInterfaceLayer> layers)
    {
        return layers.FindIndex(l => l.Name == "Vanilla: Entity Health Bars") + 1;
    }

    // TODO: make UI elememnts for the arrows and rework them
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        // Lifeform analyzer
        if (DrawLifeformAnalyzerArrows)
        {
            foreach (var npc in AccessoryInfoDisplay.LifeformAnalyzerNPCs)
            {
                if (npc.active)
                    DrawNPCArrow(spriteBatch, npc);
            }
        }

        // Metal detector
        // TODO: better system for blobbing framed tiles together, and also making frameImportant tiles have their position centred
        if (DrawMetalDetectorArrows)
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
                    if (SceneMetrics.IsValidForOreFinder(tile))
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
        DrawLifeformAnalyzerArrows = false;
        DrawMetalDetectorArrows = false;
    }

    // Gets the preview texture for a tile
    private static Asset<Texture2D> GetTileTexture(int x, int y, Tile tile)
    {
        return TextureAssets.NpcHead[0];
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
        var tex = GetTileTexture(x, y, target);
        sb.Draw(tex.Value, position, null, Color.White, 0f, tex.Size() / 2, 1f, SpriteEffects.None, 0f);

        // Drawing the name and distance
        string name = AccessoryInfoDisplay.GetBestOreTileName(target.TileType, new Point(x, y));
        DrawText(sb, position, name, new Vector2(x * 16f, y * 16f));
    }

    // Calculating the position and rotation for an arrow
    // TODO: make this work with multiple UI scales
    private static (Vector2, float) GetArrowPositionAndRotation(Vector2 target)
    {
        // Getting position and rotation
        var position = target - Main.LocalPlayer.Center;
        float rotation = position.ToRotation();

        // Changing distance from player
        if (position.Length() >= MaxArrowDistanceFromPlayer)
        {
            position.Normalize();
            position *= MaxArrowDistanceFromPlayer;
        }

        // Moving to the center of the screen
        position += Main.LocalPlayer.Center - Util.ScreenPos;

        return (position, rotation);
    }

    // Draws the arrow and 
    // TODO: finish this
    private static void DrawArrow(SpriteBatch sb, Vector2 position, float rotation)
    {
        Utils.DrawInvBG(sb, Utils.CenteredRectangle(position, Vector2.One * 50f));

        var font = FontAssets.MouseText.Value;
        string text = "->";
        float scale = 3f;
        ChatManager.DrawColorCodedStringWithShadow(sb, font, text, Vector2.Lerp(Main.LocalPlayer.Center - Util.ScreenPos, position, 0.5f), Color.White, rotation, ChatManager.GetStringSize(font, text, scale * Vector2.One) / 2f, scale * Vector2.One);
    }

    // Draws the text for the name and distance
    // TODO: make this a tooltip
    private static void DrawText(SpriteBatch sb, Vector2 position, string text, Vector2 targetPos)
    {
        var font = FontAssets.MouseText.Value;

        // Name
        ChatManager.DrawColorCodedStringWithShadow(sb, font, text, position + new Vector2(0f, 30f), Color.White, 0f, ChatManager.GetStringSize(font, text, Vector2.One) / 2f, Vector2.One);

        // Distance
        int distance = (int)Util.Round(targetPos.Distance(Main.LocalPlayer.Center) / 16f);
        string distanceText = Util.GetTextValue("InfoDisplays.TilesDistance", distance);
        ChatManager.DrawColorCodedStringWithShadow(sb, font, distanceText, position + new Vector2(0f, 50f), Color.White, 0f, ChatManager.GetStringSize(font, distanceText, Vector2.One) / 2f, Vector2.One);
    }
}