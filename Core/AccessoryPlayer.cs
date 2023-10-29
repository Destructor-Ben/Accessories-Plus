namespace AccessoriesPlus.Core;

public partial class AccessoryPlayer : ModPlayer
{
    public override void PostUpdateMiscEffects()
    {
        ApplyInfoHighlights();
    }
}
