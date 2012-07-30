using UnityEngine;
using System.Collections;

public class Player {
    private static string _name;
    public Player() {
        // for skipping the login part
        _name = "Kiwi";
    }

    public string Name {
        set { _name = value; }
        get { return _name; }
    }
}
