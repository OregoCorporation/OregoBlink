using System.Collections.Generic;
using OregoBlink.game.core.context.manager.@object;
using OregoBlink.game.network.context.manager.@object.spawner;

#pragma warning disable 618

namespace OregoBlink.game.network.context.manager.@object
{
    public class OregoNetworkObjectManager : OregoObjectManager
    {
        /**
         * Register client spawners flag.
         */

        private bool isRegistered;

        /**
         * Constructor.
         */

        protected OregoNetworkObjectManager(
            IEnumerable<OregoNetworkObjectCreator> creators,
            IEnumerable<OregoNetworkObjectConfigurer> configurers
        ) : base(creators, configurers)
        {
        }

        #region Register

        public void RegisterSpawners()
        {
            //Check handlers:
            if (this.isRegistered)
            {
                return;
            }

            this.isRegistered = true;

            //Register creators:
            var creators = this.creatorMap.Values;
            foreach (var creator in creators)
            {
                (creator as OregoNetworkObjectCreator).Register();
            }

            //Register configurers:
            var configurers = this.configurerMap.Values;
            foreach (var configurer in configurers)
            {
                (configurer as OregoNetworkObjectConfigurer).Register();
            }
        }

        #endregion
    }
}