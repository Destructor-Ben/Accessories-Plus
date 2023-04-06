using Terraria;
using Terraria.ID;

namespace AccessoriesPlus.Content.AccessorySlots;
internal class BootSlot : AbstractAccessorySlot
{
    public override int FunctionalItemID => ItemID.HermesBoots;
    public override int VanityItemID => ItemID.TerrasparkBoots;

    public override bool IsValidItem(Item item)
    {
        return item.shoeSlot > 0;
    }
}
