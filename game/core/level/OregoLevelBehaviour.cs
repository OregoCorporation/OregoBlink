using System;
using System.Collections.Generic;
using System.Linq;
using OregoBlink.game.core.context.behaviour;
using OregoBlink.game.core.level.@event;
using OregoBlink.game.core.level.indexer;
using OregoBlink.game.core.level.startPosition.behaviour;
using OregoBlink.game.core.level.startPosition.placer;
using OregoBlink.game.core.level.startPosition.placer.implementation;
using UnityEngine;
using Random = System.Random;

namespace OregoBlink.game.core.level
{
    public abstract class OregoLevelBehaviour : OregoCoreBehaviour
    {
        /**
         * Event.
         */
        
        public event Action<OregoLevelEvent> OnGameEvent;

        /**
         * Player placer.
         */
        
        public OregoPlayerPlacer PlayerPlacer { get; set; } = new OregoRandomPlayerPlacer();
        
        #region Init

        protected virtual void Awake()
        {
            this.MakeIndexing();
        }

        #endregion
        
        #region Indexing

        /**
         * Indexers.
         */
        
        private readonly Dictionary<Type, OregoLevelIndexer> indexerMap = 
            new Dictionary<Type, OregoLevelIndexer>();

        /**
         * Collect.
         */
        
        public void MakeIndexing()
        {
            //Load indexers:
            var indexers = this.CreateIndexers();
            foreach (var indexer in indexers)
            {
                var type = indexer.GetType();
                this.indexerMap.Add(type, indexer);
            }
            
            //Collect game objects:
            this.IndexObjects(this.gameObject);
        }

        protected virtual void IndexObjects(GameObject node)
        {
            //Check game object for collection:
            this.IndexObject(node);
            
            //Check children:
            var nodeTransform = node.transform;
            var childCount = nodeTransform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var childTransform = nodeTransform.GetChild(i);
                var childObject = childTransform.gameObject;
                this.IndexObjects(childObject);
            }
        }

        protected virtual void IndexObject(GameObject node)
        {
            var indexers = this.indexerMap.Values;
            foreach (var indexer in indexers)
            {
                if (indexer.Matches(node))
                {
                    indexer.Index(node);
                }
            }
        }
        
        protected virtual IEnumerable<OregoLevelIndexer> CreateIndexers() => 
            new HashSet<OregoLevelIndexer>();

        public OregoLevelIndexer GetIndexer(Type type) => 
            this.indexerMap[type];
        
        #endregion                
    }
}