namespace AccessoriesPlus.Content.StatTooltips;
internal class LightPetStats : Stats
{
    public float Brightness { get; private set; } = -1f;
    public bool Controllable { get; private set; } = false;
    public bool ExposesTreasure { get; private set; } = false;
    public bool ExposesEnemies { get; private set; } = false;

    // TODO: stop auto format
    private static Dictionary<int, LightPetStats> VanillaLightPetStats = new()
    {
        [ItemID.ShadowOrb] = new LightPetStats { Brightness = 0.65f, Controllable = true },
        [ItemID.CrimsonHeart] = new LightPetStats { Brightness = 0.65f },
        [ItemID.MagicLantern] = new LightPetStats { Brightness = 0.65f, ExposesTreasure = true },
        [ItemID.FairyBell] = new LightPetStats { Brightness = 0.8f },
        [ItemID.DD2PetGhost] = new LightPetStats { Brightness = 0.8f },
        [ItemID.WispinaBottle] = new LightPetStats { Brightness = 1.2f, Controllable = true },
        [ItemID.SuspiciousLookingTentacle] = new LightPetStats { Brightness = 1.2f, ExposesTreasure = true, ExposesEnemies = true },
        [ItemID.PumpkingPetItem] = new LightPetStats { Brightness = 0.8f },
        [ItemID.GolemPetItem] = new LightPetStats { Brightness = 0.8f },
        [ItemID.FairyQueenPetItem] = new LightPetStats { Brightness = 1.2f },
    };

    /*
        [ItemID.ShadowOrb]                 = new LightPetStats { Brightness = 0.65f, Controllable = true },
        [ItemID.CrimsonHeart]              = new LightPetStats { Brightness = 0.65f },
        [ItemID.MagicLantern]              = new LightPetStats { Brightness = 0.65f, ExposesTreasure = true },
        [ItemID.FairyBell]                 = new LightPetStats { Brightness = 0.8f },
        [ItemID.DD2PetGhost]               = new LightPetStats { Brightness = 0.8f },
        [ItemID.WispinaBottle]             = new LightPetStats { Brightness = 1.2f, Controllable = true },
        [ItemID.SuspiciousLookingTentacle] = new LightPetStats { Brightness = 1.2f, ExposesTreasure = true, ExposesEnemies = true },
        [ItemID.PumpkingPetItem]           = new LightPetStats { Brightness = 0.8f },
        [ItemID.GolemPetItem]              = new LightPetStats { Brightness = 0.8f },
        [ItemID.FairyQueenPetItem]         = new LightPetStats { Brightness = 1.2f },
     */

    private LightPetStats() { }

    // TODO: if something is a light pet but doesn't have stats instead of saying unknown brightness it will say nothing
    public static LightPetStats Get(Item item)
    {
        return item.shoot <= ProjectileID.None || !ProjectileID.Sets.LightPet[item.shoot]
            ? null
            : VanillaLightPetStats.TryGetOrGiven(item.type, default);
    }

    public override void Apply(List<TooltipLine> tooltips)
    {
        if (!Config.Instance.StatsLightPets)
            return;

        if (Brightness != -1f)
            tooltips.Add(Util.GetTooltipLine("LightPetStats.Brightness", (int)(Brightness * 100f)));
        else
            tooltips.Add(Util.GetTooltipLine("LightPetStats.BrightnessUnknown"));

        if (Controllable)
            tooltips.Add(Util.GetTooltipLine("LightPetStats.Controllable"));

        if (ExposesEnemies)
            tooltips.Add(Util.GetTooltipLine("LightPetStats.ExposesEnemies"));

        if (ExposesTreasure)
            tooltips.Add(Util.GetTooltipLine("LightPetStats.ExposesTreasure"));
    }
}
