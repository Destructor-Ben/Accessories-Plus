using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace AccessoriesPlus.Items
{
	public class MagicMitten : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Mitten");
			Tooltip.SetDefault("Immunity to Frozen and Chilled\nReduces the cooldown of healing potions by 25%");
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
			Item.handOffSlot = 2;
			Item.handOnSlot = 7;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HandWarmer)
				.AddIngredient(ItemID.PhilosophersStone)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.buffImmune[46] = true;
			player.buffImmune[47] = true;
			player.pStone = true;
		}
    }
}