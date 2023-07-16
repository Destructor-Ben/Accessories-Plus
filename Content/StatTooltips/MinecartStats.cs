namespace AccessoriesPlus.Content.StatTooltips;
// TODO: merge minecart stats into mount stats
internal class MinecartStats : Stats
{
    // TODO: change these to pixel values
    public float MaxSpeed { get; private set; } = -1f;
    public float Acceleration { get; private set; } = -1f;

    private MinecartStats() { }

    public static MinecartStats Get(Item item)
    {
        // TODO: make this more compatible with mods, use MountStats, do I even need this?
        return item.mountType <= MountID.None || !MountID.Sets.Cart[item.mountType]
            ? null
            : Main.LocalPlayer.UsingSuperCart
            ? new MinecartStats { MaxSpeed = 102f, Acceleration = 31f }
            : item.mountType == MountID.DiggingMoleMinecart
            ? new MinecartStats { MaxSpeed = 31f, Acceleration = 6f }
            : new MinecartStats { MaxSpeed = 66f, Acceleration = 12f };
    }

    public override void Apply(List<TooltipLine> tooltips)
    {
        if (!Config.Instance.StatsMinecarts)
            return;

        tooltips.Add(Util.GetTooltipLine("MinecartStats.MaxSpeed", (int)MaxSpeed));
        tooltips.Add(Util.GetTooltipLine("MinecartStats.Acceleration", (int)Acceleration));
    }
}
