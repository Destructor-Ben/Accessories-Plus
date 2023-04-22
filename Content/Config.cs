using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Content;
[Label("$Mods.AccessoriesPlus.Config.Name")]
public class Config : ModConfig
{
    public static Config Instance => ModContent.GetInstance<Config>();
    public override ConfigScope Mode => ConfigScope.ServerSide;

    // Accessory slots
    public bool mlSlot;
    public bool wingSlot;
    public bool shieldSlot;
    public bool bootSlot;
    public bool forceWingSlot;
    public bool forceShieldSlot;
    public bool forceBootSlot;

    // Stat tooltips
    public bool wingStats;
    public bool hookStats;
    public bool lightPetStats;
    public bool minecartStats;
    public bool mountStats;
}
