using CommandSystem;
using PluginAPI.Core;
using SwiftAPI.Commands;
using SwiftAPI.Utility.Targeters;
using SwiftShops.API;
using System.Collections.Generic;

namespace SwiftShops.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SubtractBalance : CommandBase
    {
        public override string[] GetAliases() => ["rbal"];

        public override string GetCommandName() => "removebalance";

        public override string GetDescription() => "Subtracts money to balances selected players.";

        public override PlayerPermissions[] GetPerms() => [PlayerPermissions.PlayersManagement];

        public override bool Function(string[] args, ICommandSender sender, out string result)
        {
            if (!TryGetArgument(args, 1, out string arg1) || !float.TryParse(arg1, out float amount))
            {
                result = "Please input a number! ";

                return false;
            }

            List<Player> players = Player.GetPlayers();

            if (TryGetArgument(args, 2, out string arg2))
            {
                if (TargeterManager.TryGetTargeter(arg2, out TargeterBase targ))
                    players = targ.GetPlayers();
                else if (Player.TryGet(arg2, out Player p))
                    players = [p];
            }

            players.RemoveAll((p) => string.IsNullOrEmpty(p.Nickname));

            foreach (Player p in players)
                p.SetBalance(p.GetBalance() - amount);

            result = "Removed $" + amount + " from " + players.Count + " player(s). ";

            return true;
        }
    }
}
