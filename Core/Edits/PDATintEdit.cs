using TerraUtil.Edits;

namespace AccessoriesPlus.Core.Edits;

public class PDATintEdit : Detour
{
    public override void Apply()
    {
        On_NPC.GetNPCColorTintedByBuffs += delegate (On_NPC.orig_GetNPCColorTintedByBuffs orig, NPC self, Color npcColor)
        {
            var originalColor = orig(self, npcColor);

            if (!AccessoryInfoDisplay.LifeformAnalyzerNPCs.Contains(self) || !Util.InfoDisplayActive(InfoDisplay.LifeformAnalyzer) || !PDAConfig.Instance.LifeformAnalyzerHighlight)
                return originalColor;

            byte r = 200;
            byte g = 170;
            byte b = 0;

            if (npcColor.R < r)
                npcColor.R = r;

            if (npcColor.G < g)
                npcColor.G = g;

            if (npcColor.B < b)
                npcColor.B = b;

            return npcColor;
        };
    }
}
