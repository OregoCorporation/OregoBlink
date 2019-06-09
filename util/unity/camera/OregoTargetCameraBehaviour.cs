using UnityEngine;

namespace OregoBlink.util.unity.camera
{
    internal sealed class OregoTargetCameraBehaviour : MonoBehaviour
    {
        /**
         * Target.
         */
        
        internal GameObject Target { get; set; }

        /**
         * Moves for the player by Y.
         */

        private void Update()
        {
            if (this.Target == null)
            {
                return;
            }
            
            //Update my position:
            var myPosition = this.transform.position;
            var playerPosition = this.Target.transform.position;
            this.transform.position = new Vector3(
                myPosition.x,
                myPosition.y,
                playerPosition.z
            );
        }
    }
}