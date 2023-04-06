using Terraria;
using Terraria.ModLoader;

namespace AccessoriesPlus.Content.AccessorySlots;
internal class CustomSlotSystem : GlobalItem
{
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        // TODO - Can't put in vanity slots
        var wingSlot = ModContent.GetInstance<WingSlot>();
        var shieldSlot = ModContent.GetInstance<ShieldSlot>();
        var bootSlot = ModContent.GetInstance<BootSlot>();

        if (wingSlot.IsValidItem(item))
            return wingSlot.Type == slot;
        else if (shieldSlot.IsValidItem(item))
            return shieldSlot.Type == slot;
        else if (bootSlot.IsValidItem(item))
            return bootSlot.Type == slot;

        return true;
    }
}
