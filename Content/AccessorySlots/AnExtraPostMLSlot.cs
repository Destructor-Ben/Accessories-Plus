using Terraria;
using Terraria.ModLoader;

namespace AccessoriesPlus.Content.AccessorySlots;
internal class AnExtraPostMLSlot : ModAccessorySlot
{
    public override bool IsEnabled()
    {
        return Main.hardMode && NPC.downedMoonlord;
    }
}
