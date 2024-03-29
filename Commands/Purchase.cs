﻿using CommandSystem;
using PluginAPI.Core;
using SwiftAPI.Commands;
using SwiftShops.API;

namespace SwiftShops.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Purchase : CommandBase
    {
        public override string[] GetAliases() => ["pur"];

        public override string GetCommandName() => "purchase";

        public override string GetDescription() => "Purchases a shop item.";

        public override PlayerPermissions[] GetPerms() => null;

        public override bool GetRequirePlayer() => true;

        public override bool PlayerBasedFunction(Player player, string[] args, out string result)
        {
            if (!TryGetArgument(args, 1, out string arg1))
            {
                result = "\nYour Current Balance: <color=#FFFF00>$" + player.GetBalance() + "</color>\n\nItem List: \n";

                foreach (ShopItem item in ShopManager.GetAllItems())
                    result += "\n" + item.ToString();

                return true;
            }

            return player.Purchase(arg1, out result);
        }
    }
}
