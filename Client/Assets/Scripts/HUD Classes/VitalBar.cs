/// <summary>
/// vital bar.cs
/// Glenn Latomme (glenn.latomme@gmail.com)
/// 
/// for showing the players vital bar or a mob
/// </summary>
using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
    public bool _isPlayervitalBar;

    private int _maxBarLength;         // The max length of the vital bar
    private int _curBarLength;          // the current length of the vital bar
    private GUITexture _display;

    void Awake() {
        _display = gameObject.GetComponent<GUITexture>();
    }

    void Start() {
        _maxBarLength = (int)_display.pixelInset.width;

        OnEnable();
    }

    /// <summary>
    /// this is called hen the game object is enabled
    /// </summary>
    public void OnEnable() {
        if (_isPlayervitalBar) {
            Messenger<int, int>.AddListener("player vital update", OnChangevitalBarSize);
        } else {
            ToggleDisplay(false);
            Messenger<int, int>.AddListener("mob vital update", OnChangevitalBarSize);
            Messenger<bool>.AddListener("show mob vitalbar", ToggleDisplay);
        }
    }

    /// <summary>
    /// this is called hen the game object is disabled
    /// </summary>
    public void OnDisable() {
        if (_isPlayervitalBar)
            Messenger<int, int>.RemoveListener("player vital update", OnChangevitalBarSize);
        else {
            Messenger<int, int>.RemoveListener("mob vital update", OnChangevitalBarSize);
            Messenger<bool>.RemoveListener("show mob vitalbar", ToggleDisplay);
        }
    }

    /// <summary>
    /// This wil calculate the total size of the vitalbar in relation with the maxvital.
    /// </summary>
    /// <param name="curvital">The current vital.</param>
    /// <param name="maxvital">The max vital.</param>
    public void OnChangevitalBarSize(int curvital, int maxvital) {
        _curBarLength = (int)((curvital / (float)maxvital) * _maxBarLength);     // calculates the current vital bar length, based on the players vital %

        _display.pixelInset = CalculatePosition();
    }

    /// <summary>
    /// Setthign the vitalbar to the player or Mob
    /// </summary>
    /// <param name="b">Is player?</param>
    public void SetPlayervital(bool b) {
        _isPlayervitalBar = b;
    }

    public Rect CalculatePosition() {
        float yPos = _display.pixelInset.y / 2 - 10;
        float xPos = 0;

        if (!_isPlayervitalBar) {
            xPos = (_maxBarLength - _curBarLength) - (_maxBarLength / 4 + 10);
            return new Rect(xPos, yPos, _curBarLength, _display.pixelInset.height);
        } else
            return new Rect(_display.pixelInset.x, yPos, _curBarLength, _display.pixelInset.height);
    }

    /// <summary>
    /// Enable or disable the vital bar.
    /// </summary>
    /// <param name="show">Either show it or not.</param>
    private void ToggleDisplay(bool show) {
        _display.enabled = show;
    }
}

