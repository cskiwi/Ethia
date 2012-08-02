using UnityEngine;
using System.Collections;

[AddComponentMenu("Enviroments/Sun")]
public class Sun : MonoBehaviour {
    public float maxLightBrightness;
    public float minLightBrightness;

    public float maxFlareBrightness;
    public float minFlareBrightness;

    public bool giveLight = false;

    void Start() {
        giveLight = GetComponent<Light>() != null;      // if has a light check the give light
    }

}
