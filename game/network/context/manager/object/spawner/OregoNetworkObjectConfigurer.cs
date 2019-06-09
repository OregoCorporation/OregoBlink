using OregoBlink.game.core.context.manager.@object.controller;
using OregoBlink.game.network.util;
using OregoBlink.util.unity.gameObject;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618

namespace OregoBlink.game.network.context.manager.@object.spawner
{
    public abstract class OregoNetworkObjectConfigurer : 
        OregoObjectConfigurer, OregoNetworkSpawner
    {
        /**
         * Id.
         */

        public abstract string SpawnId { get; }

        /**
         * Configure.
         */
        
        protected abstract void OnConfigure(GameObject gameObject);

        #region Server

        public override void Configure(GameObject gameObject)
        {
            //Configure game object:
            this.OnConfigure(gameObject);
            
            //Make object online:
            var hash128 = NetworkHash128.Parse(this.SpawnId);
            OregoNetworkUtils.SpawnObject(gameObject, hash128);             
        }

        #endregion

        #region Client

        public void Register()
        {
            var hash128 = NetworkHash128.Parse(this.SpawnId);
            ClientScene.RegisterSpawnHandler(
                hash128,
                this.OnClientSpawn,
                this.OnClientUnSpawn
            );
        }

        public GameObject OnClientSpawn(Vector3 position, NetworkHash128 assetid)
        {
            var gameObject = this.NextClientObject();
            gameObject.SetPosition(position);
            this.OnConfigure(gameObject);
            return gameObject;
        }

        public void OnClientUnSpawn(GameObject spawned) =>
            Object.Destroy(spawned);
        
        public abstract GameObject NextClientObject();
        
        #endregion        
    }
}