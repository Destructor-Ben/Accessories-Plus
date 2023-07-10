namespace AccessoriesPlus.Content.StatTooltips;
internal class WingStats : Stats
{
    public float FlightTime = -1f;
    public float FlightHeight = -1f;
    public float MaxHSpeed = -1f;
    public float HAcceleration = -1f;
    public bool CanHover = false;
    public float MaxHSpeedHover = -1f;
    public float HAccelerationHover = -1f;

    // TODO: add vertical wing stats

    // TODO: fill this out
    // TODO: combine these into a single dictionary of WingStats
    public static Dictionary<int, float> VanillaFlightHeight = new()
    {
        { ItemID.WingsSolar, 16f * 100 },
    };

    private WingStats(float flightTime = -1f, float flightHeight = -1f, float maxHSpeed = -1f, float hAcceleration = -1f, bool canHover = false, float maxHSpeedHover = -1f, float hAccelerationHover = -1f, string misc = "") : base(misc)
    {
        FlightTime = flightTime;
        FlightHeight = flightHeight;
        MaxHSpeed = maxHSpeed;
        HAcceleration = hAcceleration;
        CanHover = canHover;
        MaxHSpeedHover = maxHSpeedHover;
        HAccelerationHover = hAccelerationHover;
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
        // TODO: calculate
        stats.FlightHeight = Util.FromDictOrDefault(item.type, VanillaFlightHeight, -1f);

        // Max horizontal speed
        stats.MaxHSpeed = vanillaStats.AccRunSpeedOverride;

        // Horizontal acceleration
        stats.HAcceleration = vanillaStats.AccRunAccelerationMult;

        // Can hover
        stats.CanHover = vanillaStats.HasDownHoverStats;

        // Max horizontal speed (hovering)
        stats.MaxHSpeed = vanillaStats.DownHoverSpeedOverride;

        // Horizontal acceleration (hovering)
        stats.HAcceleration = vanillaStats.DownHoverAccelerationMult;

        // Modded hooks
        // TODO: WingMovement (vertical) and WingAirLogicTweaks (horizontal)
        //ItemLoader.HorizontalWingSpeeds
        //ItemLoader.VerticalWingSpeeds

        return stats;
    }
}
