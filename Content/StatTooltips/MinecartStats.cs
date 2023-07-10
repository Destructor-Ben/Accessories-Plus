namespace AccessoriesPlus.Content.StatTooltips;
internal class MinecartStats : Stats
{
    public float MaxSpeed { get; private set; } = -1f;
    public float Acceleration { get; private set; } = -1f;

    private MinecartStats(float maxSpeed = -1f, float acceleration = -1f, string misc = "") : base(misc)
    {
        MaxSpeed = maxSpeed;
        Acceleration = acceleration;
    }

    public static MinecartStats Get(Item item)
    {
        // TODO: allow modded minecart stats and also allow different super cart stats
        return item.mountType == -1 || !MountID.Sets.Cart[item.mountType]
            ? null
            : Main.LocalPlayer.UsingSuperCart
            ? new MinecartStats(102f, 31f)
            : item.mountType == MountID.DiggingMoleMinecart
            ? new MinecartStats(31f, 6f)
            : new MinecartStats(66f, 12f);
    }
}
