using System.Collections.Generic;
using UnityEngine;

namespace OregoBlink.game.core.level.indexer
{
    public abstract class OregoLevelIndexer
    {
        /**
         * Collection.
         */

        private readonly List<GameObject> objects = new List<GameObject>();

        /**
         * Matches.
         */
        
        public abstract bool Matches(GameObject gameObject);

        /**
         * Add.
         */
        
        public virtual void Index(GameObject gameObject)
        {
            this.objects.Add(gameObject);            
        }
        
        /**
         * Get.
         */
        
        public GameObject this[int index] => this.objects[index];
    }
}