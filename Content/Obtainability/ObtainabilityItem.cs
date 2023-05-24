using Terraria.GameContent.ItemDropRules;

namespace AccessoriesPlus.Content.Obtainability;
internal class ObtainabilityItem : GlobalItem
{
    public override void SetStaticDefaults()
    {
        // Shimmer transmutations
        if (Config.Instance.ObtainabilityShimmer)
        {
            // Hand of creation
            ItemID.Sets.ShimmerTransformToItem[ItemID.BrickLayer] = ItemID.ExtendoGrip;
            ItemID.Sets.ShimmerTransformToItem[ItemID.ExtendoGrip] = ItemID.PaintSprayer;
            ItemID.Sets.ShimmerTransformToItem[ItemID.PaintSprayer] = ItemID.PortableCementMixer;
            ItemID.Sets.ShimmerTransformToItem[ItemID.PortableCementMixer] = ItemID.BrickLayer;

            // Travelling merchant accessories
            ItemID.Sets.ShimmerTransformToItem[ItemID.Stopwatch] = ItemID.LifeformAnalyzer;
            ItemID.Sets.ShimmerTransformToItem[ItemID.LifeformAnalyzer] = ItemID.DPSMeter;
            ItemID.Sets.ShimmerTransformToItem[ItemID.DPSMeter] = ItemID.Stopwatch;

            // Shiny red balloon to balloon pufferfish
            ItemID.Sets.ShimmerTransformToItem[ItemID.ShinyRedBalloon] = ItemID.BalloonPufferfish;

            // Corruption and crimson counterparts
            ItemID.Sets.ShimmerTransformToItem[ItemID.PutridScent] = ItemID.FleshKnuckles;
            ItemID.Sets.ShimmerTransformToItem[ItemID.FleshKnuckles] = ItemID.PutridScent;

            ItemID.Sets.ShimmerTransformToItem[ItemID.BandofStarpower] = ItemID.PanicNecklace;
            ItemID.Sets.ShimmerTransformToItem[ItemID.PanicNecklace] = ItemID.BandofStarpower;

            ItemID.Sets.ShimmerTransformToItem[ItemID.WormScarf] = ItemID.BrainOfConfusion;
            ItemID.Sets.ShimmerTransformToItem[ItemID.BrainOfConfusion] = ItemID.WormScarf;
        }
    }

    // Removing certain drops from presents
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        if (Config.Instance.ObtainabilityPresents && item.type == ItemID.Present)
        {
            foreach (var rule in itemLoot.Get())
            {
                if (rule is SequentialRulesNotScalingWithLuckRule drop)
                {
                    var modifiedRules = drop.rules.ToList();

                    // Toolbox
                    modifiedRules.Remove(modifiedRules.Where(r => r is CommonDrop d && d.itemId == ItemID.Toolbox).First());
                    // Hand warmer
                    modifiedRules.Remove(modifiedRules.Where(r => r is CommonDrop d && d.itemId == ItemID.HandWarmer).First());

                    drop.rules = modifiedRules.ToArray();
                }
            }
        }
    }

    // Adding recipes
    public override void AddRecipes()
    {
        // Hand warmer recipe
        if (Config.Instance.ObtainabilityPresents)
        {
            Recipe.Create(ItemID.HandWarmer)
                .AddTile(TileID.TinkerersWorkbench)
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.FlinxFur, 5)
                .Register();
        }

        // TODO - more accessory recipes
        if (Config.Instance.ObtainabiltyRecipes)
        {

        }
    }
}
