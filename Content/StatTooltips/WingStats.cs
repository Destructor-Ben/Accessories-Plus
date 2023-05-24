namespace AccessoriesPlus.Content.StatTooltips;
internal class WingStats : Stats
{
    /*
    Wings
        WingStats automatically added - Contains flight time and horizontal movement
        Manually add vanilla for WingMovement (veritcal) and WingAirLogicTweaks (horizontal)
        Automatically add for ModItems
        Manually add flight height (or calculate)
    */

    private WingStats(string misc = "") : base(misc)
    {
    }

    public static WingStats Get(Item item)
    {
        if (item.wingSlot <= 0)
            return null;

        var stats = new WingStats();

        // Flight time

        // Flight height

        // Max horizontal speed

        // Horizontal acceleration

        // Can hover

        // Max horizontal speed (hovering)

        // Horizontal acceleration (hovering)

        return stats;
    }
}
