using UnityEngine;

public class Armor : MonoBehaviour {
    private int _armorLevel;

    public Armor() {
        _armorLevel = 0;
    }

    public Armor(int armorLevel) {
        _armorLevel = armorLevel;
    }

    public int ArmorLevel {
        get { return _armorLevel; }
        set { _armorLevel = value; }
    }
}
