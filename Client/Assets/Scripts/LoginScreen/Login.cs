using UnityEngine;
using System.Collections;

public class Login : MonoBehaviour {
    /* 
     * Connection order
     * 
     * client > MYSQL  // check for username
     * MYSQL > CLIENT  // Send answer
     * CLIENT > SERVER // If login is accepted -> connect to server
     * SERVER > CLIENT // Server sends info back
     */

    // default values
    private string _messageLog;
    private string _userName;
    private string _password;

    private Player _game_character;

    private bool _showLoginFailedWindow;

    public GUISkin GUISkin;

    void Start() {
        _messageLog = "";
        _userName = "";
        _password = "";
        _game_character = new Player();
        _showLoginFailedWindow = false;
    }

    void OnGUI() {
        GUI.skin = GUISkin; 
        GUI.Window(0, new Rect(Screen.width/2 - 150, Screen.height/2 - 150, 300, 300), LoginWindow, "");
        // GUI.Window(1, new Rect(10, 10, 300, 500), showMessageLog, "");

        if (_showLoginFailedWindow)
            GUI.Window(0, new Rect(Screen.width / 2 - 150, Screen.height / 2 - 125, 300, 250), showRetrylogin, "");
    }

    private void LoginWindow(int windowID) {     
        GUI.Label(new Rect(50, 85, 200, 33), "Login");

        _userName = GUI.TextField(new Rect(50, 120, 200, 33), _userName, 200);
        _password = GUI.PasswordField(new Rect(50, 160, 200, 33), _password, '•', 25);


        if (GUI.Button(new Rect(50, 200, 200, 33), "Login")) {
            if (_userName != ""){
                if (_password != ""){
					StartCoroutine(LoginCheck(_userName, _password));
                    return;
				}
			}
            LoginFailed();
        }
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    private void showMessageLog(int winndowID) {
        GUI.Label(new Rect(50, 85, 200, 33), "Message log");
        GUI.Box(new Rect(50, 150, 200, 285), _messageLog);
    }

    private void showRetrylogin(int windowID) {
        GUI.BringWindowToFront(0);
        GUI.Label(new Rect(50, 85, 200, 33), "Login failed, try again");
        if (GUI.Button(new Rect(50, 150, 200, 33), "OK"))
            _showLoginFailedWindow = false;

        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }

    private void LoginSuccesfull() {
        _game_character.Name = _userName;
        Application.LoadLevel(1);
    }
    private void LoginFailed() {
        _showLoginFailedWindow = true;
    }
	private IEnumerator LoginCheck(string user, string pass) {
		string url = "http://bubblegum.dyndns-ip.com:8080/DBconnect.php?user=" + user + "&pass=" + pass;
		Debug.Log(url.ToString());
	    WWW w = new WWW(url);
	    yield return w;
	    if (!string.IsNullOrEmpty(w.error)) { 
			Debug.LogError("Failed: " + w.error);
	    } else {
			Debug.Log("Result = " + w.text);
			if (w.text == "Succes")
				LoginSuccesfull();
			else 
				LoginFailed();
	    }
	}
}
