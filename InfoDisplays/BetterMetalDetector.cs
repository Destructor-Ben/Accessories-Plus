using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AccessoriesPlus.InfoDisplays
{
    public class BetterMetalDetector : InfoDisplay
    {
		public override void SetStaticDefaults()
		{
			InfoName.SetDefault("Treasure");
		}

		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<APModPlayer>().betterMD;
		}

		public override string DisplayValue()
		{
			return "unfinished";
		}
	}
}
