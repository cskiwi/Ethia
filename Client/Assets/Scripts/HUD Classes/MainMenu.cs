using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public const float VERSION = .1f;
    public bool clearPrefs = false;

    private string _levelToLoad = "";

    private const string CHARTER_GENERATION = "Charater Generation";
    private const string FIRST_LEVEL = "Level1";

    void Start() {
        if (clearPrefs)
            PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("ver")) {
            Debug.Log("There is a ver key");
            if (PlayerPrefs.GetFloat("ver") != VERSION) {
                Debug.Log("Saved version is not the same as current version");
            } else {
                Debug.Log("Saved version is the same as the current version");
                if (PlayerPrefs.HasKey("Player Name")) {
                    Debug.Log("There is a Player name key");
                    if (PlayerPrefs.GetString("Player Name") == "") {
                        Debug.Log("The player name key is empty");
                    } else {
                        Debug.Log("The player name key has a value");
                    }
                } else {
                    Debug.Log("There is no player name key");
                }
            }
        } else {
            Debug.Log("There is no ver key");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("ver", VERSION);
            _levelToLoad = CHARTER_GENERATION;
        }
    }

    void Update() {
        if (_levelToLoad == "")
            return;

        Application.LoadLevel(_levelToLoad);
    }
}
