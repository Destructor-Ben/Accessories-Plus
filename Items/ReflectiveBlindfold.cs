using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AccessoriesPlus.Items
{
	public class ReflectiveBlindfold : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reflective Blindfold");
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
			Item.faceSlot = 5;
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