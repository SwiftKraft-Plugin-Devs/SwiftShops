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
    }
}
