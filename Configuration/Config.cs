using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Configuration;
internal class Config : ModConfig
{
    public static Config Instance => ModContent.GetInstance<Config>();
    public override ConfigScope Mode => ConfigScope.ServerSide;

    #region Improved Accessories
    [Header("ImprovedAccessories")]

    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedTerrasparkBoots = true;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedAnkhShield = true;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedHorseshoeBundle = true;

    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedHandOfCreation = true;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedPDA = true;

    //public bool ReworkedShinyStone = true;
    #endregion

    #region Improved Grappling Hooks
    //[Header("ImprovedGrapplingHooks")]

    //public bool AutoDislodgeGrapple = false;
    #endregion

    #region Improved Pets
    //[Header("ImprovedPets")]
    #endregion

    #region Improved Mounts
    //[Header("ImprovedMounts")]
    #endregion

    #region Improved Minecarts
    //[Header("ImprovedMinecarts")]
    #endregion

    #region Accessory Slots
    [Header("AccessorySlots")]

    [DefaultValue(true)]
    public bool SlotMoonlord = true;
    [DefaultValue(true)]
    public bool SlotWings = true;
    [DefaultValue(true)]
    public bool SlotShield = true;
    [DefaultValue(true)]
    public bool SlotBoots = true;
    [DefaultValue(true)]
    public bool SlotForceWings = true;
    [DefaultValue(true)]
    public bool SlotForceShields = true;
    [DefaultValue(true)]
    public bool SlotForceBoots = true;
    #endregion

    #region Stat Tooltips
    [Header("StatTooltips")]

    [DefaultValue(true)]
    public bool StatsWings = true;
    [DefaultValue(true)]
    public bool StatsHooks = true;
    [DefaultValue(true)]
    public bool StatsLightPets = true;
    [DefaultValue(true)]
    public bool StatsMinecarts = true;
    [DefaultValue(true)]
    public bool StatsMounts = true;
    #endregion

    #region Obtainability
    [Header("Obtainability")]

    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityRecipes = true;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityShimmer = true;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityNPCDrops = true;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityTravellingMerchant = true;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityPresents = true;
    #endregion

    /* Descriptions to be added later
    Ingame description
    [c/c78fff:Improved Grappling Hooks]
    - A

    [c/c78fff:Improved Pets]
    - A

    [c/c78fff:Improved Mounts]
    - A

    [c/c78fff:Improved Minecarts]
    - A

    Steam description
    [b]Improved Grappling Hooks[/b]
    [list]
    [*]A
    [/list]

    [b]Improved Pets[/b]
    [list]
    [*]A
    [/list]

    [b]Improved Mounts[/b]
    [list]
    [*]A
    [/list]

    [b]Improved Minecarts[/b]
    [list]
    [*]A
    [/list]
    */
}
