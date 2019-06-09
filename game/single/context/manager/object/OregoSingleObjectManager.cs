using System.Collections.Generic;
using OregoBlink.game.core.context.manager.@object;
using OregoBlink.repo.@object.implementation.single.spawner;

namespace OregoBlink.repo.@object.implementation.single
{
    public abstract class OregoSingleObjectManager : OregoObjectManager
    {
        /**
         * Constructor.
         */

        protected OregoSingleObjectManager(
            IEnumerable<OregoSingleObjectCreator> creators,
            IEnumerable<OregoSingleObjectConfigurer> configurers
        ) : base(creators, configurers)
        {
        }
    }
}