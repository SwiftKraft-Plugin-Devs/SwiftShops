using PlayerRoles;
using PluginAPI.Core;

namespace SwiftShops.API
{
    public abstract class ShopItem
    {
        public float Price;

        public string ID;

        public RoleTypeId[] Blacklist = [];
        public Faction[] BlacklistFaction = [];

        public virtual bool Purchase(Player p, out string output)
        {
            if (!CanPurchase(p))
            {
                output = "You cannot purchase this item as " + RoleTranslations.GetRoleName(p.Role) + "! ";
                return false;
            }

            if (p.GetBalance() < Price)
            {
                output = "Insufficient Funds! ";
                return false;
            }

            bool effected = Effect(p, out output);

            if (effected)
                p.RemoveBalance(Price);

            return effected;
        }

        public bool CanPurchase(Player p) => !Blacklist.Contains(p.Role) && !BlacklistFaction.Contains(p.Role.GetFaction());

        public abstract bool Effect(Player p, out string output);

        public abstract string GetDisplayName();

        public override string ToString() => "<color=#FFFFFF>" + ID.ToUpper() + "</color> | " + GetDisplayName() + " | <color=#FFFF00>$" + Price + "</color>";
    }
}
