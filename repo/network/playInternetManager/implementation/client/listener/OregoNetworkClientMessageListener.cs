using UnityEngine.Networking;

#pragma warning disable 618

namespace OregoBlink.repo.network.playInternetManager.implementation.client.listener
{
    public abstract class OregoNetworkClientMessageListener<T> where T : MessageBase, new()
    {
        /**
         * Message type.
         */
        
        protected abstract short messageType { get; }

        /**
         * Start.
         */

        public void Start()
        {
            var networkManager = NetworkManager.singleton;
            var client = networkManager.client;
            client.RegisterHandler(this.messageType, this.OnReceiveMessage);
        }

        /**
         * Listen.
         */

        private void OnReceiveMessage(NetworkMessage networkMessage)
        {
            var message = networkMessage.ReadMessage<T>();
            var connection = networkMessage.conn;
            this.OnReceiveMessage(message, connection);
        }

        protected abstract void OnReceiveMessage(T message, NetworkConnection connection);

        /**
         * Stop.
         */

        public void Stop()
        {
            var networkManager = NetworkManager.singleton;
            var client = networkManager.client;
            client.UnregisterHandler(this.messageType);
        }
    }
}