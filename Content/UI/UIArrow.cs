using Terraria.GameContent;

namespace AccessoriesPlus.Content.UI;
internal class UIArrow : UIElement
{
    public Vector2 WorldTarget;

    public UIArrow(Vector2 worldTarget)
    {
        WorldTarget = worldTarget;
    }

    public override void OnInitialize()
    {
        Width.Set(100, 0f);
        Height.Set(100, 0f);
        Left.Set(0, 0f);
        Top.Set(0, 0f);

        // TODO - add other UI elements
        Main.instance.LoadItem(5000);
        Append(new UIImage(TextureAssets.Item[5000]));
    }

    public override void Update(GameTime gameTime)
    {
        // TODO - position on the screen
        var position = (WorldTarget - Main.LocalPlayer.Center) / new Vector2(Main.maxTilesX * 16f, Main.maxTilesY * 16f);
        position.Normalize();
        position *= 0.5f;
        position += Vector2.One * 0.5f;
        position *= 2f;
        HAlign = position.X;
        VAlign = position.Y;
        Main.NewText(position);

        base.Update(gameTime);
    }
}
