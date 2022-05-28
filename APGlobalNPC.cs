using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AccessoriesPlus
{
	public class APGlobalNPC : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			// Adding web slinger drop
			if (npc.type == NPCID.BlackRecluse || npc.type == NPCID.WallCreeper)
			{
				npcLoot.Add(ItemDropRule.Common(ItemID.WebSlinger, 25));
			}

			// Adding hand warmer drop
			if (npc.type == NPCID.ZombieEskimo || npc.type == NPCID.ZombieMerman)
            {
				npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 2));
			}
		}
	}
}