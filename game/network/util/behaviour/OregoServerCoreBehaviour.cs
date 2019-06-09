using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618

namespace OregoBlink.game.network.util.behaviour
{
    public class OregoServerCoreBehaviour : OregoServerNetworkBehaviour
    {
        private void Update()
        {
            if (this.isServer)
            {
                this.ServerUpdate();
                this.ServerSync();
            }
        }

        /**
         * Trigger.
         */

        #region TriggerEnter

        protected void OnTriggerEnter(Collider other)
        {
            var otherId = this.FetchNetworkIdByServer(other);
            if (otherId.HasValue)
            {
                this.RpcHandleTriggerEnter(otherId.Value);
            }
        }

        [ClientRpc]
        protected void RpcHandleTriggerEnter(NetworkInstanceId otherId)
        {
            //Find other object:
            var otherCollider = this.GetClientComponent<Collider>(otherId);
            this.OnClientTriggerEnter(otherCollider);
        }

        protected virtual void OnClientTriggerEnter(Collider other)
        {
        }

        #endregion

        #region TriggerExit

        protected void OnTriggerExit(Collider other)
        {
            var otherId = this.FetchNetworkIdByServer(other);
            if (otherId.HasValue)
            {
                this.RpcHandleTriggerExit(otherId.Value);
            }
        }

        [ClientRpc]
        protected void RpcHandleTriggerExit(NetworkInstanceId otherId)
        {
            var otherCollider = this.GetClientComponent<Collider>(otherId);
            this.OnClientTriggerExit(otherCollider);
        }

        protected virtual void OnClientTriggerExit(Collider other)
        {
        }

        #endregion

        /**
         * Sync.
         */

        protected virtual void ServerSync()
        {
        }

        /**
         * Util.
         */

        public virtual T GetClientComponent<T>(NetworkInstanceId id) where T : Component =>
            OregoNetworkUtils.FindClientComponent<T>(id);
    }
}