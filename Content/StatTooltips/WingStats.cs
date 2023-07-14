namespace AccessoriesPlus.Content.StatTooltips;
internal class WingStats : Stats
{
    public float FlightTime { get; private set; } = -1f;
    public float FlightHeight { get; private set; } = -1f;
    public float MaxHSpeed { get; private set; } = -1f;
    public float HAccelerationMult { get; private set; } = 1f;
    public bool CanHover { get; private set; } = false;
    public float MaxHSpeedHover { get; private set; } = -1f;
    public float HAccelerationMultHover { get; private set; } = 1f;

    // TODO: add vertical wing stats

    // TODO: make this wing IDs instead of item IDs
    public static Dictionary<int, float> VanillaFlightHeight = new()
    {
        { ItemID.CreativeWings, 18 * 16f },
        { ItemID.AngelWings, 53 * 16f },
        { ItemID.DemonWings, 53 * 16f },
        { ItemID.FairyWings, 67 * 16f },
        { ItemID.FinWings, 67 * 16f },
        { ItemID.FrozenWings, 67 * 16f },
        { ItemID.HarpyWings, 67 * 16f },
        { ItemID.Jetpack, 77 * 16f },
        { ItemID.RedsWings, 77 * 16f },
        { ItemID.DTownsWings, 77 * 16f },
        { ItemID.WillsWings, 77 * 16f },
        { ItemID.CrownosWings, 77 * 16f },
        { ItemID.CenxsWings, 77 * 16f },
        { ItemID.BejeweledValkyrieWing, 77 * 16f },
        { ItemID.Yoraiz0rWings, 77 * 16f },
        { ItemID.JimsWings, 77 * 16f },
        { ItemID.SkiphsWings, 77 * 16f },
        { ItemID.LokisWings, 77 * 16f },
        { ItemID.ArkhalisWings, 77 * 16f },
        { ItemID.LeinforsWings, 77 * 16f },
        { ItemID.GhostarsWings, 77 * 16f },
        { ItemID.SafemanWings, 77 * 16f },
        { ItemID.FoodBarbarianWings, 77 * 16f },
        { ItemID.GroxTheGreatWings, 77 * 16f },
        { ItemID.LeafWings, 81 * 16f },
        { ItemID.BatWings, 81 * 16f },
        { ItemID.BeeWings, 81 * 16f },
        { ItemID.ButterflyWings, 81 * 16f },
        { ItemID.FlameWings, 81 * 16f },
        { ItemID.Hoverboard, 94 * 16f },
        { ItemID.BoneWings, 94 * 16f },
        { ItemID.MothronWings, 94 * 16f },
        { ItemID.GhostWings, 94 * 16f },
        { ItemID.BeetleWings, 94 * 16f },
        { ItemID.FestiveWings, 107 * 16f },
        { ItemID.SpookyWings, 107 * 16f },
        { ItemID.TatteredFairyWings, 107 * 16f },
        { ItemID.SteampunkWings, 107 * 16f },
        { ItemID.BetsyWings, 119 * 16f },
        { ItemID.RainbowWings, 128 * 16f },
        { ItemID.FishronWings, 143 * 16f },
        { ItemID.WingsNebula, 143 * 16f },
        { ItemID.WingsVortex, 143 * 16f },
        { ItemID.WingsSolar, 167 * 16f },
        { ItemID.WingsStardust, 167 * 16f },
        { ItemID.LongRainbowTrailWings, 201 * 16f },
    };

    private WingStats(float flightTime = -1f, float flightHeight = -1f, float maxHSpeed = -1f, float hAcceleration = 1f, bool canHover = false, float maxHSpeedHover = -1f, float hAccelerationHover = 1f, string misc = "") : base(misc)
    {
        FlightTime = flightTime;
        FlightHeight = flightHeight;
        MaxHSpeed = maxHSpeed;
        HAccelerationMult = hAcceleration;
        CanHover = canHover;
        MaxHSpeedHover = maxHSpeedHover;
        HAccelerationMultHover = hAccelerationHover;
    }

    public static WingStats Get(Item item)
    {
        if (item.wingSlot <= 0)
            return null;

        var stats = new WingStats();
        var vanillaStats = Main.LocalPlayer.GetWingStats(item.wingSlot);

        // Flight time
        stats.FlightTime = vanillaStats.FlyTime;

        // Flight height
        // TODO: calculate flight height
        stats.FlightHeight = VanillaFlightHeight.TryGetOrGiven(item.type, -1f);

        // Max horizontal speed
        stats.MaxHSpeed = vanillaStats.AccRunSpeedOverride;

        // Horizontal acceleration
        stats.HAccelerationMult = vanillaStats.AccRunAccelerationMult;

        // Can hover
        stats.CanHover = vanillaStats.HasDownHoverStats;

        // Max horizontal speed (hovering)
        stats.MaxHSpeedHover = vanillaStats.DownHoverSpeedOverride;

        // Horizontal acceleration (hovering)
        stats.HAccelerationMultHover = vanillaStats.DownHoverAccelerationMult;

        // Modded hooks
        // TODO: WingMovement (vertical) and WingAirLogicTweaks (horizontal)
        // ItemLoader.HorizontalWingSpeeds
        // ItemLoader.VerticalWingSpeeds

        return stats;
    }
}
