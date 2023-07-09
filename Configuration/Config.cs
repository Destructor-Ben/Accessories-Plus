﻿using System.ComponentModel;
using Terraria.ModLoader.Config;
using TerraUtil.Configuration;

#pragma warning disable CS0649
namespace AccessoriesPlus.Configuration;
internal class Config : ModConfiguration
{
    public static Config Instance => ModContent.GetInstance<Config>();
    public override ConfigScope Mode => ConfigScope.ServerSide;

    #region Improved Accessories

    [Header("ImprovedAccessories")]

    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedTerrasparkBoots;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedAnkhShield;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedHorseshoeBundle;

    [DefaultValue(true)]
    [ReloadRequired]
    public bool ImprovedHandOfCreation;
    public PDAConfig ImprovedPDA = new PDAConfig();

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
    public bool SlotMoonlord;
    [DefaultValue(true)]
    public bool SlotWings;
    [DefaultValue(true)]
    public bool SlotShield;
    [DefaultValue(true)]
    public bool SlotBoots;
    [DefaultValue(true)]
    public bool SlotForceWings;
    [DefaultValue(true)]
    public bool SlotForceShields;
    [DefaultValue(true)]
    public bool SlotForceBoots;

    #endregion

    #region Stat Tooltips

    [Header("StatTooltips")]

    [DefaultValue(true)]
    public bool StatsWings;
    [DefaultValue(true)]
    public bool StatsHooks;
    [DefaultValue(true)]
    public bool StatsLightPets;
    [DefaultValue(true)]
    public bool StatsMinecarts;
    [DefaultValue(true)]
    public bool StatsMounts;

    #endregion

    #region Obtainability

    [Header("Obtainability")]// TODO: make the config for obtainability more verbose

    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityRecipes;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityShimmer;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityNPCDrops;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityTravellingMerchant;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ObtainabilityPresents;

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
#pragma warning restore CS0649
