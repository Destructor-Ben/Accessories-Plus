using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Silver : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.SilverBar, ItemID.TungstenBar };
}
