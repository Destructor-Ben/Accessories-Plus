using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;
internal class FartBalloons : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.FartInABalloon, ItemID.BalloonHorseshoeFart };
}
