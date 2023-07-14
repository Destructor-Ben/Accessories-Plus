namespace AccessoriesPlus.Content.StatTooltips;
internal class LightPetStats : Stats
{
    public float Brightness { get; private set; } = -1f;
    public bool Controllable { get; private set; } = false;
    public bool ExposesTreasure { get; private set; } = false;
    public bool ExposesEnemies { get; private set; } = false;

    public static Dictionary<int, LightPetStats> VanillaLightPetStats { get; private set; } = new()
    {
        { ItemID.ShadowOrb,                 new LightPetStats(0.65f, controllable: true) },
        { ItemID.CrimsonHeart,              new LightPetStats(0.65f) },
        { ItemID.MagicLantern,              new LightPetStats(0.65f, exposesTreasure: true) },
        { ItemID.FairyBell,                 new LightPetStats(0.8f) },
        { ItemID.DD2PetGhost,               new LightPetStats(0.8f) },
        { ItemID.WispinaBottle,             new LightPetStats(1.2f, controllable: true) },
        { ItemID.SuspiciousLookingTentacle, new LightPetStats(1.2f, exposesTreasure: true, exposesEnemies: true) },
        { ItemID.PumpkingPetItem,           new LightPetStats(0.8f) },
        { ItemID.GolemPetItem,              new LightPetStats(0.8f) },
        { ItemID.FairyQueenPetItem,         new LightPetStats(1.2f) },
    };

    private LightPetStats(float brightness = -1f, bool controllable = false, bool exposesTreasure = false, bool exposesEnemies = false, string misc = "") : base(misc)
    {
        Brightness = brightness;
        Controllable = controllable;
        ExposesTreasure = exposesTreasure;
        ExposesEnemies = exposesEnemies;
    }

    // TODO: if something is a light pet but doesn't have stats instead of saying unknown brightness it will say nothing
    public static LightPetStats Get(Item item)
    {
        return VanillaLightPetStats.TryGetOrGiven(item.type, null);
    }
}
