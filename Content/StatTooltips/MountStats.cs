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

    private MountStats() { }

    public static MountStats Get(Item item)
    {
        if (item.mountType <= MountID.None || MountID.Sets.Cart[item.mountType])
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

    public override void Apply(List<TooltipLine> tooltips)
    {
        if (!Config.Instance.StatsMounts)
            return;

        // Flight
        if (FlightTime > 0)
            tooltips.Add(Util.GetTooltipLine("MountStats.FlightTime", (decimal)Util.Round(FlightTime / 60f, 0.1f)));

        // Run speeds
        if (RunSpeed != 0f)
            tooltips.Add(Util.GetTooltipLine("MountStats.RunSpeed", (decimal)Util.Round(RunSpeed * Util.PPTToMPH, 0.1f)));

        if (SwimSpeed != 0f)
            tooltips.Add(Util.GetTooltipLine("MountStats.SwimSpeed", (decimal)Util.Round(SwimSpeed * Util.PPTToMPH, 0.1f)));

        if (Acceleration != 0f)
            tooltips.Add(Util.GetTooltipLine("MountStats.Acceleration", (decimal)Util.Round(Acceleration * Util.PPTPTToMPHPS, 0.1f)));

        // Jumping
        if (JumpSpeed != 0f)
            tooltips.Add(Util.GetTooltipLine("MountStats.JumpSpeed", (decimal)Util.Round(JumpSpeed * Util.PPTToMPH, 0.1f)));// TODO: is this correct?

        if (JumpHeight != 0)
            tooltips.Add(Util.GetTooltipLine("MountStats.JumpHeight", (decimal)Util.Round(JumpHeight / 16f, 0.1f)));// TODO: is this correct?

        // Misc
        if (HeightBoost != 0)
            tooltips.Add(Util.GetTooltipLine("MountStats.HeightBoost", (decimal)Util.Round(HeightBoost / 16f, 0.1f)));

        if (FallDamageMult != 1f)
            tooltips.Add(Util.GetTooltipLine("MountStats.FallDamageMult", FallDamageMult));

        // Non stat lines
        if (CanHover)
            tooltips.Add(Util.GetTooltipLine("MountStats.CanHover"));

        if (AutoJump)
            tooltips.Add(Util.GetTooltipLine("MountStats.AutoJump"));
    }
}
