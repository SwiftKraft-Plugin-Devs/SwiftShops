using PluginAPI.Core;

namespace SwiftShops.API
{
    public abstract class ShopItem
    {
        public float Price;

        public string ID;

        public virtual bool Purchase(Player p, out string output)
        {
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

        public abstract bool Effect(Player p, out string output);

        public abstract string GetDisplayName();

        public override string ToString() => ID + " | " + GetDisplayName() + " - $" + Price;
    }
}
