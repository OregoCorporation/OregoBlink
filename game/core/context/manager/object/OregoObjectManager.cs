using System;
using System.Collections.Generic;
using OregoBlink.game.core.context.manager.@object.controller;
using UnityEngine;

namespace OregoBlink.game.core.context.manager.@object
{
    public abstract class OregoObjectManager
    {
        /**
         * Creators.
         */
        
        protected readonly Dictionary<Type, OregoObjectCreator> creatorMap =
            new Dictionary<Type, OregoObjectCreator>();

        /**
         * Configurers.
         */
        
        protected readonly Dictionary<Type, OregoObjectConfigurer> configurerMap =
            new Dictionary<Type, OregoObjectConfigurer>();

        /**
         * Constructor.
         */

        protected OregoObjectManager(
            IEnumerable<OregoObjectCreator> creators,
            IEnumerable<OregoObjectConfigurer> configurers
        )
        {
            //Add creators:
            foreach (var creator in creators)
            {
                var type = creator.GetType();
                this.creatorMap.Add(type, creator);
            }
            
            //Add configurers:
            foreach (var configurer in configurers)
            {
                var type = configurer.GetType();
                this.configurerMap.Add(type, configurer);
            }
        }

        #region Configure

        /**
         * Configure.
         */

        public void ConfigureHierarchy(GameObject gameObject)
        {
            //Configure object:
            this.Configure(gameObject);

            //Configure children:
            var transform = gameObject.transform;
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var childTransform = transform.GetChild(i);
                var childObject = childTransform.gameObject;
                this.ConfigureHierarchy(childObject);
            }
        }

        public void Configure(GameObject gameObject)
        {
            //Fetch spawners:
            var configurers = this.configurerMap.Values;
            foreach (var configurer in configurers)
            {
                if (configurer.Matches(gameObject))
                {
                    configurer.Configure(gameObject);
                }
            }
        }

        public void ConfigureWith(Type type, GameObject gameObject)
        {
            var configurer = this.configurerMap[type];
            configurer?.Configure(gameObject);
        }

        #endregion

        #region Create

        public GameObject CreateWith(Type type)
        {
            var creator = this.creatorMap[type];
            return creator?.Create();
        }

        #endregion
    }
}