using Terraria.GameContent.ItemDropRules;

namespace AccessoriesPlus.Core;

public class ObtainabilityNPC : GlobalNPC
{
    private static List<int> travellingMerchantItems = new()
    {
        ItemID.Stopwatch,
        ItemID.LifeformAnalyzer,
        ItemID.DPSMeter,
        ItemID.BrickLayer,
        ItemID.ExtendoGrip,
        ItemID.PaintSprayer,
        ItemID.PortableCementMixer,
        ItemID.ActuationAccessory,
    };
    private static List<int> ankhShieldItems = new()
    {
        ItemID.ArmorPolish,
        ItemID.Vitamins,
        ItemID.Bezoar,
        ItemID.AdhesiveBandage,
        ItemID.Megaphone,
        ItemID.Nazar,
        ItemID.TrifoldMap,
        ItemID.FastClock,
        ItemID.Blindfold,
        ItemID.PocketMirror,
    };

    // Modifying NPC shops
    public override void ModifyShop(NPCShop shop)
    {
        // Adding toolbox to Mechanic
        if (Config.Instance.ObtainabilityPresents && shop.NpcType == NPCID.Mechanic)
            shop.Add(new NPCShop.Entry(ItemID.Toolbox));
    }

    // Adding a guaranteed accessory to the travelling merchant
    public override void SetupTravelShop(int[] shop, ref int nextSlot)
    {
        if (Config.Instance.ObtainabilityTravellingMerchant)
        {
            int attempts = 0;
            while (attempts < 100)
            {
                attempts++;

                int itemType = travellingMerchantItems[Main.rand.Next(travellingMerchantItems.Count)];
                if (shop.Contains(itemType))
                    continue;

                shop[nextSlot] = itemType;
                nextSlot++;
                break;
            }
        }
    }

    // Modifying NPC loot
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        // Making ankh shield drops more common
        // TODO: this is bugged
        if (Config.Instance.ObtainabilityNPCDrops)
        {
            var rules = npcLoot.Get().Where(r => r is DropBasedOnExpertMode drop && drop.ruleForNormalMode is CommonDrop drop2 && drop.ruleForExpertMode is CommonDrop drop3 && ankhShieldItems.Contains(drop2.itemId));
            foreach (var rule in rules)
            {
                // Modifying existing drop
                var drop = rule as DropBasedOnExpertMode;
                var n = (CommonDrop)drop.ruleForNormalMode;
                var e = (CommonDrop)drop.ruleForExpertMode;

                n.chanceNumerator = 3;

                // Since expert is weird, it changes the number of times rolled instead of the denominator/numerator
                e.chanceNumerator = 6;

                if (e is CommonDropWithRerolls reroll)
                    reroll.timesToRoll = 1;
            }
        }
    }
}
