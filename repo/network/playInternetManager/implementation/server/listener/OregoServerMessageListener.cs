using UnityEngine.Networking;

#pragma warning disable 618

namespace OregoBlink.repo.network.playInternetManager.implementation.server.listener
{
    public abstract class OregoNetworkServerMessageListener<T> where T : MessageBase, new()
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
            NetworkServer.RegisterHandler(this.messageType, this.OnReceiveMessage);
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
            NetworkServer.UnregisterHandler(this.messageType);
        }
    }
}