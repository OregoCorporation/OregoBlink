using UnityEngine;

namespace OregoBlink.game.core.level.indexer.implementation
{
    public class OregoSimpleLevelIndexer<T> : OregoLevelIndexer where T : Component
    {        
        /**
         * Matches.
         */
        
        public override bool Matches(GameObject gameObject) => 
            gameObject.GetComponent<T>() != null;
    }
}