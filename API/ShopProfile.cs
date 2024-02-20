using PluginAPI.Core;
using System.Collections.Generic;

namespace SwiftShops.API
{
    public class ShopProfile
    {
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

        public override string ToString() => ID + " | " + DisplayName + $" - Activated: {(Activated ? "<color=#00FF00>" : "<color=#FF0000>")}" + Activated + "</color>";

        public static bool Purchase(ShopItem item, Player p, out string output) => item.Purchase(p, out output);
    }
}
