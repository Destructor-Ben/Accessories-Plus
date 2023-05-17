using Terraria.ModLoader.Default;

namespace AccessoriesPlus.Content.AccessorySlots;
internal class CustomSlotSystem : GlobalItem
{
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        var wingSlot = ModContent.GetInstance<WingSlot>();
        var shieldSlot = ModContent.GetInstance<ShieldSlot>();
        var bootSlot = ModContent.GetInstance<BootSlot>();

        // Have to minus the number of accessory slots, since that is how many slots ahead the vanity one is, and subtracting it gets back to the original slot
        int vanitySlot = slot - player.GetModPlayer<ModAccessorySlotPlayer>().SlotCount;
        if (Config.Instance.SlotForceWings && wingSlot.IsValidItem(item))
            return modded && (wingSlot.Type == slot || wingSlot.Type == vanitySlot);
        else if (Config.Instance.SlotForceShields && shieldSlot.IsValidItem(item))
            return modded && (shieldSlot.Type == slot || shieldSlot.Type == vanitySlot);
        else if (Config.Instance.SlotForceBoots && bootSlot.IsValidItem(item))
            return modded && (bootSlot.Type == slot || bootSlot.Type == vanitySlot);

        return true;
    }
}
