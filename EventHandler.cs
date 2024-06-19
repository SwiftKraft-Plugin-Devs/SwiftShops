using Hints;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using SwiftShops.API;

namespace SwiftShops
{
    public class EventHandler
    {
        [PluginEvent(ServerEventType.PlayerLeft)]
        public void PlayerLeft(PlayerLeftEvent _event)
        {
            if (ShopManager.Balance.ContainsKey(_event.Player))
                ShopManager.Balance.Remove(_event.Player);
        }

        [PluginEvent(ServerEventType.PlayerSearchedPickup)]
        public bool PlayerSearchedPickup(PlayerSearchedPickupEvent _event)
        {
            if (!ShopProfile.WorldItems.ContainsKey(_event.Item.Info.Serial))
                return true;

            ShopProfile.WorldShopItem item = ShopProfile.WorldItems[_event.Item.Info.Serial];

            bool status = item.Purchase(_event.Player, out string output);
            _event.Player.ReceiveHint(output + "\nYour Balance: " + _event.Player.GetBalance(), [HintEffectPresets.FadeOut()]);
            return false;
        }

        [PluginEvent(ServerEventType.PlayerSearchPickup)]
        public void PlayerSearchPickup(PlayerSearchPickupEvent _event)
        {
            if (!ShopProfile.WorldItems.ContainsKey(_event.Item.Info.Serial))
                return;

            ShopProfile.WorldShopItem item = ShopProfile.WorldItems[_event.Item.Info.Serial];
            _event.Player.ReceiveHint("Trying to Purchase: " + item.Item.GetDisplayName() + "\nCost: $" + item.Item.Price + "\nYour Balance: " + _event.Player.GetBalance(), [HintEffectPresets.FadeOut()]);
        }
    }
}
