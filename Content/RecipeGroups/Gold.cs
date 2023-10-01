using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Gold : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.GoldBar, ItemID.PlatinumBar };
}
