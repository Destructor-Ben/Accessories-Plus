using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;

public class SharkronBalloons : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.SharkronBalloon, ItemID.BalloonHorseshoeSharkron };
}
