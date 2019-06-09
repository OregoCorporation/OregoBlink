using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 618

namespace OregoBlink.game.network.util
{
    public static class OregoNetworkUtils
    {
        #region DefaultSpawn

        /**
         * Spawns a game object with childrens.
         */

        public static void MakeOnline(GameObject gameObject)
        {
            //Try send clone on client:
            if (gameObject.GetComponent<NetworkIdentity>() != null)
            {
                //Spawn object:
                NetworkServer.Spawn(gameObject);
            }

            //Spawn children:
            var transform = gameObject.transform;
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var childTransform = transform.GetChild(i);
                var childObject = childTransform.gameObject;
                MakeOnline(childObject);
            }
        }

        public static void MakeOnlineByPrefab(GameObject prefab)
        {
            //Check prefab:
            if (prefab.GetComponent<NetworkIdentity>() != null)
            {
                //Spawn object:
                var clone = Object.Instantiate(prefab);
                NetworkServer.Spawn(clone);
            }

            //Spawn children:
            var transform = prefab.transform;
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var childTransform = transform.GetChild(i);
                var childObject = childTransform.gameObject;
                MakeOnlineByPrefab(childObject);
            }
        }

        #endregion

        #region Register

        public static void RegisterByPrefab(GameObject prefab)
        {
            //Try register prefab:
            if (prefab.GetComponent<NetworkIdentity>() != null)
            {
                //Spawn object:
                ClientScene.RegisterPrefab(prefab);
            }

            //Register children:
            var transform = prefab.transform;
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var childTransform = transform.GetChild(i);
                var childObject = childTransform.gameObject;
                RegisterByPrefab(childObject);
            }
        }

        #endregion

        #region CustomSpawn

        public static void SpawnObject(GameObject otherObject, NetworkHash128 hash128)
        {
            //Fucking reflect:
            var view = otherObject.GetComponent<NetworkIdentity>();
            var assetId = view
                .GetType()
                .GetField("m_AssetId", BindingFlags.NonPublic | BindingFlags.Instance);
            assetId.SetValue(view, hash128);

            Debug.Log("BURN ASSET ID");

            //Fucking network server:
            var type = typeof(NetworkServer);
            var info = type.GetProperty("instance", BindingFlags.NonPublic | BindingFlags.Static);
            var networkServer = info.GetValue(null, null) as NetworkServer;

            Debug.Log("BURN SERVER INSTANCE");

            //Fucking spawn:
            type.GetMethod("SpawnObject", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(networkServer,
                    new object[]
                    {
                        otherObject
                    });

            Debug.Log("BURN SPAWN OBJECT");
        }

        #endregion

        #region Extensions

        public static NetworkInstanceId? FetchNetworkIdByServer(
            this NetworkBehaviour networkBehaviour,
            Component other
        )
        {
            if (!networkBehaviour.isServer)
            {
                return null;
            }

            var otherIdentity = other.GetComponent<NetworkIdentity>();
            if (otherIdentity == null)
            {
                return null;
            }

            return otherIdentity.netId;
        }

        public static bool IsRemoteClient(this NetworkBehaviour behaviour) => 
            behaviour.isClient && !behaviour.isServer;
        
        public static T FindClientComponent<T>(NetworkInstanceId id) where T : Component
        {
            var otherObject = ClientScene.FindLocalObject(id);
            return otherObject.GetComponent<T>();
        }

        #endregion
    }
}