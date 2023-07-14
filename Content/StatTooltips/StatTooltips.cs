namespace AccessoriesPlus.Content.StatTooltips;
internal class StatTooltips : GlobalItem
{
    // TODO: move the inserting to a method in the stats classes for better organization
    // TODO: cast all floats to decimals to avoid stupid rounding errors
    // TODO: use object initializers instead of contructors for all stats, because it makes duplicate default values for properties
    // TODO: consistent rounding: either 0.0, 0.5, or 0
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        // Getting stats
        var wingStats = WingStats.Get(item);
        var hookStats = HookStats.Get(item);
        var lightPetStats = LightPetStats.Get(item);
        var minecartStats = MinecartStats.Get(item);
        var mountStats = MountStats.Get(item);

        // Wings
        if (Config.Instance.StatsWings && wingStats != null)
        {
            AddMiscLine(tooltips, wingStats, "Equipable", after: true);

            var wingTooltips = new List<TooltipLine>
            {
                Util.GetTooltipLine("WingStats.FlightTime", Util.Round(wingStats.FlightTime / 60f, 0.1f)),
                wingStats.FlightHeight != -1f ? Util.GetTooltipLine("WingStats.FlightHeight", Util.Round(wingStats.FlightHeight / 16f, 0.1f)) : Util.GetTooltipLine("WingStats.FlightHeightUnknown"),
                wingStats.MaxHSpeed != -1f ? Util.GetTooltipLine("WingStats.MaxHSpeed", Util.Round(wingStats.MaxHSpeed * Util.PPTToMPH, 0.1f)) : Util.GetTooltipLine("WingStats.MaxHSpeedUnknown"),
                Util.GetTooltipLine("WingStats.HAccelerationMult", wingStats.HAccelerationMult),
            };

            if (wingStats.CanHover)
            {
                wingTooltips.Add(Util.GetTooltipLine("WingStats.CanHover"));
                wingTooltips.Add(wingStats.MaxHSpeedHover != -1f ? Util.GetTooltipLine("WingStats.MaxHSpeedHover", Util.Round(wingStats.MaxHSpeedHover * Util.PPTToMPH, 0.1f)) : Util.GetTooltipLine("WingStats.MaxHSpeedHoverUnknown"));
                wingTooltips.Add(Util.GetTooltipLine("WingStats.HAccelerationMultHover", wingStats.HAccelerationMultHover));
            }

            wingTooltips.Add(Util.GetTooltipLine("WingStats.NegatesFallDamage"));

            tooltips.InsertTooltips("Equipable", after: true, wingTooltips.ToArray());
        }

        // Hooks
        if (Config.Instance.StatsHooks && hookStats != null)
        {
            AddMiscLine(tooltips, hookStats, "Equipable", after: true);

            var hookTooltips = new List<TooltipLine>
            {
                hookStats.Reach != -1 ? Util.GetTooltipLine("HookStats.Reach", (decimal)Util.Round(hookStats.Reach / 16f, 0.1f)) : Util.GetTooltipLine("HookStats.ReachUnknown"),
                hookStats.NumHooks != -1 ? Util.GetTooltipLine("HookStats.NumHooks", hookStats.NumHooks) : Util.GetTooltipLine("HookStats.NumHooksUnknown"),
                Util.GetTooltipLine(hookStats.Latching switch
                {
                    HookStats.LatchingMode.Single => "HookStats.LatchingSingle",
                    HookStats.LatchingMode.Individual => "HookStats.LatchingIndividual",
                    HookStats.LatchingMode.Simultaneous => "HookStats.LatchingSimultaneous",
                    _ => "HookStats.LatchingUnknown",
                }),
                hookStats.ShootSpeed != -1 ? Util.GetTooltipLine("HookStats.ShootSpeed", Util.Round(hookStats.ShootSpeed * Util.PPTToMPH), 0.1f) : Util.GetTooltipLine("HookStats.ShootSpeedUnknown"),
                hookStats.RetreatSpeed != -1 ? Util.GetTooltipLine("HookStats.RetreatSpeed", Util.Round(hookStats.RetreatSpeed * Util.PPTToMPH), 0.1f) : Util.GetTooltipLine("HookStats.RetreatSpeedUnknown"),
                hookStats.PullSpeed != -1 ? Util.GetTooltipLine("HookStats.PullSpeed", Util.Round(hookStats.PullSpeed * Util.PPTToMPH), 0.1f) : Util.GetTooltipLine("HookStats.PullSpeedUnknown"),
            };

            tooltips.InsertTooltips("Equipable", after: true, hookTooltips.ToArray());
        }

        // Light pets
        if (Config.Instance.StatsLightPets && lightPetStats != null)
        {
            AddMiscLine(tooltips, lightPetStats, "Equipable", after: true);

            var lightPetTooltips = new List<TooltipLine>
            {
                lightPetStats.Brightness != -1f ? Util.GetTooltipLine("LightPetStats.Brightness", (int)(lightPetStats.Brightness * 100f)) : Util.GetTooltipLine("LightPetStats.BrightnessUnknown")
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
            AddMiscLine(tooltips, minecartStats, "Equipable", after: true);

            var mountTooltips = new List<TooltipLine>();

            // Flight
            if (mountStats.FlightTime > 0)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.FlightTime", (decimal)Util.Round(mountStats.FlightTime / 60f, 0.1f)));

            // Run speeds
            if (mountStats.RunSpeed != 0f)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.RunSpeed", (decimal)Util.Round(mountStats.RunSpeed * Util.PPTToMPH, 0.1f)));

            if (mountStats.SwimSpeed != 0f)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.SwimSpeed", (decimal)Util.Round(mountStats.SwimSpeed * Util.PPTToMPH, 0.1f)));

            if (mountStats.Acceleration != 0f)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.Acceleration", mountStats.Acceleration));// TODO: make this a unit that is visible

            // Jumping
            if (mountStats.JumpSpeed != 0f)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.JumpSpeed", (decimal)Util.Round(mountStats.JumpSpeed * Util.PPTToMPH, 0.1f)));

            if (mountStats.JumpHeight != 0)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.JumpHeight", (decimal)Util.Round(mountStats.JumpHeight / 16f, 0.1f)));

            // Misc
            if (mountStats.HeightBoost != 0)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.HeightBoost", (decimal)Util.Round(mountStats.HeightBoost / 16f, 0.1f)));

            if (mountStats.FallDamageMult != 1f)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.FallDamageMult", mountStats.FallDamageMult));

            // Non stat lines
            if (mountStats.CanHover)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.CanHover"));

            if (mountStats.AutoJump)
                mountTooltips.Add(Util.GetTooltipLine("MountStats.AutoJump"));

            tooltips.InsertTooltips("Equipable", after: true, mountTooltips.ToArray());
        }
    }

    // Adds the misc info to the tooltips
    // TODO: either move this to be added after all of the other tooltips or remove misc liens entirely
    private static void AddMiscLine(List<TooltipLine> tooltips, Stats stats, string nameToInsertAfter, bool after)
    {
        if (!string.IsNullOrEmpty(stats?.Misc ?? ""))
            tooltips.InsertTooltips(nameToInsertAfter, after, Util.GetTooltipLine(stats.Misc));
    }
}
