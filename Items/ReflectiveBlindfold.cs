using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AccessoriesPlus.Items
{
	public class ReflectiveBlindfold : ModItem
	{
		// If better ankh shield is enabled
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<APServerConfig>().betterAnkhShield;
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blinding Mirror");
			Tooltip.SetDefault("Immunity to Darkness and Stoned");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
			Item.maxStack = 1;
			Item.value = Item.sellPrice(gold: 2);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Blindfold)
				.AddIngredient(ItemID.PocketMirror)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.buffImmune[22] = true;
			player.buffImmune[156] = true;
		}
    }
}