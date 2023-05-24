namespace AccessoriesPlus.Content.UI;
internal class UISystem : ModSystem
{
    public static UISystem Instance => ModContent.GetInstance<UISystem>();

    public UserInterface PDAInterface;
    public UIPDA PDAState;

    private GameTime uiGameTime;

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
        // TODO - which layer
        int index = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
        if (index == -1)
            return;

        layers.Insert(index, new LegacyGameInterfaceLayer(
            "AccessoriesPlus: PDAInterface",
            delegate
            {
                if (uiGameTime != null && PDAInterface?.CurrentState != null)
                    PDAInterface.Draw(Main.spriteBatch, uiGameTime);

                return true;
            },
            InterfaceScaleType.UI
        ));
    }

    public override void UpdateUI(GameTime gameTime)
    {
        uiGameTime = gameTime;
        PDAInterface.Update(gameTime);
    }
}
