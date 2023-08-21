using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;
internal class SharkronBalloons : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.SharkronBalloon, ItemID.BalloonHorseshoeSharkron };
}
