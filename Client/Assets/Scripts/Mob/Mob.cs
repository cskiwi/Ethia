using UnityEngine;
using System.Collections;

public class Mob : BaseCharacter {
    public int curHealth;
    public int maxHealth;

    void Start() {
//        GetPrimaryAttribute((int)AttributeName.Constitution).BaseValue = 100;
//        GetVital((int)VitalName.Health).Update();

        Name = "Battle Cube";
    }

    void Update() {
        
    }

    public void DispalyHealth() {
        Messenger<int, int>.Broadcast("mob vital update", curHealth, maxHealth);
    }
}
