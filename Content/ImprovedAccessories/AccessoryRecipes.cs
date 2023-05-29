namespace AccessoriesPlus.Content.ImprovedAccessories;
internal class AccessoryRecipes : ModSystem
{
    public static RecipeGroup AnyFartBalloon;
    public static RecipeGroup AnyTsunamiBalloon;

    // Recipe groups
    public override void AddRecipeGroups()
    {
        AnyFartBalloon = Util.RegisterRecipeGroup("FartBalloons", ItemID.FartInABalloon, ItemID.BalloonHorseshoeFart);
        AnyTsunamiBalloon = Util.RegisterRecipeGroup("TsunamiBalloons", ItemID.SharkronBalloon, ItemID.BalloonHorseshoeSharkron);
    }

    // Adding recipes
    public override void AddRecipes()
    {
        // Hand of creation
        if (Config.Instance.ImprovedHandOfCreation)
        {
            Recipe.Create(ItemID.HandOfCreation)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.ArchitectGizmoPack)
                .AddIngredient(ItemID.AncientChisel)
                .AddIngredient(ItemID.TreasureMagnet)
                .AddIngredient(ItemID.PortableStool)
                .AddIngredient(ItemID.Toolbelt)
                .AddIngredient(ItemID.Toolbox)
                .Register();
        }

        // Terraspark boots
        if (Config.Instance.ImprovedTerrasparkBoots)
        {
            Recipe.Create(ItemID.TerrasparkBoots)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.FrostsparkBoots)
                .AddIngredient(ItemID.LavaWaders)
                .AddIngredient(ItemID.AmphibianBoots)
                .Register();
        }

        // Ankh shield
        if (Config.Instance.ImprovedAnkhShield)
        {
            Recipe.Create(ItemID.AnkhShield)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.AnkhCharm)
                .AddIngredient(ItemID.ObsidianShield)
                .AddIngredient(ItemID.FrozenShield)
                .AddIngredient(ItemID.HeroShield)
                .DisableDecraft()
                .Register();
            Recipe.Create(ItemID.AnkhShield)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.AnkhCharm)
                .AddIngredient(ItemID.ObsidianShield)
                .AddIngredient(ItemID.FrozenTurtleShell)
                .AddIngredient(ItemID.HeroShield)
                .Register();
            Recipe.Create(ItemID.AnkhShield)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.AnkhCharm)
                .AddIngredient(ItemID.ObsidianShield)
                .AddIngredient(ItemID.FrozenShield)
                .AddIngredient(ItemID.FleshKnuckles)
                .DisableDecraft()
                .Register();

            Recipe.Create(ItemID.AnkhCharm)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.ArmorBracing)
                .AddIngredient(ItemID.MedicatedBandage)
                .AddIngredient(ItemID.ThePlan)
                .AddIngredient(ItemID.CountercurseMantra)
                .AddIngredient(ItemID.ReflectiveShades)
                .AddIngredient(ItemID.HandWarmer)
                .Register();
        }

        // Bundle of horseshoe balloons
        if (Config.Instance.ImprovedHorseshoeBundle)
        {
            Recipe.Create(ItemID.HorseshoeBundle)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.BundleofBalloons)
                .AddIngredient(ItemID.LuckyHorseshoe)
                .Register();

            Recipe.Create(ItemID.BundleofBalloons)
                .AddTile(TileID.TinkerersWorkbench)
                .AddRecipeGroup(RecipeGroupID.CloudBalloons)
                .AddRecipeGroup(RecipeGroupID.BlizzardBalloons)
                .AddRecipeGroup(RecipeGroupID.SandstormBalloons)
                .AddRecipeGroup(AnyFartBalloon)
                .AddRecipeGroup(AnyTsunamiBalloon)
                .Register();
        }
    }

    // Removing recipes not from this mod
    public override void PostAddRecipes()
    {
        // Hand of creation
        if (Config.Instance.ImprovedHandOfCreation)
            Util.RemoveRecipesForItem(ItemID.HandOfCreation);

        // Terraspark boots
        if (Config.Instance.ImprovedTerrasparkBoots)
            Util.RemoveRecipesForItem(ItemID.TerrasparkBoots);

        // Ankh shield
        if (Config.Instance.ImprovedAnkhShield)
        {
            Util.RemoveRecipesForItem(ItemID.AnkhShield);
            Util.RemoveRecipesForItem(ItemID.AnkhCharm);
        }

        // Bundle of horseshoe balloons
        if (Config.Instance.ImprovedHorseshoeBundle)
        {
            Util.RemoveRecipesForItem(ItemID.BundleofBalloons);
            Util.RemoveRecipesForItem(ItemID.HorseshoeBundle);
        }
    }
}
