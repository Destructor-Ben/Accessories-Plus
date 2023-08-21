using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;
internal class Gold : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.GoldBar, ItemID.PlatinumBar };
}
