namespace AccessoriesPlus.Content.StatTooltips;
internal class MountStats : Stats
{
    public int FlightTime { get; private set; } = 0;
    public bool CanHover { get; private set; } = false;
    public float RunSpeed { get; private set; } = 0f;
    public float SwimSpeed { get; private set; } = 0f;
    public float Acceleration { get; private set; } = 0f;
    public float JumpSpeed { get; private set; } = 0f;
    public int JumpHeight { get; private set; } = 0;
    public bool AutoJump { get; private set; } = false;
    public int HeightBoost { get; private set; } = 0;
    public float FallDamageMult { get; private set; } = 0f;

    // TODO: add parameters to constructor? i want to remove constructors anyway and use object initializers
    private MountStats(string misc = "") : base(misc)
    {
    }

    public static MountStats Get(Item item)
    {
        if (item.mountType < 0 || MountID.Sets.Cart[item.mountType])
            return null;

        var stats = new MountStats();
        var vanillaStats = Mount.mounts[item.mountType];

        // Flight time
        stats.FlightTime = vanillaStats.flightTimeMax;

        // Can hover
        stats.CanHover = vanillaStats.usesHover;

        // Run speed
        stats.RunSpeed = Math.Max(vanillaStats.runSpeed, vanillaStats.dashSpeed);

        // Swim speed
        stats.SwimSpeed = vanillaStats.swimSpeed;

        // Acceleration
        stats.Acceleration = vanillaStats.acceleration;

        // Jump speed
        stats.JumpSpeed = vanillaStats.jumpSpeed;

        // Jump height
        stats.JumpHeight = vanillaStats.jumpHeight;

        // Autojump
        stats.AutoJump = vanillaStats.constantJump;

        // Height boost
        stats.HeightBoost = vanillaStats.heightBoost;

        // Fall damage multiplier
        stats.FallDamageMult = vanillaStats.fallDamage;

        return stats;
    }
}
