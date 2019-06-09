using UnityEngine;

namespace OregoBlink.game.core.context.manager.@object.controller
{
    public abstract class OregoObjectCreator
    {
        /**
         * Prefab.
         */
        
        public abstract GameObject Prefab { get; }

        /**
         * Spawn.
         */
        
        public virtual GameObject Create()
        {
            var clone = Object.Instantiate(this.Prefab);
            this.OnCreateObject(clone);
            return clone;
        }
        
        protected virtual void OnCreateObject(GameObject clone)
        {
        }
    }
}