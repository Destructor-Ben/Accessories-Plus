using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AccessoriesPlus.InfoDisplays
{
    public class BetterLifeformAnalyzer : InfoDisplay
    {
		public override void SetStaticDefaults()
		{
			InfoName.SetDefault("Rare Creatures");
		}

		public override bool Active()
		{
			return Main.LocalPlayer.GetModPlayer<APModPlayer>().betterLA;
		}

		public override string DisplayValue()
		{
			return "unfinished";
		}
	}
}
