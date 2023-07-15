namespace AccessoriesPlus.Content.StatTooltips;
internal class StatTooltips : GlobalItem
{
    // TODO: cast all floats to decimals to avoid stupid rounding errors
    // TODO: consistent rounding: either 0.0, 0.5, or 0
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        var stats = Stats.GetStats(item);
        if (stats == null)
            return;

        var statTooltips = new List<TooltipLine>();
        stats.Apply(statTooltips);
        tooltips.InsertTooltips(stats.LineNameToInsertAround, stats.After, statTooltips.ToArray());
    }
}
