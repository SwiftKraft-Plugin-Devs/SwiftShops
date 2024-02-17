using PluginAPI.Core;

namespace SwiftShops.API
{
    public class RegularShopItem : InventoryShopItem
    {
        public ItemType Item;

        public override bool Effect(Player p, out string output)
        {
            bool status = base.Effect(p, out output);
            if (status)
                p.AddItem(Item);
            return status;
        }

        public override string GetDisplayName() => Translations.Get(Item);
    }
}
