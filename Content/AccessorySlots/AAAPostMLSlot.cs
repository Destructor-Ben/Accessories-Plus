namespace AccessoriesPlus.Content.AccessorySlots;
// Triple A so it gets placed before other slots
internal class AAAPostMLSlot : ModAccessorySlot
{
    public override bool IsEnabled()
    {
        return Config.Instance.SlotMoonlord && Main.hardMode && NPC.downedMoonlord;
    }
}
