using AccessoriesPlus.Content.ImprovedAccessories;

namespace AccessoriesPlus.Content;

public class Detours : ModSystem
{
    public override void Load()
    {
        /*/ Automatically dislodging grappling hooks
        // TODO: finish
        On_Player.GrappleMovement += delegate (On_Player.orig_GrappleMovement orig, Player self)
        {
            if (Config.Instance.AutoDislodgeGrapple)
            {
                // Vanilla code
                if (self.grappling[0] < 0)
                    return;

                self.StopVanityActions();
                if (Main.myPlayer == self.whoAmI && self.mount.Active)
                    self.mount.Dismount(self);

                // Actually disloding
                foreach (int hook in self.grappling)
                {
                    if (hook < 0)
                        continue;

                    var proj = Main.projectile[hook];
                    var pos = proj.Center.ToTileCoordinates();
                    var tile = Main.tile[pos.X, pos.Y];

                    if ((TileID.Sets.Platforms[tile.TileType] || tile.TileType == TileID.PlanterBox) && proj.Distance(self.Center) <= 24f)
                        proj.Kill();
                }
            }

            orig(self);
        };*/

        // Making lifeform analyzer have custom highlight colours
        // TODO: make rare npcs sparkle if they are highlighted as well
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
