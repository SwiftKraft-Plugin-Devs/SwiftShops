using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace SwiftShops
{
    public class Plugin
    {
        /// <summary>
        /// Singleton for the SwiftShops API base plugin class. 
        /// This can be used anywhere to access the base class.
        /// </summary>
        public static Plugin Instance;

        private const string Author = "SwiftKraft";

        private const string Name = "SwiftShops";

        private const string Description = "An Open Source Shop API for NWAPI SCP: SL Servers. ";

        private const string Version = "Alpha v0.0.1";

        [PluginPriority(LoadPriority.High)]
        [PluginEntryPoint(Name, Version, Description, Author)]
        public void Init()
        {
            Instance = this;

            EventManager.RegisterEvents<EventHandler>(this);

            Log.Info("SwiftShops Loaded! ");
        }
    }
}
