using PluginAPI.Core;
using System.Collections.Generic;
using UnityEngine;

namespace SwiftShops.API
{
    public static class ShopManager
    {
        public static readonly Dictionary<Player, float> Balance = [];
        public static readonly Dictionary<string, ShopProfile> RegisteredShopProfiles = [];

        public static float GetBalance(this Player p)
        {
            if (p == null)
                return -Mathf.Infinity;

            p.RegisterBalance();
            return Balance[p];
        }

        public static void SetBalance(this Player p, float amount)
        {
            if (p == null)
                return;

            p.RegisterBalance();
            Balance[p] = amount;
        }

        public static void RemoveBalance(this Player p, float amount) => p.SetBalance(p.GetBalance() - amount);

        public static void RegisterBalance(this Player p)
        {
            if (!Balance.ContainsKey(p))
                Balance.Add(p, 0f);
        }

        public static bool RegisterProfile(ShopProfile prof)
        {
            if (ProfileExists(prof.ID))
                return false;
            RegisteredShopProfiles.Add(prof.ID, prof);
            return true;
        }

        public static void SetProfileActive(this ShopProfile prof, bool activeness) => prof.Activated = activeness;

        public static void SetProfileActive(this string id, bool activeness)
        {
            if (TryGetProfile(id, out ShopProfile prof))
                prof.SetProfileActive(activeness);
        }

        public static bool Purchase(this Player p, string id, out string output)
        {
            if (TryGetItem(id, out ShopItem item))
                return ShopProfile.Purchase(item, p, out output);
            output = GetFailString(id);
            return false;
        }

        public static ShopItem[] GetAllItems()
        {
            List<ShopItem> items = [];
            foreach (ShopProfile prof in RegisteredShopProfiles.Values)
            {
                ShopItem[] ss = prof.GetAllItems();
                foreach (ShopItem s in ss)
                    items.Add(s);
            }
            return [.. items];
        }

        public static ShopItem GetItem(string id, out ShopProfile profile)
        {
            foreach (ShopProfile prof in RegisteredShopProfiles.Values)
                if (prof.Activated && prof.TryGetItem(id, out ShopItem item))
                {
                    profile = prof;
                    return item;
                }
            profile = null;
            return null;
        }

        public static ShopItem GetItem(string id) => GetItem(id, out _);

        public static bool TryGetItem(string id, out ShopProfile profile, out ShopItem item)
        {
            item = GetItem(id, out profile);
            return item != null && profile != null;
        }

        public static bool TryGetItem(string id, out ShopItem item) => TryGetItem(id, out _, out item);

        public static ShopProfile GetProfile(string id)
        {
            if (ProfileExists(id))
                return RegisteredShopProfiles[id];
            return null;
        }

        public static bool TryGetProfile(string id, out ShopProfile output)
        {
            output = GetProfile(id);
            return output != null;
        }

        public static bool ProfileExists(string id) => RegisteredShopProfiles.ContainsKey(id);

        public static string GetFailString(string id) => $"Item \"{id}\" doesn't exist! ";
    }
}
