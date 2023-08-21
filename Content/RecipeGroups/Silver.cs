using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;
internal class Silver : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.SilverBar, ItemID.TungstenBar };
}
