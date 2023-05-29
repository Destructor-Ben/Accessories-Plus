namespace AccessoriesPlus.Content.ImprovedAccessories;
internal class AccessoryPlayer : ModPlayer
{
    /*/ TODO - finish shiny stone rework
    public bool MaxLife => Player.statLife == Player.statLifeMax2;
    public bool FullyChargedShinyStone;
    public bool JustHurt;
    public float ShinyStoneCharge => MathHelper.Clamp(ShinyStoneChargeCounter, 0f, ShinyStoneChargeCounterMax) / ShinyStoneChargeCounterMax;
    public int ShinyStoneChargeCounter = 0;
    public int ShinyStoneChargeCounterMax = 120;
    public int LifeRegenCounter = 0;

    public override void ResetEffects()
    {
        JustHurt = false;
        FullyChargedShinyStone = MaxLife && ShinyStoneChargeCounter >= ShinyStoneChargeCounterMax;
    }

    public override void UpdateLifeRegen()
    {
        if (Player.shinyStone)
        {
            // TODO regen effects
            // Increment life regen counter
            //LifeRegenCounter++;
            //Player.lifeRegenCount += LifeRegenCounter;
        }
    }

    public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
        ModifyHit(ref modifiers);
    }

    public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
        ModifyHit(ref modifiers);
    }

    public void ModifyHit(ref Player.HurtModifiers modifiers)
    {
        // If the stone is charged then half damage
        if (FullyChargedShinyStone)
        {
            modifiers.FinalDamage *= 0.5f;
            JustHurt = true;
        }
    }

    public override void PostHurt(Player.HurtInfo info)
    {
        if (FullyChargedShinyStone)
        {
            // Add immune time
            Player.AddImmuneTime(info.CooldownCounter, 90);

            // Reset counters
            ShinyStoneChargeCounter = 0;
            LifeRegenCounter = 0;

            // Visual effects
            for (int i = 0; i < 30; i++)
            {
                var velocity = Main.rand.NextVector2Circular(5f, 5f);
                Dust.NewDustPerfect(Player.Center, DustID.Pixie, velocity);
            }
        }
    }

    public override void PostUpdate()
    {
        // Adding light
        if (FullyChargedShinyStone)
        {
            Lighting.AddLight(Player.Center, MathHelper.Lerp(0f, 0.9f, ShinyStoneCharge), MathHelper.Lerp(0f, 0.7f, ShinyStoneCharge), MathHelper.Lerp(0f, 0.1f, ShinyStoneCharge));
        }

        // Increasing counters
        if (MaxLife)
            ShinyStoneChargeCounter++;
    }

    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        // Colouring the player if the stone is fully charged
        if (FullyChargedShinyStone)
        {
            r = MathHelper.Lerp(r, 0.9f, ShinyStoneCharge);
            g = MathHelper.Lerp(r, 0.7f, ShinyStoneCharge);
            b = MathHelper.Lerp(r, 0.1f, ShinyStoneCharge);
        }
    }*/
}
