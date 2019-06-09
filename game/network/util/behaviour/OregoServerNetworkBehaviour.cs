using UnityEngine.Networking;

#pragma warning disable 618

namespace OregoBlink.game.network.util.behaviour
{
    public class OregoServerNetworkBehaviour : NetworkBehaviour
    {
        #region Update

        private void Update()
        {
            if (this.isServer)
            {
                this.ServerUpdate();
            }
        }

        private void FixedUpdate()
        {
            if (this.isServer)
            {
                this.ServerFixedUpdate();
            }
        }

        #endregion

        #region Callback

        private void OnDisable()
        {
            if (this.isServer)
            {
                this.OnServerDisable();
            }
        }

        #endregion

        public virtual void OnServerDisable(){
        }

        public virtual void ServerUpdate(){
        }

        public virtual void ServerFixedUpdate(){
        }        
    }
}