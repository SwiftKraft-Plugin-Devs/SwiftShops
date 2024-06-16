using PluginAPI.Core;
using PluginAPI.Core.Items;
using System.Collections.Generic;
using UnityEngine;

namespace SwiftShops.API
{
    public class ShopProfile
    {
        public static readonly Dictionary<ushort, WorldShopItem> WorldItems = [];

        public string ID;
        public string DisplayName;

        public readonly Dictionary<string, ShopItem> Items = [];

        public bool Activated;

        public ShopItem GetItem(string id)
        {
            if (HasItem(id))
                return Items[id];
            return null;
        }

        public bool TryGetItem(string id, out ShopItem output)
        {
            output = GetItem(id);
            return output != null;
        }

        public ShopItem[] GetAllItems() => [.. Items.Values];

        public bool AddItem(ShopItem item)
        {
            if (HasItem(item.ID))
                return false;
            Items.Add(item.ID, item);
            return true;
        }

        public void RemoveItem(ShopItem item) => Items.Remove(item.ID);

        public void RemoveItem(string id) => Items.Remove(id);

        public void ClearItems() => Items.Clear();

        public bool HasItem(string id) => Items.ContainsKey(id);

        public bool HasItem(ShopItem item) => Items.ContainsValue(item);

        public bool Purchase(string id, Player p, out string output)
        {
            if (TryGetItem(id, out ShopItem item))
                return Purchase(item, p, out output);
            output = ShopManager.GetFailString(id);
            return false;
        }

        public override string ToString() => ID + " | " + DisplayName + " - Activated: " + Activated;

        public static bool Purchase(ShopItem item, Player p, out string output) => item.Purchase(p, out output);

        public static ItemPickup CreateWorldItem(ShopItem item, ItemType droppedItem, Vector3 position, Quaternion rotation, int maxUses = 0)
        {
            ItemPickup pickup = ItemPickup.Create(droppedItem, position, rotation);
            WorldItems.Add(pickup.Serial, new(item, pickup, maxUses));
            pickup.Spawn();
            return pickup;
        }

        public class WorldShopItem(ShopItem item, ItemPickup worldItem, int maxUses)
        {
            public readonly int MaxUses = maxUses;
            public int CurrentUses { get; private set; } = maxUses;

            public readonly ShopItem Item = item;

            public readonly ItemPickup WorldItem = worldItem;

            public bool CanPurchase() => MaxUses <= 0 || CurrentUses < MaxUses;

            public bool Purchase(Player p, out string output)
            {
                if (!CanPurchase())
                {
                    WorldItem.Destroy();
                    output = "Max Uses Reached! ";
                    return false;
                }

                bool purchase = Item.Purchase(p, out output);

                if (purchase)
                {
                    if (MaxUses > 0)
                        output += "\nUses: " + ++CurrentUses + "/" + MaxUses;

                    if (!CanPurchase())
                        WorldItem.Destroy();
                }

                return purchase;
            }
        }
    }
}
