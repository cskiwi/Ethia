using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {
    public enum State {
        Idle,
        Initialize,
        Setup,
        SpawnMob
    }

    public GameObject[] mobPrefabs;     // An array to hold all of the prefabs of mobs we want to spawn
    public GameObject[] spawnPoints;    // An array of all the spawn points

    public State state;                 // this is our local var that holds our current state

    void Awake() {
        state = State.Initialize;
    }

    IEnumerator Start() {
        while (true) {
            switch (state) {
                case State.Initialize:
                    Initialize();
                    break;
                case State.Setup:
                    Setup();
                    break;
                case State.SpawnMob:
                    SpawnMob();
                    break;
            }

            yield return 0;
        }
    }

    private void Initialize() {
        Debug.Log("-----=[Initialzie]=-----");

        if (!checkForMobPrefabs())
            return;

        if (!checkForSpawnPoints())
            return;

        state = State.Setup;
    }

    private void Setup() {
        Debug.Log("-----=[Setup]=-----");

        state = State.SpawnMob;
    }

    /// <summary>
    /// Spawn a mob in an open spawn point
    /// </summary>
    private void SpawnMob() {
        Debug.Log("-----=[SpawnMob]=-----");

        GameObject[] gos = AvailableSpawnPoints();

        for (int i = 0; i < gos.Length; i++) {
            GameObject go = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)], gos[i].transform.position, Quaternion.identity) as GameObject;

            go.transform.parent = gos[i].transform;
        }

        state = State.Idle;
    }

    /// <summary>
    /// Check if there are mob prefabs
    /// </summary>
    /// <returns>if has atleasd one prefab,returns true</returns>
    private bool checkForMobPrefabs() {
        return mobPrefabs.Length > 0;
    }

    /// <summary>
    /// Check if there are spawn points
    /// </summary>
    /// <returns>if has at leased one spawn point, returns true</returns>
    private bool checkForSpawnPoints() {
        return spawnPoints.Length > 0;
    }

    /// <summary>
    /// generate a list of available spawnpoints that does not have any mobs chiled to it
    /// </summary>
    /// <returns>An array of spawnpoints</returns>
    private GameObject[] AvailableSpawnPoints() {
        List<GameObject> gos = new List<GameObject>();

        for (int i = 0; i < spawnPoints.Length; i++) {
            if (spawnPoints[i].transform.childCount == 0) {
                Debug.Log("-----=[Spawn point Availible]=-----");
                gos.Add(spawnPoints[i]);
            }
        }    

        return gos.ToArray();
    }
}
