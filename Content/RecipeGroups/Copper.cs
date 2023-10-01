using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Copper : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.CopperBar, ItemID.TinBar };
}
