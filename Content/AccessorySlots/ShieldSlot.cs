﻿namespace AccessoriesPlus.Content.AccessorySlots;

public class ShieldSlot : AbstractAccessorySlot
{
    public override int FunctionalItemID => ItemID.CobaltShield;
    public override int VanityItemID => ItemID.AnkhShield;

    public override bool IsValidItem(Item item)
    {
        return item.shieldSlot > 0;
    }

    public override bool IsEnabled()
    {
        return Config.Instance.SlotShield;
    }
}
