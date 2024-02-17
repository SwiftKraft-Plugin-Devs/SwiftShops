using PluginAPI.Core;
using SwiftAPI.API.CustomItems;

namespace SwiftShops.API
{
    public class CustomShopItem : InventoryShopItem
    {
        public CustomItemBase Item;

        public override bool Effect(Player p, out string output)
        {
            bool status = base.Effect(p, out output);
            if (status)
                p.GiveCustomItem(Item);
            return status;
        }

        public override string GetDisplayName() => Item.DisplayName;
    }
}
