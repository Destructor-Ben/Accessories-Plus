using MonoMod.Cil;

namespace AccessoriesPlus.Content;
internal class ILEdits : ModSystem
{
    public override void Load()
    {
        // Disabling vanilla shiny stone effects
        IL_Player.UpdateLifeRegen += delegate (ILContext il)
        {
            return;
            try
            {
                var c = new ILCursor(il);

                while (c.TryGotoNext(MoveType.After, i => i.MatchLdfld(typeof(Player).GetField(nameof(Player.shinyStone)))))
                {
                    c.EmitDelegate(delegate (bool shinyStoneActive)
                    {
                        return !Config.Instance.ReworkedShinyStone && shinyStoneActive;
                    });
                }
            }
            catch (Exception e)
            {
                throw new ILPatchFailureException(Mod, il, e);
            }
        };

        // Allowing mechanical, informational, and building accessories to be in vanity slots, regular slots, or inventory to function
    }
}
