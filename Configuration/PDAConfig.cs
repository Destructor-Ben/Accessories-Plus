using Terraria.ModLoader.Config;
using TerraUtil.Configuration;

namespace AccessoriesPlus.Configuration;
internal class PDAConfig : SubConfig
{
    [Header("MetalDetector")]
    public bool MetalDetectorDistanceInfo = true;
    public bool MetalDetectorArrows = true;
    public bool MetalDetectorHighlight = true;

    [ReloadRequired]
    public bool TrackGems = true;
    [ReloadRequired]
    public bool TrackHellstone = true;
    // TODO: add whitelist and blacklist for metal detector when TileDefinition finally comes around

    [Header("Radar")]
    public bool RadarHighlight = true;

    [Header("LifeformAnalyzer")]
    public bool LifeformAnalyzerDistanceInfo = true;
    public bool LifeformAnalyzerArrows = true;
    public bool LifeformAnalyzerHighlight = true;

    public bool UseNPCWhitelist = true;
    public bool UseNPCBlacklist = true;
    //public Dictionary<NPCDefinition, int> NPCWhitelist = new();
    // TODO: add rarities to NPC whitelist
    public List<NPCDefinition> NPCWhitelist = new();
    public List<NPCDefinition> NPCBlacklist = new();
}