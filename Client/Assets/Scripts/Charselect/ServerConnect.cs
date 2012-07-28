using UnityEngine;
using System.Collections;

public class ServerConnect : MonoBehaviour {
    private const string SERVERIP = "192.168.1.11";
    private const string SERVERIPLOCAL = "127.0.0.1";
    private const int PORT = 25000;
    private string _messageLog;
    private Player game_character;
   
    public GUISkin GUISkin;
    void Start() {
        _messageLog = "";
        game_character = new Player();

        // initiate server connection
        if (Network.peerType == NetworkPeerType.Disconnected) {
            Network.Connect(SERVERIPLOCAL, PORT);
        }
    }

    void OnGUI() {
        GUI.skin = GUISkin;
        GUI.Window(0, new Rect(10, 10, 300, 300), TempTools, "");
        GUI.Window(1, new Rect(Screen.width - 300, 10, 300, 500), showMessageLog, "");
    }

    private void showMessageLog(int winndowID) {
        GUI.Label(new Rect(50, 85, 200, 33), "Message log");
        GUI.Box(new Rect(50, 150, 200, 285), _messageLog);
    }
    private void TempTools(int winndowID) {
        GUI.Label(new Rect(50, 85, 200, 33), "SomeInfo");

        if (Network.peerType == NetworkPeerType.Client) {
            GUI.Label(new Rect(50, 120, 200, 33), "client");

            if (GUI.Button(new Rect(50, 160, 200, 33), "Logut"))
                Network.Disconnect();

            if (GUI.Button(new Rect(50, 200, 200, 33), "Send hello to server"))
                SendInfoToServer("hi");
        }
    }
    #region RPC things
    // senders
    [RPC]
    void SendInfoToServer(string info){
        networkView.RPC("ClientToServer", RPCMode.Server, game_character.Name, info);
    }

    // Receivers
    [RPC]
    void SetPlayerInfo(NetworkPlayer player) {
        // Meh
    }
    [RPC]
    void ServerToClient(string someInfo) {
        _messageLog += someInfo + "\n";
    }

    // Aivailble server receivers
    [RPC]
    void ClientToServer(string sender, string someInfo) { }
    #endregion
}
