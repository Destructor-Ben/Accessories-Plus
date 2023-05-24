namespace AccessoriesPlus.Content.UI;
internal class UIPDA : UIState
{
    // TODO - how should this be activated
    public bool DrawLifeformAnalyzer = false;
    public bool DrawMetalDetector = false;

    public override void OnInitialize()
    {
        Append(new UIArrow(new Vector2(Main.spawnTileX, Main.spawnTileY).ToWorldCoordinates()));
    }

    public override void Update(GameTime gameTime)
    {
        //RemoveAllChildren();

        if (false)// (DrawLifeformAnalyzer)
        {
            foreach (var npc in Main.npc)
            {
                if (npc.active && npc.rarity > 0 && npc.Distance(Main.LocalPlayer.Center) <= 4000f)
                    Append(new UIArrow(new Vector2(Main.spawnTileX, Main.spawnTileY).ToWorldCoordinates()));
            }
        }

        // Resetting the display toggles
        DrawLifeformAnalyzer = false;
        DrawMetalDetector = false;

        base.Update(gameTime);
    }
}
