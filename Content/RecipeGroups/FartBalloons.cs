using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;

public class FartBalloons : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.FartInABalloon, ItemID.BalloonHorseshoeFart };
}
