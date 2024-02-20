using CommandSystem;
using PluginAPI.Core;
using SwiftAPI.Commands;
using SwiftAPI.Utility.Targeters;
using SwiftShops.API;
using System.Collections.Generic;

namespace SwiftShops.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetBalance : CommandBase
    {
        public override string[] GetAliases() => ["sbal"];

        public override string GetCommandName() => "setbalance";

        public override string GetDescription() => "Sets balances of selected players.";

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
                p.SetBalance(amount);

            result = "Set the balance of " + players.Count + " player(s) to " + amount;

            return true;
        }
    }
}
