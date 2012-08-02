using UnityEngine;

public class Jewelry : BuffItem {
    private JewelrySlot _slot;      // Store the slot the jewelry is in

    private Jewelry() {
        _slot = JewelrySlot.PocketItem;
    }

    private Jewelry(JewelrySlot slot) {
        _slot = slot;
    }

    public JewelrySlot Slot {
        get { return _slot; }
        set { _slot = value; }
    }
}

public enum JewelrySlot {
    EarRings,
    Necklaces,
    Bracelets,
    Rings,
    PocketItem
}
