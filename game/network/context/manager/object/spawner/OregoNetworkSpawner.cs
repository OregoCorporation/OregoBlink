#pragma warning disable 618

namespace OregoBlink.game.network.context.manager.@object.spawner
{
    public interface OregoNetworkSpawner
    {
        /**
         * Id.
         */

        string SpawnId { get; }

        /**
         * Client.
         */

        void Register();
    }
}