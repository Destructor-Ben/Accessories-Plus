using Terraria.ModLoader.Default;

namespace AccessoriesPlus.Content.AccessorySlots;
internal class CustomSlotSystem : GlobalItem
{
    // TODO: fill out
    private static List<int> BuilderAccessories = new()
    {
        ItemID.HandOfCreation,
    };

    private static List<int> BuilderAccessoriesFromAccPlus = new()
    {

    };

    // Forcing wings, etc. inmto modded slots
    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        var wingSlot = ModContent.GetInstance<WingSlot>();
        var shieldSlot = ModContent.GetInstance<ShieldSlot>();
        var bootSlot = ModContent.GetInstance<BootSlot>();

        // Have to minus the number of accessory slots, since that is how many slots ahead the vanity one is, and subtracting it gets back to the original slot
        int vanitySlot = slot - player.GetModPlayer<ModAccessorySlotPlayer>().SlotCount;
        if (Config.Instance.SlotWings && Config.Instance.SlotForceWings && wingSlot.IsValidItem(item))
            return modded && (wingSlot.Type == slot || wingSlot.Type == vanitySlot);
        else if (Config.Instance.SlotShield && Config.Instance.SlotForceShields && shieldSlot.IsValidItem(item))
            return modded && (shieldSlot.Type == slot || shieldSlot.Type == vanitySlot);
        else if (Config.Instance.SlotBoots && Config.Instance.SlotForceBoots && bootSlot.IsValidItem(item))
            return modded && (bootSlot.Type == slot || bootSlot.Type == vanitySlot);

        return true;
    }

    // Making builder accessories work in the inventory
    public override void UpdateInfoAccessory(Item item, Player player)
    {
        if (BuilderAccessories.Contains(item.type) || Config.Instance.ImprovedHandOfCreation && BuilderAccessoriesFromAccPlus.Contains(item.type))
        {
            Main.NewText(item.Name);
            player.CopyVanillaEquipEffects(item.type, true);
        }
    }
}
