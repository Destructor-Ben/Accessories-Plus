using System;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;

namespace AccessoriesPlus.Content.Accessories.Reworks;
internal class PetRework : ModSystem
{
    static Player UICharacter_player(UICharacter self)
    {
        return (Player)typeof(UICharacter).GetField("_player", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
    }

    static Projectile[] UICharacter_petProjectiles(UICharacter self)
    {
        return (Projectile[])typeof(UICharacter).GetField("_petProjectiles", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(self);
    }

    public override void Load()
    {
        // Resplendent dessert in chacrater select TODO
        On_UICharacter.PreparePetProjectiles += delegate (On_UICharacter.orig_PreparePetProjectiles orig, UICharacter self)
        {
            orig(self);
            if (!UICharacter_player(self).hideMisc[0])
            {
                var petProjectiles = UICharacter_petProjectiles(self);
                var item = UICharacter_player(self).miscEquips[0];

                if (petProjectiles.Length > 0 && item.type == ItemID.ResplendentDessert)
                {
                    var dummy = new Projectile();
                    dummy.SetDefaults(ProjectileID.BerniePet);
                    dummy.isAPreviewDummy = true;

                    Array.Resize(ref petProjectiles, petProjectiles.Length + 1);
                    petProjectiles[^1] = dummy;
                }
            }
        };
    }
}
