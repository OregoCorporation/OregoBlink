using UnityEngine.Networking;

#pragma warning disable 618

namespace OregoBlink.game.network.util.behaviour
{
    public abstract class OregoClientNetworkBehaviour : NetworkBehaviour
    {
        private void OnDisable()
        {
            if (this.isClient)
            {
                this.OnClientDisable();
            }
        }

        private void Update()
        {
            if (this.isClient)
            {
                this.ClientUpdate();                
            }            
        }

        private void FixedUpdate()
        {
            if (this.isClient)
            {
                this.ClientFixedUpdate();                
            }
        }

        public abstract void OnClientDisable();

        public abstract void ClientUpdate();

        public abstract void ClientFixedUpdate();
    }
}