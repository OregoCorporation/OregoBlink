using System;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

#pragma warning disable 618

namespace OregoBlink.repo.network.playInternetManager.implementation.server
{
    public class OregoPlayInternetServerNetworkManager
    {
        #region Event

        public event Action<bool, string, MatchInfo> OnMatchCreatedEvent;

        public event Action OnServerStartedEvent;

        public event Action<NetworkConnection> OnConnectClientEvent;

        public event Action<NetworkConnection> OnClientReadyEvent;

        public event Action<string> OnSceneChangedEvent;

        public event Action<NetworkConnection, short, NetworkReader> OnAddClientPlayerEvent;

        public event Action<NetworkConnection> OnDisconnectClientEvent;

        public event Action<bool, string> OnDestroyMatchEvent;

        public event Action OnServerStopededEvent;
        
        #endregion
        
        #region Create match

        /**
         * Create internet match.
         */

        public virtual void CreateInternetLobby(
            string lobbyName,
            uint roomSize,
            string publicClientAddress,
            string privateClientAddress,
            int eloScoreForMatch,
            int requestDomain,
            bool matchAdvertise,
            string matchPassword,
            NetworkMatch.DataResponseDelegate<MatchInfo> callback = null
        )
        {
            //Check callback:
            if (callback == null)
            {
                callback = NetworkManager.singleton.OnMatchCreate;
            }
            
            //Create match:
            var matchMaker = NetworkManager.singleton.matchMaker;
            matchMaker.CreateMatch(
                lobbyName,
                roomSize,
                matchAdvertise,
                matchPassword,
                publicClientAddress,
                privateClientAddress,
                eloScoreForMatch,
                requestDomain,
                callback
            );
        }

        public virtual void DeployHost(MatchInfo matchInfo)
        {
            NetworkManager.singleton.StartHost(matchInfo);
        }
        
        #endregion
        
        #region Callback

        public virtual void OnMatchCreated(bool success, string extendedInfo, MatchInfo info)
        {
            this.OnMatchCreatedEvent?.Invoke(success, extendedInfo, info);
        }

        public virtual void OnServerStarted()
        {
            this.OnServerStartedEvent?.Invoke();
        }

        public virtual void OnConnectClient(NetworkConnection connection)
        {
            this.OnConnectClientEvent?.Invoke(connection);
        }

        public virtual void OnClientReady(NetworkConnection connection)
        {
            this.OnClientReadyEvent?.Invoke(connection);
        }

        public virtual void OnSceneChanged(string sceneName)
        {
            this.OnSceneChangedEvent?.Invoke(sceneName);
        }

        public virtual void OnAddClientPlayer(
            NetworkConnection connection,
            short playerControllerId,
            NetworkReader extraMessageReader
        )
        {
            this.OnAddClientPlayerEvent?.Invoke(connection, playerControllerId, extraMessageReader);
        }

        public virtual void OnDisconnectClient(NetworkConnection connection)
        {
            this.OnDisconnectClientEvent?.Invoke(connection);
        }
        
        public virtual void OnDestroyMatch(bool success, string extendedInfo)
        {
            this.OnDestroyMatchEvent?.Invoke(success, extendedInfo);
        }
       
        public virtual void OnServerStoped()
        {
            this.OnServerStopededEvent?.Invoke();
        }
        
        #endregion
    }
}