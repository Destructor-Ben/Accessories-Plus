using Terraria.UI;

namespace AccessoriesPlus.Content.UI;
internal class UISystem : ModSystem
{
    public static UISystem Instance => ModContent.GetInstance<UISystem>();

    public UserInterface PDAInterface;
    public UIPDA PDAState;

    public override void Load()
    {
        if (!Main.dedServ)
        {
            PDAState = new UIPDA();
            PDAState.Activate();
            PDAInterface = new UserInterface();
            PDAInterface.SetState(PDAState);
        }
    }

    public override void Unload()
    {
        PDAInterface = null;
        PDAState = null;
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int index = layers.FindIndex(layer => layer.Name == "Vanilla: Entity Health Bars");
        if (index == -1)
            return;

        layers.Insert(index, new LegacyGameInterfaceLayer(
            "AccessoriesPlus: PDAInterface",
            delegate
            {
                PDAInterface.Draw(Main.spriteBatch, null);
                return true;
            },
            InterfaceScaleType.Game
        ));
    }

    public override void UpdateUI(GameTime gameTime)
    {
        PDAInterface.Update(gameTime);
    }
}
