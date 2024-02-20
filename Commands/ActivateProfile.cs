using CommandSystem;
using SwiftAPI.Commands;
using SwiftShops.API;

namespace SwiftShops.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ActivateProfile : CommandBase
    {
        public override string[] GetAliases() => ["asprof"];

        public override string GetCommandName() => "actishopprofile";

        public override string GetDescription() => "Activates a shop profile.";

        public override PlayerPermissions[] GetPerms() => [PlayerPermissions.PlayersManagement];

        public override bool Function(string[] args, ICommandSender sender, out string result)
        {
            if (!TryGetArgument(args, 1, out string arg1) || !ShopManager.TryGetProfile(arg1, out ShopProfile prof))
            {
                result = ListShopProfiles();

                return true;
            }

            bool state = !prof.Activated;

            if (TryGetArgument(args, 2, out string arg2) && bool.TryParse(arg2, out bool st))
                state = st;

            prof.SetProfileActive(state);

            result = "Set shop profile " + prof.DisplayName + " activeness to " + prof.Activated;

            return true;
        }

        private static string ListShopProfiles()
        {
            string s = "Shop Profiles: \n";

            foreach (ShopProfile prof in ShopManager.RegisteredShopProfiles.Values)
                s += "\n" + prof.ToString();

            return s;
        }
    }
}
