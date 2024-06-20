using Hints;
using InventorySystem;
using InventorySystem.Items.Pickups;
using MEC;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using SwiftShops.API;
using UnityEngine;

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
        public void PlayerSearchedPickup(PlayerSearchedPickupEvent _event)
        {
            if (!ShopProfile.WorldItems.ContainsKey(_event.Item.Info.Serial))
                return;

            Vector3 pos = _event.Item.Position;
            Quaternion rot = _event.Item.Rotation;

            ShopProfile.WorldShopItem item = ShopProfile.WorldItems[_event.Item.Info.Serial];

            bool status = item.Purchase(_event.Player, out string output);
            _event.Player.ReceiveHint(output + "\nYour Balance: " + _event.Player.GetBalance(), [HintEffectPresets.FadeOut()]);
            Timing.CallDelayed(Timing.WaitForOneFrame, () =>
            {
                ItemPickupBase it = _event.Player.ReferenceHub.inventory.ServerDropItem(_event.Item.Info.Serial);
                it.Position = pos;
                it.Rotation = rot;
            });
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
