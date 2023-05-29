using AccessoriesPlus.Content.ImprovedAccessories;
using Terraria.UI.Chat;

namespace AccessoriesPlus.Content.UI;
internal class UIPDA : UIState
{
    // TODO - makes these activated either while equipped, while hovering over the text, or with a hotkey
    public bool DrawLifeformAnalyzer = false;
    public bool DrawMetalDetector = false;

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
            for (int x = Main.LocalPlayer.Center.ToTileCoordinates().X - Main.buffScanAreaWidth / 2; x < Main.LocalPlayer.Center.ToTileCoordinates().X + Main.buffScanAreaWidth / 2; x++)
            {
                for (int y = Main.LocalPlayer.Center.ToTileCoordinates().Y - Main.buffScanAreaHeight / 2; y < Main.LocalPlayer.Center.ToTileCoordinates().Y + Main.buffScanAreaHeight / 2; y++)
                {
                    // TODO - stop clusters of the same block from looking weird
                    var tile = Main.tile[x, y];
                    if (SceneMetrics.IsValidForOreFinder(tile) && new Point(x, y).ToWorldCoordinates().Distance(Main.LocalPlayer.Center) >= 150f)
                        DrawTileArrow(spriteBatch, tile, x, y);
                }
            }
        }

        // Resetting the display toggles
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

    // TODO - make this actually look good
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
        var font = FontAssets.MouseText.Value;
        string name = npc.GivenOrTypeName;
        ChatManager.DrawColorCodedStringWithShadow(sb, font, name, position + new Vector2(0f, 30f), Color.White, 0f, ChatManager.GetStringSize(font, name, Vector2.One) / 2f, Vector2.One);

        string distance = Util.RoundToNearest(target.Distance(Main.LocalPlayer.Center) / 16f) + " tiles";
        ChatManager.DrawColorCodedStringWithShadow(sb, font, distance, position + new Vector2(0f, 50f), Color.White, 0f, ChatManager.GetStringSize(font, distance, Vector2.One) / 2f, Vector2.One);
    }

    // Draws an arrow to the tile
    private static void DrawTileArrow(SpriteBatch sb, Tile target, int x, int y)
    {
        // Getting position
        (var position, float rotation) = GetArrowPositionAndRotation(new Point(x, y).ToWorldCoordinates());

        // Drawing the background and arrow
        DrawArrow(sb, position, rotation);

        // Drawing the tile
        // TODO

        // Drawing the name and distance
        var font = FontAssets.MouseText.Value;
        string name = AccessoryInfoDisplay.GetBestOreTileName(target.TileType, new Point(x, y));
        ChatManager.DrawColorCodedStringWithShadow(sb, font, name, position + new Vector2(0f, 30f), Color.White, 0f, ChatManager.GetStringSize(font, name, Vector2.One) / 2f, Vector2.One);

        string distance = Util.RoundToNearest(new Vector2(x * 16f, y * 16f).Distance(Main.LocalPlayer.Center) / 16f) + " tiles";
        ChatManager.DrawColorCodedStringWithShadow(sb, font, distance, position + new Vector2(0f, 50f), Color.White, 0f, ChatManager.GetStringSize(font, distance, Vector2.One) / 2f, Vector2.One);
    }
}
