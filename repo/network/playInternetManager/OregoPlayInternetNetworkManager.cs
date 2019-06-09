using System.Collections.Generic;
using OregoBlink.repo.network.playInternetManager.implementation.client;
using OregoBlink.repo.network.playInternetManager.implementation.server;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

#pragma warning disable 618

namespace OregoBlink.repo.network.playInternetManager
{
    public class OregoPlayInternetNetworkManager : NetworkManager
    {
        public OregoPlayInternetServerNetworkManager ServerNetworkManager { get; protected set; }

        public OregoPlayInternetClientNetworkManager ClientNetworkManager { get; protected set; }

        #region Init
        
        public virtual void Init()
        {
            this.ServerNetworkManager = new OregoPlayInternetServerNetworkManager();
            this.ClientNetworkManager = new OregoPlayInternetClientNetworkManager();
        }

        #endregion

        #region Service

        public void SeUnityInternetService()
        {
            this.StartMatchMaker();
        }

        #endregion

        #region Server

        public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo info)
        {
            base.OnMatchCreate(success, extendedInfo, info);
            this.ServerNetworkManager.OnMatchCreated(success, extendedInfo, info);
        }

        public override void OnStopServer()
        {
            this.ServerNetworkManager.OnServerStoped();
        }

        public override void OnStartServer()
        {
            this.ServerNetworkManager.OnServerStarted();
        }

        public override void OnServerConnect(NetworkConnection connection)
        {
            this.ServerNetworkManager.OnConnectClient(connection);
        }

        public override void OnServerReady(NetworkConnection connection)
        {
            base.OnServerReady(connection);
            this.ServerNetworkManager.OnClientReady(connection);
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            this.ServerNetworkManager.OnSceneChanged(sceneName);
        }

        public override void OnServerAddPlayer(
            NetworkConnection connection,
            short playerControllerId,
            NetworkReader extraMessageReader
        )
        {
            base.OnServerAddPlayer(connection, playerControllerId, extraMessageReader);
            this.ServerNetworkManager.OnAddClientPlayer(connection, playerControllerId, extraMessageReader);
        }

        public override void OnServerDisconnect(NetworkConnection connection)
        {
            base.OnServerDisconnect(connection);
            this.ServerNetworkManager.OnDisconnectClient(connection);
        }

        public override void OnDestroyMatch(bool success, string extendedInfo)
        {
            base.OnDestroyMatch(success, extendedInfo);
            this.ServerNetworkManager.OnDestroyMatch(success, extendedInfo);
        }

        #endregion

        #region Client

        public override void OnClientConnect(NetworkConnection connection)
        {
            base.OnClientConnect(connection);
            this.ClientNetworkManager.OnConnectClient(connection);
        }

        public override void OnClientSceneChanged(NetworkConnection connection)
        {
            base.OnClientSceneChanged(connection);
            this.ClientNetworkManager.OnClientSceneChanged(connection);
        }

        public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
        {
            base.OnMatchList(success, extendedInfo, matchList);
            this.ClientNetworkManager.OnMatchesFound(success, extendedInfo, matchList);
        }

        public override void OnStartClient(NetworkClient networkClient)
        {
            this.ClientNetworkManager.OnStartClient(networkClient);
        }

        public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo info)
        {
            base.OnMatchJoined(success, extendedInfo, info);
            this.ClientNetworkManager.OnMatchJoined(success, extendedInfo, info);
        }

        public override void OnStopClient()
        {
            this.ClientNetworkManager.OnStopClient();
        }

        public override void OnClientDisconnect(NetworkConnection connection)
        {
            base.OnClientDisconnect(connection);
            this.ClientNetworkManager.OnDisconnectClient();
        }

        public override void OnDropConnection(bool success, string extendedInfo)
        {
            base.OnDropConnection(success, extendedInfo);
            this.ClientNetworkManager.OnDropConnectionResult();
        }

        public override void OnClientError(NetworkConnection connection, int errorCode)
        {
            this.ClientNetworkManager.OnClientError(connection, errorCode);
        }

        public override void OnClientNotReady(NetworkConnection connection)
        {
            this.ClientNetworkManager.OnClientNotReady(connection);
        }
        
        #endregion
    }
}