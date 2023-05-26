using Terraria.GameContent;

namespace AccessoriesPlus.Content.UI;
internal class UIArrow : UIElement
{
    public Vector2 WorldTarget;
    private UIImage icon;

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
        icon = new UIImage(TextureAssets.Item[5000]);
        Append(icon);
    }

    public override void Update(GameTime gameTime)
    {
        // TODO - position on the screen
        var position = WorldTarget - Main.LocalPlayer.Center;
        float rotation = position.ToRotation();

        position.Normalize();
        position *= 5f;

        position += Main.LocalPlayer.Center - Main.screenPosition;
        HAlign = position.X / Main.screenWidth;
        VAlign = position.Y / Main.screenHeight;
        //icon.Rotation = rotation;

        Main.NewText(HAlign.ToString() + " " + VAlign.ToString());
        Main.NewText(rotation);

        base.Update(gameTime);
    }
}
