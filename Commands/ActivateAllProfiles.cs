using CommandSystem;
using SwiftAPI.Commands;
using SwiftShops.API;

namespace SwiftShops.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ActivateAllProfiles : CommandBase
    {
        public override string[] GetAliases() => ["asallprof"];

        public override string GetCommandName() => "actiallshopprofile";

        public override string GetDescription() => "Activates or toggles all shop profiles.";

        public override PlayerPermissions[] GetPerms() => [PlayerPermissions.PlayersManagement];

        public override bool Function(string[] args, ICommandSender sender, out string result)
        {
            if (!TryGetArgument(args, 2, out string arg2) || !bool.TryParse(arg2, out bool st))
            {
                ToggleAll();

                result = "Toggled all shop profiles activeness. ";

                return true;
            }

            SetAll(st);

            result = "Set all shop profiles activeness to " + st;

            return true;
        }

        private static void SetAll(bool st)
        {
            foreach (ShopProfile prof in ShopManager.RegisteredShopProfiles.Values)
                prof.SetProfileActive(st);
        }

        private static void ToggleAll()
        {
            foreach (ShopProfile prof in ShopManager.RegisteredShopProfiles.Values)
                prof.SetProfileActive(!prof.Activated);
        }
    }
}
