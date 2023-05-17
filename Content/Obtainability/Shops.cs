namespace AccessoriesPlus.Content.Obtainability;
internal class Shops : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        // Adding toolbox to Mechanic
        if (Config.Instance.ObtainabilityPresents && shop.NpcType == NPCID.Mechanic)
            shop.Add(new NPCShop.Entry(ItemID.Toolbox));
    }

    public override void SetupTravelShop(int[] shop, ref int nextSlot)
    {
        // Adding a guaranteed accessory to the travelling merchant
        if (Config.Instance.ObtainabilityTravellingMerchant)
        {
            if (Main.rand.NextBool())
            {
                // Adding a pda accessory
                shop[nextSlot] = Main.rand.Next(3) switch
                {
                    1 => ItemID.Stopwatch,
                    2 => ItemID.LifeformAnalyzer,
                    _ => ItemID.DPSMeter,
                };
                nextSlot++;
            }
            else
            {
                // Adding an architect gizmo pack accessory
                shop[nextSlot] = Main.rand.Next(4) switch
                {
                    1 => ItemID.BrickLayer,
                    2 => ItemID.ExtendoGrip,
                    3 => ItemID.PaintSprayer,
                    _ => ItemID.PortableCementMixer,
                };
                nextSlot++;
            }
        }
    }
}
