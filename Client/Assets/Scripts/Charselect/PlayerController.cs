using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private Player _gameChar;

	// Use this for initialization
	void Start () {
        _gameChar = new Player();
        SendInfoToServer("Hi");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    [RPC]
    void SendInfoToServer(string info) {
        networkView.RPC("ClientToServer", RPCMode.Server, _gameChar.Name, info);
    }


    void OnConnectedToServer() {
        //_messageLog += "Connected to server" + "\n";
    }
    void OnDisconnectedToServer() {
       // _messageLog += "Disco from server" + "\n";
    }
}
