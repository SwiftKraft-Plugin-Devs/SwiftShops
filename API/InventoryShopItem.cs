using PluginAPI.Core;

namespace SwiftShops.API
{
    public abstract class InventoryShopItem : ShopItem
    {
        public override bool Effect(Player p, out string output)
        {
            if (p.IsInventoryFull)
            {
                output = "Inventory full! ";
                return false;
            }
            output = "Purchased " + GetDisplayName();
            return true;
        }
    }
}
