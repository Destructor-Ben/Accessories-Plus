using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AccessoriesPlus
{
    public class APModPlayer : ModPlayer
    {
        public bool betterLA;
		public bool betterMD;

		public override void ResetEffects()
		{
			betterLA = false;
			betterMD = false;
		}

		public override void UpdateEquips()
		{
			if (Player.accCritterGuide && ModContent.GetInstance<APServerConfig>().betterLifeformAnalyzer)
				betterLA = true;
				//Player.accCritterGuide = false;
			if (Player.accOreFinder && ModContent.GetInstance<APServerConfig>().betterMetalDetector)
				betterMD = true;
				//Player.accOreFinder = false;
		}
	}
}
