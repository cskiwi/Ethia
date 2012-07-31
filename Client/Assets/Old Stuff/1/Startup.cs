using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {
    public bool useFullscreen = false;
    private bool _useSecondMonitor = false;
    private int _width, _height;
    private string[] _arguments;

	// Use this for initialization
	void Awake () {
        _width = 0;
        _height = 0;

        // gonna rework this with an ini file, so you can save settings n stuff, 
        // but for now this works great :)
        _arguments = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < _arguments.Length; i++)
        {
            if (_arguments[i] == "-secondscreen")
                _useSecondMonitor = true;
            if (_arguments[i] == "-width")
                _width = int.Parse(_arguments[i + 1]);
            if (_arguments[i] == "-height")
                _height = int.Parse(_arguments[i + 1]);
        }

        if (_useSecondMonitor) 
            Screen.fullScreen = false;
        else if (useFullscreen)
                Screen.fullScreen = true;

        // setting the resolution to the max
        int CurrentWidth = 1920;
        int CurrentHeight = 1080;
        if (useFullscreen)
            Screen.SetResolution(CurrentWidth + _width, CurrentHeight + _height, false);
	}

    private void OnGUI()
    {
        string TextOutput;

        TextOutput = "First screen: \nWidth = " + Screen.width + ", Height = " + Screen.height + "\n";
        TextOutput += "Second screen: \nWidth = " + _width + ", Height = " + _height + "\n";
        for (int i = 0; i < _arguments.Length; i++)
        {
            TextOutput += _arguments[i] + "\n";
        }
        // GUI.Label(new Rect(0, 0, Screen.width, Screen.height), TextOutput);
    }
}
