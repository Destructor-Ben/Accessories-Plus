using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace AccessoriesPlus.Items
{
	public class IlluminantDye : ModItem
	{
		// If illuminant dye is enabled
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModContent.GetInstance<APServerConfig>().illuminantDye;
		}

		public override void SetStaticDefaults()
		{
			// Dye shader
			if (!Main.dedServ)
			{
				GameShaders.Armor.BindShader(Item.type, new ArmorShaderData(new Ref<Effect>(Mod.Assets.Request<Effect>("Effects/IlluminantDye", AssetRequestMode.ImmediateLoad).Value), "IlluminantDyePass"));
			}

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}

		public override void SetDefaults()
		{
			int dye = Item.dye;
			Item.CloneDefaults(ItemID.BloodbathDye);
			Item.dye = dye;
		}
	}
}