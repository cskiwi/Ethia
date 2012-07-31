using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
    public GameObject playerCharater;
    public GameObject gameSettings;

    private GameObject _pc;
    private Vector3 _playerSpawnPointPos;       // Default spawn point when spawn point is not found

    void Start() {
        GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);
        _playerSpawnPointPos = new Vector3(650, 1, 565);

        if (go == null) {
            Debug.LogWarning("Cannot find player spawn point");

            go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
            // Debug.Log("Created player spawn point");

            go.transform.position = _playerSpawnPointPos;
            // Debug.Log("Moved player spawn point");
        }

        _pc = Instantiate(playerCharater, go.transform.position, Quaternion.identity) as GameObject;
        _pc.name = "pc";

        LoadCharater();
    }

    public void LoadCharater() {
        GameObject gs = GameObject.Find("__GameSettings");

        if (gs == null) {
            GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
            gs1.name = "__GameSettings";
        }

        GameObject.Find("__GameSettings").GetComponent<GameSettings>().LoadCharaterData();
    }
}
