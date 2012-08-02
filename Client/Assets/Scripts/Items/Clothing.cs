using UnityEngine;

public class Clothing : BuffItem {
    private ArmorSlot _slot;      // Store the slot the armor is in

    private Clothing() {
        _slot = ArmorSlot.Head;
    }

    private Clothing(ArmorSlot slot) {
        _slot = slot;
    }

    public ArmorSlot Slot {
        get { return _slot; }
        set { _slot = value; }
    }
}

public enum ArmorSlot {
    Head,
    Shoulders,
    UpperBody,
    Torso,
    Legs,
    Hands,
    Feed,
    Back
}
