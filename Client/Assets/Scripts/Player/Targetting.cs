/// <summary>
/// Targetting.cs
/// Glenn Latomme (glenn.latomme@gmail.com)
/// 
/// This script can be attached to any permanent gameobject and is responsible for alllowing the player to target different mobs that are within range
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour {
    public List<Transform> targets;
    public Transform selectedTarget;

    private Transform _myTransform;

    void Start() {
        targets = new List<Transform>();
        selectedTarget = null;
        _myTransform = transform;

        AddAllEnemies();
    }

    public void AddAllEnemies() {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in go) {
            AddTarget(enemy.transform);
        }
    }

    public void AddTarget(Transform enemy) {
        targets.Add(enemy);
    }

    public void SortTargetsByDistance() {
        targets.Sort(delegate(Transform t1, Transform t2) {
            return Vector3.Distance(t1.position, _myTransform.position).CompareTo(Vector3.Distance(t2.position, _myTransform.position));
        });
    }

    private void TargetEnemy() {
        if (selectedTarget == null) {
            SortTargetsByDistance();
            selectedTarget = targets[0];
        } else {
            int index = targets.IndexOf(selectedTarget);
            if (index < targets.Count - 1)
                index++;
            else
                index = 0;
            DeselectTarget();
            selectedTarget = targets[index];
        }
        SelectTarget();
    }

    private void SelectTarget() {
        Transform name = selectedTarget.FindChild("Name");

        if (name == null) {
            Debug.LogError("Could not find the name on" + selectedTarget.name);
            return;
        }
        name.GetComponent<TextMesh>().text = selectedTarget.GetComponent<Mob>().Name;
        name.GetComponent<MeshRenderer>().enabled = true;
        selectedTarget.GetComponent<Mob>().DispalyHealth();

        Messenger<bool>.Broadcast("show mob vitalbar", true);
    }

    private void DeselectTarget() {
        selectedTarget.FindChild("Name").GetComponent<MeshRenderer>().enabled = false;
        selectedTarget = null;

        Messenger<bool>.Broadcast("show mob vitalbar", false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab))
            TargetEnemy();
    }
}
