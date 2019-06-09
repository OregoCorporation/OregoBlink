using OregoBlink.game.core.context.manager.@object.controller;
using OregoBlink.game.network.util;
using OregoBlink.util.unity.gameObject;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618

namespace OregoBlink.game.network.context.manager.@object.spawner
{
    public abstract class OregoNetworkObjectCreator : 
        OregoObjectCreator, OregoNetworkSpawner
    {
        /**
         * Id.
         */

        public abstract string SpawnId { get; }

        #region Server

        public override GameObject Create()
        {
            //Create object:
            var gameObject = base.Create();
            
            //Make object online:
            var hash128 = NetworkHash128.Parse(this.SpawnId);
            OregoNetworkUtils.SpawnObject(gameObject, hash128);
            return gameObject;
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
            //Create game object:
            var gameObject = base.Create();
            gameObject.SetPosition(position);
            return gameObject;
        }

        public void OnClientUnSpawn(GameObject spawned) =>
            Object.Destroy(spawned);

        #endregion        
    }
}