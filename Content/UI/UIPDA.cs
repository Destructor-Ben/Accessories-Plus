namespace AccessoriesPlus.Content.UI;
internal class UIPDA : UIState
{
    // TODO - makes these activated either while equipped, while hovering over the text, or with a hotkey
    public bool DrawLifeformAnalyzer = false;
    public bool DrawMetalDetector = false;

    public override void Update(GameTime gameTime)
    {
        // Removing existing arrows
        // TODO - event based system, might be more performant than updating every frame (have too see)
        RemoveAllChildren();
        Append(new UIArrow(new Vector2(Main.spawnTileX, Main.spawnTileY).ToWorldCoordinates()));

        // Lifeform analyzer
        if (DrawLifeformAnalyzer)
        {
            foreach (var npc in Main.npc)
            {
                if (npc.active && npc.rarity > 0 && npc.Distance(Main.LocalPlayer.Center) <= 4000f)
                    Append(new UIArrow(npc.Center));
            }
        }

        // Metal detector
        if (DrawMetalDetector)
        {
        }

        // Resetting the display toggles
        DrawLifeformAnalyzer = false;
        DrawMetalDetector = false;

        base.Update(gameTime);
    }
}
