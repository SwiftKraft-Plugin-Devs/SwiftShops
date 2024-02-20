using CommandSystem;
using PluginAPI.Core;
using SwiftAPI.Commands;
using SwiftAPI.Utility.Targeters;
using SwiftShops.API;
using System.Collections.Generic;

namespace SwiftShops.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ListBalance : CommandBase
    {
        public override string[] GetAliases() => ["lbal"];

        public override string GetCommandName() => "listbalance";

        public override string GetDescription() => "Lists balances of selected players.";

        public override PlayerPermissions[] GetPerms() => [PlayerPermissions.PlayersManagement];

        public override bool Function(string[] args, ICommandSender sender, out string result)
        {
            result = "Players: \n";

            List<Player> players = Player.GetPlayers();

            if (TryGetArgument(args, 1, out string arg1))
            {
                if (TargeterManager.TryGetTargeter(arg1, out TargeterBase targ))
                    players = targ.GetPlayers();
                else if (Player.TryGet(arg1, out Player p))
                    players = [p];
            }

            players.RemoveAll((p) => string.IsNullOrEmpty(p.Nickname));

            foreach (Player p in players)
                result += "\n - " + p.Nickname + ": $" + p.GetBalance();

            return true;
        }
    }
}
