using UnityEngine;

public class Consumable : BuffItem {
    private Vital[] _vital;            // A list of vitals to heal
    private int[] _amountToHeal;       // The amount to heal each vital

    private float _buffTime;            // How long the buff lasts if there is one

    public Consumable() {
        Reset();
    }

    public Consumable(Vital[] vital, int[] amountToHeal, float buffTime) {
        _vital = vital;
        _amountToHeal = amountToHeal;
        _buffTime = buffTime;
    }

    public void Reset() {
        _buffTime = 0;

        for (int i = 0; i < _vital.Length; i++) {
            _vital[i] = new Vital();
            _amountToHeal[i] = 0;
        }
    }

    public int VitalCount() {
        return _vital.Length;
    }

    public Vital VitalAtIndex(int index) {
        if (index < _vital.Length + 1 && index > -1)
            return _vital[index];
        else
            return new Vital();
    }

    public int HealAtIndex(int index) {
        if (index < _amountToHeal.Length + 1 && index > -1)
            return _amountToHeal[index];
        else
            return 0;
    }

    public void SetVitalAt(int index, Vital vital) {
        if (index < _vital.Length + 1 && index > -1)
            _vital[index] = vital;
    }

    public void SetHealAt(int index, int heal) {
        if (index < _amountToHeal.Length + 1 && index > -1)
            _amountToHeal[index] = heal;
    }

    public void SetVitalAndHealAt(int index, Vital vital, int heal) {
        SetVitalAt(index, vital);
        SetHealAt(index, heal);
    }

    public float BuffTime {
        get { return _buffTime; }
        set { _buffTime = value; }
    }
}
