using Terraria.ModLoader;

namespace AccessoriesPlus;
internal class AccessoriesPlus : Mod
{
    // TODO intermod compat
    internal static Mod Calamity;
    internal static Mod Thorium;
    internal static Mod ModOfRedemption;

    internal static bool CalamityEnabled;
    internal static bool ThoriumEnabled;
    internal static bool ModOfRedemptionEnabled;

    public override void Load()
    {
        CalamityEnabled = ModLoader.TryGetMod("CalamityMod", out Calamity);
        ThoriumEnabled = ModLoader.TryGetMod("Thorium", out Thorium);
        ModOfRedemptionEnabled = ModLoader.TryGetMod("ModOfRedemption", out ModOfRedemption);
    }

    // TODO allow stat adding
    public override object Call(params object[] args)
    {
        return base.Call(args);
    }
}
