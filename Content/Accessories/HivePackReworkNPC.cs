using Terraria;
using Terraria.ModLoader;

namespace AccessoriesPlus.Content.Accessories;
internal class HivePackReworkNPC : GlobalNPC
{
    public override bool PreAI(NPC npc)
    {
        npc.friendly = Main.player[npc.target].strongBees;
        return true;
    }
}