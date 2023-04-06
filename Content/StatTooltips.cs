using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AccessoriesPlus.Content;
internal class StatTooltips : GlobalItem
{
    internal static Dictionary<int, WingStats> wingStats;
    internal static Dictionary<int, HookStats> hookStats;
    internal static Dictionary<int, LighPetStats> lightPetStats;
    internal static Dictionary<int, MinecartStats> minecartStats;
    internal static Dictionary<int, MountStats> mountStats;

    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;// TODO temporary
    }

    public override void Load()
    {
        // Vanilla wing stats https://terraria.wiki.gg/wiki/Wings/List

        // Vanilla hook stats https://terraria.wiki.gg/wiki/Hooks

        // Vanilla light pet stats https://terraria.wiki.gg/wiki/Pets#Light_Pets

        // Vanilla minecart stats https://terraria.wiki.gg/wiki/Minecarts

        // Vanilla mount stats https://terraria.wiki.gg/wiki/Mounts
    }

    public override void Unload()
    {
        /*wingStats.Clear();
        wingStats = null;
        hookStats.Clear();
        hookStats = null;
        lightPetStats.Clear();
        lightPetStats = null;
        minecartStats.Clear();
        minecartStats = null;
        mountStats.Clear();
        mountStats = null;*/
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (wingStats.TryGetValue(item.type, out var statWings))
        {
            tooltips.InsertRange(tooltips.FindIndex(0, l => l.Name == "Equipable"), new List<TooltipLine>
            {
                new TooltipLine(Mod, "AccessoriesPlus: NegateFallDmg", statWings.negatesFallDamage.Value),
                new TooltipLine(Mod, "AccessoriesPlus: NegateFallDmg", statWings.negatesFallDamage.Value),
                new TooltipLine(Mod, "AccessoriesPlus: NegateFallDmg", statWings.negatesFallDamage.Value),
                new TooltipLine(Mod, "AccessoriesPlus: NegateFallDmg", statWings.negatesFallDamage.Value),
                new TooltipLine(Mod, "AccessoriesPlus: NegateFallDmg", statWings.negatesFallDamage.Value),
            });
        }

        if (hookStats.TryGetValue(item.type, out var statHook))
            tooltips.Add(new TooltipLine(Mod, "", ""));

        if (lightPetStats.TryGetValue(item.type, out var statLightPet))
            tooltips.Add(new TooltipLine(Mod, "", ""));

        if (minecartStats.TryGetValue(item.type, out var statMinecart))
            tooltips.Add(new TooltipLine(Mod, "", ""));

        if (mountStats.TryGetValue(item.type, out var statMount))
            tooltips.Add(new TooltipLine(Mod, "", ""));
    }

    internal static void AddTooltipRange(List<TooltipLine> tooltips, string insertAfter, params LocalizedText[] lines)
    {

    }

    public class StatLine
    {
        public string name;
        public LocalizedText value;
    }

    public class WingStats
    {
        public LocalizedText negatesFallDamage;
        public LocalizedText flightTime;
        public LocalizedText flightHeight;
        public LocalizedText horizontalSpeed;
        public LocalizedText verticalMultiplier;

        public LocalizedText misc;
    }

    public class HookStats
    {
        public LocalizedText reach;
        public LocalizedText velocity;
        public LocalizedText numHooks;
        public LocalizedText latchingMode;

        public LocalizedText misc;
    }

    public class LighPetStats
    {
        public LocalizedText brightness;

        public LocalizedText misc;
    }

    public class MinecartStats
    {
        public LocalizedText misc;
    }

    public class MountStats
    {
        public LocalizedText misc;
    }
}
