using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace AccessoriesPlus.Configuration;
[Label("$Mods.AccessoriesPlus.Config.DisplayName")]
internal class Config : ModConfig
{
    // TODO - implement config
    // TODO - localize config
    public static Config Instance => ModContent.GetInstance<Config>();
    public override ConfigScope Mode => ConfigScope.ServerSide;

    #region Improved Accessories
    [Header("$Mods.AccessoriesPlus.Config.Headers.ImprovedAccessories")]

    [Label("$Mods.AccessoriesPlus.Config.ImprovedTerrasparkBoots.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.ImprovedTerrasparkBoots.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedTerrasparkBoots = true;
    [Label("$Mods.AccessoriesPlus.Config.ImprovedAnkhShield.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.ImprovedAnkhShield.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedAnkhShield = true;
    [Label("$Mods.AccessoriesPlus.Config.ImprovedBOHBShield.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.ImprovedBOHBShield.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedBOHBShield = true;
    [Label("$Mods.AccessoriesPlus.Config.ImprovedHandOfCreation.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.ImprovedHandOfCreation.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedHandOfCreation = true;

    public bool ReworkedShinyStone = true;
    #endregion

    #region Improved Grappling Hooks
    [Header("$Mods.AccessoriesPlus.Config.Headers.ImprovedGrapplingHooks")]

    public bool GrapplingHooks = true;
    #endregion

    #region Improved Pets
    [Header("$Mods.AccessoriesPlus.Config.Headers.ImprovedPets")]

    public bool Pets = true;
    #endregion

    #region Improved Mounts
    [Header("$Mods.AccessoriesPlus.Config.Headers.ImprovedMounts")]

    public bool Mounts = true;
    #endregion

    #region Improved Minecarts
    [Header("$Mods.AccessoriesPlus.Config.Headers.ImprovedMinecarts")]

    public bool Minecarts = true;
    #endregion

    #region Accessory Slots
    [Header("$Mods.AccessoriesPlus.Config.Headers.AccessorySlots")]

    [Label("$Mods.AccessoriesPlus.Config.SlotMoonlord.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.SlotMoonlord.Tooltip")]
    [DefaultValue(true)]
    public bool SlotMoonlord = true;
    [Label("$Mods.AccessoriesPlus.Config.SlotWings.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.SlotWings.Tooltip")]
    [DefaultValue(true)]
    public bool SlotWings = true;
    [Label("$Mods.AccessoriesPlus.Config.SlotShield.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.SlotShield.Tooltip")]
    [DefaultValue(true)]
    public bool SlotShield = true;
    [Label("$Mods.AccessoriesPlus.Config.SlotBoots.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.SlotBoots.Tooltip")]
    [DefaultValue(true)]
    public bool SlotBoots = true;
    [Label("$Mods.AccessoriesPlus.Config.SlotForceWings.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.SlotForceWings.Tooltip")]
    [DefaultValue(true)]
    public bool SlotForceWings = true;
    [Label("$Mods.AccessoriesPlus.Config.SlotForceShields.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.SlotForceShields.Tooltip")]
    [DefaultValue(true)]
    public bool SlotForceShields = true;
    [Label("$Mods.AccessoriesPlus.Config.SlotForceBoots.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.SlotForceBoots.Tooltip")]
    [DefaultValue(true)]
    public bool SlotForceBoots = true;
    #endregion

    #region Stat Tooltips
    [Header("$Mods.AccessoriesPlus.Config.Headers.StatTooltips")]

    [Label("$Mods.AccessoriesPlus.Config.StatsWings.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.StatsWings.Tooltip")]
    [DefaultValue(true)]
    public bool StatsWings = true;
    [Label("$Mods.AccessoriesPlus.Config.StatsHooks.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.StatsHooks.Tooltip")]
    [DefaultValue(true)]
    public bool StatsHooks = true;
    [Label("$Mods.AccessoriesPlus.Config.StatsLightPets.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.StatsLightPets.Tooltip")]
    [DefaultValue(true)]
    public bool StatsLightPets = true;
    [Label("$Mods.AccessoriesPlus.Config.StatsMinecarts.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.StatsMinecarts.Tooltip")]
    [DefaultValue(true)]
    public bool StatsMinecarts = true;
    [Label("$Mods.AccessoriesPlus.Config.StatsMounts.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.StatsMounts.Tooltip")]
    [DefaultValue(true)]
    public bool StatsMounts = true;
    #endregion

    #region Obtainability
    [Header("$Mods.AccessoriesPlus.Config.Headers.Obtainability")]

    [Label("$Mods.AccessoriesPlus.Config.ObtainabilityRecipes.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.ObtainabilityRecipes.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabiltyRecipes = true;
    [Label("$Mods.AccessoriesPlus.Config.ObtainabilityShimmer.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.ObtainabilityShimmer.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityShimmer = true;
    [Label("$Mods.AccessoriesPlus.Config.ObtainabilityTravellingMerchant.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.ObtainabilityTravellingMerchant.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityTravellingMerchant = true;
    [Label("$Mods.AccessoriesPlus.Config.ObtainabilityPresents.Label")]
    [Tooltip("$Mods.AccessoriesPlus.Config.ObtainabilityPresents.Tooltip")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityPresents = true;
    #endregion
}
