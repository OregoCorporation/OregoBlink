using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;

#pragma warning disable 618

namespace OregoBlink.repo.network.playInternetManager.implementation.client
{
    public class OregoPlayInternetClientNetworkManager
    {
        #region Event

        public event Action<NetworkConnection> OnConnectClientEvent;

        public event Action<NetworkConnection> OnClientSceneChangedEvent;

        public event Action<bool, string, List<MatchInfoSnapshot>> OnMatchesFoundEvent;

        public event Action<NetworkClient> OnStartClientEvent;

        public event Action<bool, string, MatchInfo> OnMatchJoinedEvent;

        public event Action OnStopClientEvent;

        public event Action OnDisconnectEvent;

        public event Action OnDropEvent;

        public event Action<NetworkConnection, int> OnClientErrorEvent;

        public event Action<NetworkConnection> OnClientNotReadyEvent;
        
        #endregion

        #region Find matches

        public virtual void FindInternetLobbies(int startPageNumber,
            int resultPageSize,
            string matchNameFilter,
            bool filterOutPrivateMatchesFromResults,
            int eloScoreTarget,
            int requestDomain,
            NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> callback = null
        )
        {
            //Check callback:
            if (callback == null)
            {
                callback = NetworkManager.singleton.OnMatchList;
            }

            //Find matches:
            var matchMaker = NetworkManager.singleton.matchMaker;
            matchMaker.ListMatches(
                startPageNumber,
                resultPageSize,
                matchNameFilter,
                filterOutPrivateMatchesFromResults,
                eloScoreTarget,
                requestDomain,
                callback
            );
        }

        #endregion

        #region Join match

        public virtual void JoinInternetLobby(
            NetworkID networkId,
            string matchPassword,
            string publicClientAddress,
            string privateClientAddress,
            int eloScoreForClient,
            int requestDomain,
            NetworkMatch.DataResponseDelegate<MatchInfo> callback = null
        )
        {
            //Check callback:
            if (callback == null)
            {
                callback = NetworkManager.singleton.OnMatchJoined;
            }

            //Join match:
            var matchMaker = NetworkManager.singleton.matchMaker;
            matchMaker.JoinMatch(
                networkId,
                matchPassword,
                publicClientAddress,
                privateClientAddress,
                eloScoreForClient,
                requestDomain,
                callback
            );
        }

        public virtual void DeployClient(MatchInfo matchInfo)
        {
            NetworkManager.singleton.StartClient(matchInfo);
        }
        
        #endregion

        #region Callback

        public virtual void OnConnectClient(NetworkConnection connection)
        {
            this.OnConnectClientEvent?.Invoke(connection);
        }

        public virtual void OnClientSceneChanged(NetworkConnection connection)
        {
            this.OnClientSceneChangedEvent?.Invoke(connection);
        }

        public virtual void OnMatchesFound(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
        {
            this.OnMatchesFoundEvent?.Invoke(success, extendedInfo, matchList);
        }

        public virtual void OnStartClient(NetworkClient networkClient)
        {
            this.OnStartClientEvent?.Invoke(networkClient);
        }

        public virtual void OnMatchJoined(bool success, string extendedInfo, MatchInfo info)
        {
            this.OnMatchJoinedEvent?.Invoke(success, extendedInfo, info);
        }

        public virtual void OnStopClient()
        {
            this.OnStopClientEvent?.Invoke();
        }

        public virtual void OnDisconnectClient()
        {
            this.OnDisconnectEvent?.Invoke();
        }

        public virtual void OnDropConnectionResult()
        {
            this.OnDropEvent?.Invoke();
        }
        
        public virtual void OnClientError(NetworkConnection networkConnection, int errorCode)
        {
            this.OnClientErrorEvent?.Invoke(networkConnection, errorCode);
        }
        
        public virtual void OnClientNotReady(NetworkConnection connection)
        {
            this.OnClientNotReadyEvent?.Invoke(connection);
        }
        
        #endregion

        #region Util

        protected static void RegisterPrefab(Component component) => 
            ClientScene.RegisterPrefab(component.gameObject);       
        
        #endregion
    }
}