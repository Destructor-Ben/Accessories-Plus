using TerraUtil.RecipeGroups;

namespace AccessoriesPlus.Content.RecipeGroups;
internal class Cobalt : ModRecipeGroup
{
    public override List<int> ValidItems => new() { ItemID.CobaltBar, ItemID.PalladiumBar };
}
