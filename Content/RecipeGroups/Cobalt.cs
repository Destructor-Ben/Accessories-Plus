using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;

public class Cobalt : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.CobaltBar, ItemID.PalladiumBar };
}
