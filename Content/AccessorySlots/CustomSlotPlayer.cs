namespace AccessoriesPlus.Content.AccessorySlots;
internal class CustomSlotPlayer : ModPlayer
{
    public override void UpdateEquips()
    {
        InfoAccsUpdate();
    }

    public override void ResetInfoAccessories()
    {
        InfoAccsUpdate();
    }

    // Updating in inventory and vanity slots
    public void InfoAccsUpdate()
    {
        return;// TODO: temporary
        // Vanity slots
        for (int i = 13; i < 20; i++)
        {
            if (Player.IsItemSlotUnlockedAndUsable(i))
                Player.RefreshInfoAccsFromItemType(Player.armor[i]);
        }

        // Inventory
        foreach (var item in Player.inventory)
        {
            Player.RefreshInfoAccsFromItemType(item);
        }

        // Void bag
        foreach (var item in Player.bank4.item)
        {
            Player.RefreshInfoAccsFromItemType(item);
        }
    }

    // TODO: Making other players share their mechanical accessories and building accessories effects
    public override void RefreshInfoAccessoriesFromTeamPlayers(Player otherPlayer)
    {
        base.RefreshInfoAccessoriesFromTeamPlayers(otherPlayer);
    }
}
