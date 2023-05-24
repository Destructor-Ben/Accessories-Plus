using Terraria.GameContent;

namespace AccessoriesPlus.Content.ImprovedAccessories;
internal class AccessoryItem : GlobalItem
{
    public static Asset<Texture2D> OriginalBOBTexture;
    public static Asset<Texture2D> OriginalBOBBalloonTexture;
    public static Asset<Texture2D> OriginalBOHBTexture;
    public static Asset<Texture2D> OriginalBOHBBalloonTexture;

    public override void SetStaticDefaults()
    {
        // Texture replacing the bundle of balloons
        OriginalBOBTexture = TextureAssets.Item[ItemID.BundleofBalloons];
        OriginalBOBBalloonTexture = TextureAssets.Item[ItemID.HorseshoeBundle];
        OriginalBOHBTexture = TextureAssets.AccBalloon[ArmorIDs.Balloon.BundleofBalloons];
        OriginalBOHBBalloonTexture = TextureAssets.AccBalloon[ArmorIDs.Balloon.HorseshoeBundle];

        if (Config.Instance.ImprovedHorseshoeBundle)
        {
            TextureAssets.Item[ItemID.BundleofBalloons] = Util.RequestTex("BundleofBalloons");
            TextureAssets.Item[ItemID.HorseshoeBundle] = Util.RequestTex("BundleofHorseshoeBalloons");
            TextureAssets.AccBalloon[ArmorIDs.Balloon.BundleofBalloons] = Util.RequestTex("BundleofBalloons_Balloon");
            TextureAssets.AccBalloon[ArmorIDs.Balloon.HorseshoeBundle] = Util.RequestTex("BundleofHorseshoeBalloons_Balloon");
        }
    }

    public override void Unload()
    {
        // Undoing texture replacement
        TextureAssets.Item[ItemID.BundleofBalloons] = OriginalBOBTexture;
        TextureAssets.Item[ItemID.HorseshoeBundle] = OriginalBOBBalloonTexture;
        TextureAssets.AccBalloon[ArmorIDs.Balloon.BundleofBalloons] = OriginalBOHBTexture;
        TextureAssets.AccBalloon[ArmorIDs.Balloon.HorseshoeBundle] = OriginalBOHBBalloonTexture;

        OriginalBOBTexture = null;
        OriginalBOBBalloonTexture = null;
        OriginalBOHBTexture = null;
        OriginalBOHBBalloonTexture = null;
    }

    // Changing default values
    public override void SetDefaults(Item entity)
    {
        switch (entity.type)
        {
            case ItemID.AnkhShield:
                if (Config.Instance.ImprovedAnkhShield)
                    entity.defense = 12;
                break;
            default:
                break;
        }
    }

    // TODO - some way to prevent an accessorys effects from being applied

    // Accessory effects
    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        switch (item.type)
        {
            case ItemID.HandOfCreation:
                // Hand of creation
                if (Config.Instance.ImprovedHandOfCreation)
                {
                    player.CopyVanillaEquipEffects(ItemID.Toolbelt, hideVisual);
                    player.CopyVanillaEquipEffects(ItemID.Toolbox, hideVisual);
                }
                break;
            case ItemID.TerrasparkBoots:
                // Terraspark boots
                if (Config.Instance.ImprovedTerrasparkBoots)
                {
                    player.CopyVanillaEquipEffects(ItemID.AmphibianBoots, hideVisual);
                }
                break;
            case ItemID.AnkhShield:
                // Ankh shield
                if (Config.Instance.ImprovedAnkhShield)
                {
                    player.CopyVanillaEquipEffects(ItemID.HandWarmer, hideVisual);
                    player.CopyVanillaEquipEffects(ItemID.HeroShield, hideVisual);
                    player.CopyVanillaEquipEffects(ItemID.FrozenShield, hideVisual);
                }
                break;
            case ItemID.AnkhCharm:
                // Ankh charm
                if (Config.Instance.ImprovedAnkhShield)
                {
                    player.CopyVanillaEquipEffects(ItemID.HandWarmer, hideVisual);
                }
                break;
            case ItemID.BundleofBalloons:
            case ItemID.HorseshoeBundle:
                // Bundle of horseshoe balloons
                if (Config.Instance.ImprovedHorseshoeBundle)
                {
                    player.CopyVanillaEquipEffects(ItemID.FartInABalloon, hideVisual);
                    player.CopyVanillaEquipEffects(ItemID.SharkronBalloon, hideVisual);
                }
                break;
            default:
                break;
        }
    }

    // Changing tooltips
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        switch (item.type)
        {
            case ItemID.HandOfCreation:
                // Hand of creation
                if (Config.Instance.ImprovedHandOfCreation)
                {
                    tooltips.InsertTooltips("Tooltip2", after: true, Util.GetTooltipLine("HandOfCreation0"), Util.GetTooltipLine("HandOfCreation1"));
                    tooltips.RemoveTooltips("Tooltip2");
                }
                break;
            case ItemID.TerrasparkBoots:
                // Terraspark boots
                if (Config.Instance.ImprovedTerrasparkBoots)
                {
                    tooltips.InsertTooltips("Tooltip4", after: true, Util.GetTooltipLine("TerrasparkBoots0"), Util.GetTooltipLine("TerrasparkBoots1"));
                }
                break;
            case ItemID.AnkhShield:
                // Ankh shield
                if (Config.Instance.ImprovedAnkhShield)
                {
                    tooltips.InsertTooltips("Tooltip1", after: true, Util.GetTooltipLine("AnkhShield0"), Util.GetTooltipLine("AnkhShield1"), Util.GetTooltipLine("AnkhShield2"));
                }
                break;
            case ItemID.BundleofBalloons:
            case ItemID.HorseshoeBundle:
                // Bundle of horseshoe balloons
                if (Config.Instance.ImprovedHorseshoeBundle)
                {
                    tooltips.InsertTooltips("Tooltip0", after: true, Util.GetTooltipLine("BundleOfBalloons0"));
                    tooltips.RemoveTooltips("Tooltip0");
                }
                break;
            default:
                break;
        }
    }
}
