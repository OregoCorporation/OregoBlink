using UnityEngine;

namespace OregoBlink.game.core.context.manager.@object.controller
{
    public abstract class OregoObjectConfigurer
    {        
        /**
         * Matches.
         */

        public abstract bool Matches(GameObject gameObject);

        /*
         * Spawn.
         */

        public abstract void Configure(GameObject gameObject);
    }
}