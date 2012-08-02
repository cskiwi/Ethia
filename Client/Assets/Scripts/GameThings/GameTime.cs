using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
    public enum TimeOfDay {
        Idle,
        SunRise,
        SunSet
    }

    public Transform[] sun;
    private Sun[] _sunScript;

    public float sunRise;
    public float sunSet;
    public float skyboxBlendMidifier;

    public Color ambLightMax;
    public Color ambLightMin;

    private const float SECOND = 1;
    private const float MINUTE = SECOND * 60;
    private const float HOUR = MINUTE * 60;
    private const float DAY = HOUR * 24;

    private const float DAY_CYCLE_IN_MINUTES = 1;
    private const float DAY_CYCLE_IN_SECONDS = DAY_CYCLE_IN_MINUTES * MINUTE;
    private const float DEGREES_PER_SECOND = 360 / DAY;

    private float _degreeRotation;
    private float _timeOfDay;

    private TimeOfDay _tod;
    private float _noonTime;
    private float _morningLength;
    private float _eveningLength;

    public float morningLight;
    public float nightLight;
    private bool _isMorning = false;

    void Start() {
        _tod = GameTime.TimeOfDay.Idle;
        _sunScript = new Sun[sun.Length];

        RenderSettings.skybox.SetFloat("_Blend", 0);

        for (int i = 0; i < sun.Length; i++) {
            Sun temp = sun[i].GetComponent<Sun>();

            if (temp == null) {
                Debug.LogWarning("Sun script not found on sun " + i + ", adding a default script");
                sun[i].gameObject.AddComponent<Sun>();
                _sunScript[i] = sun[i].GetComponent<Sun>();
            } else {
                _sunScript[i] = temp;
            }
        }
        _timeOfDay = 0;
        _degreeRotation = DEGREES_PER_SECOND * DAY / DAY_CYCLE_IN_SECONDS;

        sunRise *= DAY_CYCLE_IN_SECONDS;
        sunSet *= DAY_CYCLE_IN_SECONDS;
        _noonTime = DAY_CYCLE_IN_SECONDS / 2;

        _morningLength = _noonTime - sunRise;
        _eveningLength = sunSet - _noonTime;

        morningLight *= DAY_CYCLE_IN_SECONDS;
        nightLight *= DAY_CYCLE_IN_SECONDS;

        // setup the lights to start at min intensity
        SetUpLighting();
    }

    void Update() {
        _timeOfDay += Time.deltaTime;

        if (_timeOfDay > DAY_CYCLE_IN_SECONDS)
            _timeOfDay -= DAY_CYCLE_IN_SECONDS;

        for (int i = 0; i < sun.Length; i++) {
            sun[i].Rotate(new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);
        }

        if (!_isMorning && _timeOfDay > morningLight && _timeOfDay < nightLight) {
            _isMorning = true;
            Messenger<bool>.Broadcast("Morning Light Time", true);
        } else if (_isMorning && _timeOfDay > nightLight) {
            _isMorning = false;
            Messenger<bool>.Broadcast("Morning Light Time", false);
        }

        if (_timeOfDay > sunRise && _timeOfDay < _noonTime)
            AdjustLLighting(true);
        else if (_timeOfDay > _noonTime && _timeOfDay < sunSet)
            AdjustLLighting(false);

        if (_timeOfDay > sunRise && _timeOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend") < 1) {
            _tod = GameTime.TimeOfDay.SunRise;
            BlendSkybox();
        } else if (_timeOfDay > sunSet && RenderSettings.skybox.GetFloat("_Blend") > 0) {
            _tod = GameTime.TimeOfDay.SunSet;
            BlendSkybox();
        } else {
            _tod = GameTime.TimeOfDay.Idle;
        }
    }

    private void BlendSkybox() {
        float temp = 0;

        switch (_tod) {
            case TimeOfDay.SunRise:
                temp = (_timeOfDay - sunRise) / skyboxBlendMidifier;
                break;
            case TimeOfDay.SunSet:
                temp = (_timeOfDay - sunSet) / skyboxBlendMidifier;
                temp = 1 - temp;
                break;
        }

        RenderSettings.skybox.SetFloat("_Blend", temp);
    }

    private void SetUpLighting() {
        RenderSettings.ambientLight = ambLightMin;

        for (int i = 0; i < _sunScript.Length; i++)
            if (_sunScript[i].giveLight)
                sun[i].GetComponent<Light>().intensity = _sunScript[i].minLightBrightness;
    }

    private void AdjustLLighting(bool brighten) {
        float pos = 0;
        if (brighten) {
            pos = (_timeOfDay - sunRise) / _morningLength;
        } else {
            pos = (sunSet - _timeOfDay) / _eveningLength;
        }

        RenderSettings.ambientLight = new Color(ambLightMin.r + ambLightMax.r * pos,
                                                ambLightMin.g + ambLightMax.g * pos,
                                                ambLightMin.b + ambLightMax.b * pos);

        for (int i = 0; i < _sunScript.Length; i++) {
            if (_sunScript[i].giveLight) {
                _sunScript[i].GetComponent<Light>().intensity = _sunScript[i].maxLightBrightness * pos;
            }
        }
    }
}