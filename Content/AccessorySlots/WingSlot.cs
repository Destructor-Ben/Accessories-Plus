using Terraria;
using Terraria.ID;

namespace AccessoriesPlus.Content.AccessorySlots;
internal class WingSlot : AbstractAccessorySlot
{
    public override int FunctionalItemID => ItemID.CreativeWings;
    public override int VanityItemID => ItemID.WingsSolar;

    public override bool IsValidItem(Item item)
    {
        return item.wingSlot > 0;
    }
}