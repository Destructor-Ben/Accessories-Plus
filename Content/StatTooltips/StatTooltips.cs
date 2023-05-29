namespace AccessoriesPlus.Content.StatTooltips;
internal class StatTooltips : GlobalItem
{
    // TODO - for stats that have manual input, get it from the hooks that tML provides
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        // Getting stats
        var wingStats = WingStats.Get(item);
        var hookStats = HookStats.Get(item);
        var lightPetStats = LightPetStats.Get(item);
        var minecartStats = MinecartStats.Get(item);
        var mountStats = MountStats.Get(item);

        // Wings
        // TODO - fix the speeds and accelerations
        if (Config.Instance.StatsWings && wingStats != null)
        {
            AddMiscLine(tooltips, wingStats, "Equipable", after: true);
            var wingTooltips = new List<TooltipLine>
            {
                Util.GetTooltipLine("WingStats.FlightTime", Util.RoundToNearest(wingStats.FlightTime / 60f, 0.5f)),
                wingStats.FlightHeight != -1f ? Util.GetTooltipLine("WingStats.FlightHeight", Util.RoundToNearest(wingStats.FlightHeight / 60f, 0.5f)) : Util.GetTooltipLine("WingStats.FlightHeightUnknown"),
                Util.GetTooltipLine("WingStats.MaxHSpeed", wingStats.MaxHSpeed),
                Util.GetTooltipLine("WingStats.HAcceleration", wingStats.HAcceleration),
            };

            if (wingStats.CanHover)
            {
                wingTooltips.Add(Util.GetTooltipLine("WingStats.CanHover"));
                if (wingStats.MaxHSpeedHover != -1)
                    wingTooltips.Add(Util.GetTooltipLine("WingStats.MaxHSpeedHover", wingStats.MaxHSpeedHover));
                if (wingStats.HAccelerationHover != -1)
                    wingTooltips.Add(Util.GetTooltipLine("WingStats.HAccelerationHover", wingStats.HAccelerationHover));
            }

            // TODO vertical stats

            wingTooltips.Add(Util.GetTooltipLine("WingStats.NegatesFallDamage"));

            tooltips.InsertTooltips("Equipable", after: true, wingTooltips.ToArray());
        }

        // Hooks
        if (Config.Instance.StatsHooks && hookStats != null)
        {
            // TODO - Fix conversions, check stats, fix rounding
            AddMiscLine(tooltips, hookStats, "Equipable", after: true);
            var hookTooltips = new List<TooltipLine>
            {
                hookStats.Reach != -1 ? Util.GetTooltipLine("HookStats.Reach", Util.RoundToNearest(hookStats.Reach / 16f, 0.5f)) : Util.GetTooltipLine("HookStats.ReachUnknown"),
                hookStats.NumHooks != -1 ? Util.GetTooltipLine("HookStats.NumHooks", hookStats.NumHooks) : Util.GetTooltipLine("HookStats.NumHooksUnknown"),
                Util.GetTooltipLine(hookStats.Latching switch
                {
                    HookStats.LatchingMode.Single => "HookStats.LatchingSingle",
                    HookStats.LatchingMode.Individual => "HookStats.LatchingIndividual",
                    HookStats.LatchingMode.Simultaneous => "HookStats.LatchingSimultaneous",
                    _ => "HookStats.LatchingUnknown",
                }),
                hookStats.ShootSpeed != -1 ? Util.GetTooltipLine("HookStats.ShootSpeed", Util.RoundToNearest(hookStats.ShootSpeed * Util.PPTToMPH), 0.5f) : Util.GetTooltipLine("HookStats.ShootSpeedUnknown"),
                hookStats.RetreatSpeed != -1 ? Util.GetTooltipLine("HookStats.RetreatSpeed", Util.RoundToNearest(hookStats.RetreatSpeed * Util.PPTToMPH), 0.5f) : Util.GetTooltipLine("HookStats.RetreatSpeedUnknown"),
                hookStats.PullSpeed != -1 ? Util.GetTooltipLine("HookStats.PullSpeed", Util.RoundToNearest(hookStats.PullSpeed * Util.PPTToMPH), 0.5f) : Util.GetTooltipLine("HookStats.PullSpeedUnknown"),
            };

            tooltips.InsertTooltips("Equipable", after: true, hookTooltips.ToArray());
        }

        // Light pets
        if (Config.Instance.StatsLightPets && lightPetStats != null)
        {
            AddMiscLine(tooltips, lightPetStats, "Equipable", after: true);
            var lightPetTooltips = new List<TooltipLine>
            {
                Util.GetTooltipLine("LightPetStats.Brightness", lightPetStats.Brightness != -1f ? (int)(lightPetStats.Brightness * 100f) : Util.GetTextValue("Unknown"))
            };

            if (lightPetStats.Controllable)
                lightPetTooltips.Add(Util.GetTooltipLine("LightPetStats.Controllable"));
            if (lightPetStats.ExposesEnemies)
                lightPetTooltips.Add(Util.GetTooltipLine("LightPetStats.ExposesEnemies"));
            if (lightPetStats.ExposesTreasure)
                lightPetTooltips.Add(Util.GetTooltipLine("LightPetStats.ExposesTreasure"));

            tooltips.InsertTooltips("Equipable", after: true, lightPetTooltips.ToArray());
        }

        // Minecarts
        if (Config.Instance.StatsMinecarts && minecartStats != null)
        {
            AddMiscLine(tooltips, minecartStats, "Equipable", after: true);
            var minecartTooltips = new List<TooltipLine>
            {
                Util.GetTooltipLine("MinecartStats.MaxSpeed", (int)minecartStats.MaxSpeed),
                Util.GetTooltipLine("MinecartStats.Acceleration", (int)minecartStats.Acceleration)
            };

            tooltips.InsertTooltips("Equipable", after: true, minecartTooltips.ToArray());
        }

        // Mounts
        if (Config.Instance.StatsMounts && mountStats != null)
        {

        }
    }

    // Adds the misc info to the tooltips
    private static void AddMiscLine(List<TooltipLine> tooltips, Stats stats, string nameToInsertAfter, bool after)
    {
        if (!string.IsNullOrEmpty(stats.Misc))
            tooltips.InsertTooltips(nameToInsertAfter, after, Util.GetTooltipLine(stats.Misc));
    }
}
