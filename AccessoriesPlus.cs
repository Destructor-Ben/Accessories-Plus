namespace AccessoriesPlus;
internal class AccessoriesPlus : Mod
{
    public static AccessoriesPlus Instance => ModContent.GetInstance<AccessoriesPlus>();

    public override void Load()
    {
        Util.Load(this);
    }

    public override void Unload()
    {
        Util.Unload();
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        Packet.Handle(reader, whoAmI);
    }
}
