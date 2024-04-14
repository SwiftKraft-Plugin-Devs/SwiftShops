using CommandSystem;
using PluginAPI.Core;
using SwiftAPI.Commands;
using SwiftShops.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShops.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class TransferBalance : CommandBase
    {
        public override string[] GetAliases() => ["tr"];

        public override string GetCommandName() => "transfer";

        public override string GetDescription() => "Transfers money to a player.";

        public override PlayerPermissions[] GetPerms() => null;

        public override bool GetRequirePlayer() => true;

        public override bool PlayerBasedFunction(Player player, string[] args, out string result)
        {
            if (!TryGetArgument(args, 1, out string arg1) || !Player.TryGetByName(arg1, out Player target))
            {
                result = "Could not find player! ";

                return false;
            }

            if (!TryGetArgument(args, 2, out string arg2) || !int.TryParse(arg2, out int amount) || amount <= 0 || amount > player.GetBalance())
            {
                result = "Please input a valid amount! ";

                return false;
            }

            target.SetBalance(target.GetBalance() + amount);
            player.SetBalance(player.GetBalance() - amount);

            target.SendBroadcast(player.DisplayNickname + " Transferred Money: $" + amount, 3);

            result = "Transaction successful! ";

            return true;
        }
    }
}
