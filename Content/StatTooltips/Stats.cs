namespace AccessoriesPlus.Content.StatTooltips;
// TODO - REMOVE
internal abstract class Stats
{
    public string Misc { get; private set; }

    protected Stats(string misc = "")
    {
        Misc = misc;
    }
}
