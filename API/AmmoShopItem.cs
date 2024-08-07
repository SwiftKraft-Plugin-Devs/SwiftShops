﻿using PluginAPI.Core;

namespace SwiftShops.API
{
    public class AmmoShopItem : ShopItem
    {
        public ItemType Item;

        public ushort Amount;

        public override bool Effect(Player p, out string output)
        {
            if (p.GetAmmoLimit(Item) < p.GetAmmo(Item) + Amount)
            {
                output = "Ammo Limit For " + GetDisplayName() + " Reached!";
                return false;
            }

            p.AddAmmo(Item, Amount);
            output = "Purchased " + GetDisplayName();
            return true;
        }

        public override string GetDisplayName() => Item + " x" + Amount;
    }
}
