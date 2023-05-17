namespace AccessoriesPlus.Content;
// TODO - add Mod.Call API for adding stats for modded items, otherwise try to get them with ModItem
// tODO - finish
internal class StatTooltips : GlobalItem
{
    internal static Dictionary<int, WingStats> wingStats;
    internal static Dictionary<int, HookStats> hookStats;
    internal static Dictionary<int, LightPetStats> lightPetStats;
    internal static Dictionary<int, MinecartStats> minecartStats;
    internal static Dictionary<int, MountStats> mountStats;

    public override void Load()
    {
        wingStats = new Dictionary<int, WingStats>();
        hookStats = new Dictionary<int, HookStats>();
        lightPetStats = new Dictionary<int, LightPetStats>();
        minecartStats = new Dictionary<int, MinecartStats>();
        mountStats = new Dictionary<int, MountStats>();
    }

    public override void SetStaticDefaults()
    {
        // TODO - manually add stats
        #region Vanilla Wing Stats https://terraria.wiki.gg/wiki/Wings/List
        wingStats.Add(ItemID.WingsSolar, new WingStats() { FlightTime = 3, FlightHeight = 167, HorizontalSpeed = 46, VerticalMultiplier = 300 });
        #endregion

        #region Vanilla Hook Stats https://terraria.wiki.gg/wiki/Hooks
        // Pre hardmode
        hookStats.Add(ItemID.GrapplingHook, new HookStats() { Reach = 18.75f, Velocity = 11.5f, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.AmethystHook, new HookStats() { Reach = 18.75f, Velocity = 10, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.SquirrelHook, new HookStats() { Reach = 19, Velocity = 11.5f, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.TopazHook, new HookStats() { Reach = 20.625f, Velocity = 10.5f, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.SapphireHook, new HookStats() { Reach = 22.5f, Velocity = 11, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.EmeraldHook, new HookStats() { Reach = 24.375f, Velocity = 11.5f, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.RubyHook, new HookStats() { Reach = 26.25f, Velocity = 12, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.AmberHook, new HookStats() { Reach = 27.5f, Velocity = 12.5f, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.DiamondHook, new HookStats() { Reach = 29.125f, Velocity = 12.5f, NumHooks = 1, LatchingMode = LatchingMode.Single });

        hookStats.Add(ItemID.WebSlinger, new HookStats() { Reach = 22.625f, Velocity = 10, NumHooks = 8, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.SkeletronHand, new HookStats() { Reach = 21.875f, Velocity = 15, NumHooks = 2, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.SlimeHook, new HookStats() { Reach = 18.75f, Velocity = 13, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.FishHook, new HookStats() { Reach = 25, Velocity = 13, NumHooks = 2, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.IvyWhip, new HookStats() { Reach = 28, Velocity = 13, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.BatHook, new HookStats() { Reach = 31.25f, Velocity = 13.5f, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.CandyCaneHook, new HookStats() { Reach = 25, Velocity = 11.5f, NumHooks = 1, LatchingMode = LatchingMode.Single });

        // Hardmode
        hookStats.Add(ItemID.DualHook, new HookStats() { Reach = 27.5f, Velocity = 14, NumHooks = 2, LatchingMode = LatchingMode.Individual });
        hookStats.Add(ItemID.QueenSlimeHook, new HookStats() { Reach = 30, Velocity = 16, NumHooks = 1, LatchingMode = LatchingMode.Single });
        hookStats.Add(ItemID.ThornHook, new HookStats() { Reach = 30, Velocity = 16, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.IlluminantHook, new HookStats() { Reach = 30, Velocity = 15, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.WormHook, new HookStats() { Reach = 30, Velocity = 15, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.TendonHook, new HookStats() { Reach = 30, Velocity = 15, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.AntiGravityHook, new HookStats() { Reach = 31.25f, Velocity = 14, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.SpookyHook, new HookStats() { Reach = 34.375f, Velocity = 15.5f, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.ChristmasHook, new HookStats() { Reach = 34.375f, Velocity = 15.5f, NumHooks = 3, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.LunarHook, new HookStats() { Reach = 34.375f, Velocity = 18, NumHooks = 4, LatchingMode = LatchingMode.Simultaneous });
        hookStats.Add(ItemID.StaticHook, new HookStats() { Reach = 37.5f, Velocity = 16, NumHooks = 2, LatchingMode = LatchingMode.Individual });
        #endregion

        #region Vanilla Light pet Stats https://terraria.wiki.gg/wiki/Pets#Light_Pets
        #endregion

        #region Vanilla Minecart Stats https://terraria.wiki.gg/wiki/Minecarts
        #endregion

        #region Vanilla Mount Stats https://terraria.wiki.gg/wiki/Mounts
        #endregion
    }

    public override void Unload()
    {
        wingStats?.Clear();
        wingStats = null;
        hookStats?.Clear();
        hookStats = null;
        lightPetStats?.Clear();
        lightPetStats = null;
        minecartStats?.Clear();
        minecartStats = null;
        mountStats?.Clear();
        mountStats = null;
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (Config.Instance.StatsWings && wingStats.TryGetValue(item.type, out var statWings))
        {
            tooltips.InsertTooltips("Tooltip0",
                after: true, Util.GetTooltipLine("WingStats.NegateFallDamage"),
                Util.GetTooltipLine("WingStats.FlightTime", statWings.FlightTime),
                Util.GetTooltipLine("WingStats.FlightHeight", statWings.FlightHeight),
                Util.GetTooltipLine("WingStats.HorizontalSpeed", statWings.HorizontalSpeed),// TODO - horzintal speed and vert mult
                Util.GetTooltipLine("WingStats.VerticalMultiplier", statWings.VerticalMultiplier)
            );

            if (statWings.Misc != null)
                tooltips.InsertTooltips("AccessoriesPlus/WingStats.VerticalMultiplier", after: true, Util.GetTooltipLine(statWings.Misc));
        }

        if (Config.Instance.StatsHooks && hookStats.TryGetValue(item.type, out var statHook))
        {
            tooltips.InsertTooltips("Equipable",
                after: true, Util.GetTooltipLine("HookStats.Reach", statHook.Reach),
                Util.GetTooltipLine("HookStats.Velocity", statHook.Velocity),// TOO - convert pixels to mph
                Util.GetTooltipLine("HookStats.NumHooks", statHook.NumHooks),
                statHook.LatchingMode == LatchingMode.Simultaneous ? Util.GetTooltipLine("HookStats.LatchingSimultaneous") : statHook.LatchingMode == LatchingMode.Individual ? Util.GetTooltipLine("HookStats.LatchingIndividual") : Util.GetTooltipLine("HookStats.LatchingSingle")
            );

            if (statHook.Misc != null)
                tooltips.InsertTooltips("AccessoriesPlus/HookStats.NumHooks", after: true, Util.GetTooltipLine(statHook.Misc));
        }

        if (Config.Instance.StatsLightPets && lightPetStats.TryGetValue(item.type, out var statLightPet))
        {
            tooltips.InsertTooltips("Equipable",
                after: true, Util.GetTooltipLine("LightPetStats.TODO"),
                statLightPet.Misc != null ? Util.GetTooltipLine(statLightPet.Misc) : null
            );
        }

        if (Config.Instance.StatsMinecarts && minecartStats.TryGetValue(item.type, out var statMinecart))
        {
            tooltips.InsertTooltips("Equipable",
                after: true, Util.GetTooltipLine("MinecartStats.TODO"),
                statMinecart.Misc != null ? Util.GetTooltipLine(statMinecart.Misc) : null
            );
        }

        if (Config.Instance.StatsMounts && mountStats.TryGetValue(item.type, out var statMount))
        {
            tooltips.InsertTooltips("Equipable",
                after: true, Util.GetTooltipLine("MountStats.TODO"),
                statMount.Misc != null ? Util.GetTooltipLine(statMount.Misc) : null
            );
        }
    }

    internal enum LatchingMode
    {
        Single,
        Individual,
        Simultaneous,
        Unknown
    }

    internal class WingStats : Stats
    {
        public float FlightTime = -1f;
        public float FlightHeight = -1f;
        public float HorizontalSpeed = -1f;
        public float VerticalMultiplier = -1f;
    }

    internal class HookStats : Stats
    {
        public float Reach = -1f;
        public float Velocity = -1f;
        public int NumHooks = -1;
        public LatchingMode LatchingMode = LatchingMode.Unknown;
    }

    internal class LightPetStats : Stats
    {
        public float Brightness = -1f;
        public bool Controllable = false;
        public bool ExposesTreasure = false;
    }

    internal class MinecartStats : Stats
    {
    }

    internal class MountStats : Stats
    {
    }

    internal abstract class Stats
    {
        public string Misc = null;
    }
}
