using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerNetwork : MonoBehaviour {
    // default values
    public GUISkin GUIskin;
    private int port = 25000;
    private string _messageLog = "";

    public void Awake() {
        if (Network.peerType == NetworkPeerType.Disconnected)
            Network.InitializeServer(10, port, false);
    }

    public void Update() {
        
    }
    public void OnGUI() {
        GUI.skin = GUIskin;

        GUI.Window(0, new Rect(10, 10, 300, 500), showServerInfo, "");
        GUI.Window(1, new Rect(310, 10, 300, 500), showMessageLog, "");
        
    }

    void OnPlayerConnected(NetworkPlayer player) {
        AskClientForInfo(player);
       
    }
    void OnPlayerDisconnected(NetworkPlayer player) {
        
    }

    void AskClientForInfo(NetworkPlayer player) {
        networkView.RPC("SetPlayerInfo", player, player);
    }   

    private void showServerInfo(int windowID) {
        if (Network.peerType == NetworkPeerType.Server) {
            GUI.Label(new Rect(50, 85, 200, 33), "server info");
            GUI.Label(new Rect(50, 125, 200, 33), "Clients attached: " + Network.connections.Length);

            if (GUI.Button(new Rect(50, 165, 200, 33), "Quit server")) {
                Network.Disconnect();
                Application.Quit();
            }
            if (GUI.Button(new Rect(50, 205, 200, 33), "Send hi to client"))
                SendInfoToClients("hi");
        }
    }

    private void showMessageLog(int winndowID) {
        GUI.Label(new Rect(50, 85, 200, 33), "Message log");
        GUI.Box(new Rect(50, 125, 200, 300), _messageLog);
    }

    // senders
    [RPC]
    void SendInfoToClients(string someInfo) {
        networkView.RPC("ServerToClient", RPCMode.Others, "Server: " + someInfo);
    }

    // Receivers
    [RPC]
    void ClientToServer(string sender, string someInfo) {
        _messageLog += sender + ": " + someInfo + "\n";
    }


    // Aivailble client receivers
    [RPC]
    void ServerToClient(string someInfo) { }
    [RPC]
    void SetPlayerInfo(NetworkPlayer player) { }

}