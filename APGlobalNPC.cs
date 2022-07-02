using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using AccessoriesPlus.Items;

namespace AccessoriesPlus
{
	public class APGlobalNPC : GlobalNPC
	{
		// Modifying NPC drops
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			// Adding web slinger drop
			if ((npc.type == NPCID.BlackRecluse || npc.type == NPCID.WallCreeper) && ModContent.GetInstance<APServerConfig>().betterWebSlinger)
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.WebSlinger, 25));
			}

			// Adding hand warmer drop
			if (npc.type == NPCID.SnowFlinx && ModContent.GetInstance<APServerConfig>().betterAnkhShield)
            {
				npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
			}
		}

		// Modifying NPC shops
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.DyeTrader && ModContent.GetInstance<APServerConfig>().illuminantDye && Main.eclipse)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<IlluminantDye>());
				nextSlot++;
            }
        }
    }
}