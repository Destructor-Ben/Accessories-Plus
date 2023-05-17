namespace AccessoriesPlus.Content.ImprovedAccessories;
internal class AccessoryItem : GlobalItem
{
    // Accessory effects
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        switch (item.type)
        {
            case ItemID.HandOfCreation:
                // Hand of creation
                player.CopyVanillaEquipEffects(ItemID.Toolbelt, hideVisual);
                player.CopyVanillaEquipEffects(ItemID.Toolbox, hideVisual);
                break;
            case ItemID.TerrasparkBoots:
                // Terraspark boots
                player.CopyVanillaEquipEffects(ItemID.AmphibianBoots, hideVisual);
                break;
            case ItemID.AnkhCharm:
            case ItemID.AnkhShield:
                // Ankh shield
                player.CopyVanillaEquipEffects(ItemID.HandWarmer, hideVisual);
                break;
            case ItemID.BundleofBalloons:
            case ItemID.HorseshoeBundle:
                // Bundle of Horseshoe balloons
                // TODO - Change texture of Bundle of balloons
                player.CopyVanillaEquipEffects(ItemID.FartInABalloon, hideVisual);
                player.CopyVanillaEquipEffects(ItemID.SharkronBalloon, hideVisual);
                break;
            default:
                break;
        }
    }

    // Changing tooltips
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        switch (item.type)
        {
            case ItemID.HandOfCreation:
                // Hand of creation
                tooltips.InsertTooltips("Tooltip2", after: true, Util.GetTooltipLine("HandOfCreation0"), Util.GetTooltipLine("HandOfCreation1"));
                tooltips.RemoveTooltips("Tooltip2");
                break;
            case ItemID.TerrasparkBoots:
                // Terraspark boots
                tooltips.InsertTooltips("Tooltip4", after: true, Util.GetTooltipLine("TerrasparkBoots0"), Util.GetTooltipLine("TerrasparkBoots1"));
                break;
            case ItemID.BundleofBalloons:
            case ItemID.HorseshoeBundle:
                // Bundle of horseshoe balloons
                tooltips.InsertTooltips("Tooltip0", after: true, Util.GetTooltipLine("BundleOfBalloons0"));
                tooltips.RemoveTooltips("Tooltip0");
                break;
            default:
                break;
        }
    }
}
