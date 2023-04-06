using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AccessoriesPlus.Content.Accessories;
internal class HivePackRework : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation)
    {
        return entity.type == ItemID.HiveBackpack;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {

    }
}
