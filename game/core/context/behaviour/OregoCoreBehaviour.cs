using UnityEngine;

namespace OregoBlink.game.core.context.behaviour
{
    public abstract class OregoCoreBehaviour : MonoBehaviour
    {
        /**
         * Update.
         */
        
        public virtual void HandleUpdate()
        {
        }
        
        public virtual void HandleFixedUpdate()
        {
        }
        
        /**
         * Trigger.
         */

        public virtual void HandleTriggerEnter(Collider other)
        {
            
        }

        public virtual void HandleTriggerEnter2D(Collider2D other)
        {
            
        }

        public virtual void HandleTriggerExit(Collider other)
        {
            
        }

        public virtual void HandleTriggerExit2D(Collider2D other)
        {
            
        }

        public virtual void HandleTriggerStay(Collider other)
        {
            
        }

        public virtual void HandleTriggerStay2D(Collider2D other)
        {
            
        }
        
        /**
         * Disable.
         */
        
        public virtual void HandleDisable()
        {}
    }
}