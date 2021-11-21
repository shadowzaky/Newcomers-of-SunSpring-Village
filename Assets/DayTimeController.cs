using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    public Color nightLightColor;
    public AnimationCurve nightTimeCurve;
    public Color dayLightColor = Color.white;
    public Text text;
    public float timeScale = 60f;
    public Light2D gobalLight;

    float time;
    int dayCount;

    float RawHours
    {
        get { return time / 3600f;}
    }
    float Hours
    {
        get { return Mathf.Floor(RawHours);}
    }

    float Minutes
    {
        get { return Mathf.Floor(time / 60f);}
    }

    void Update()
    {
        time += Time.deltaTime * timeScale;
        text.text = Hours.ToString("00") + ":" + Minutes.ToString("00");

        float v = nightTimeCurve.Evaluate(RawHours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        gobalLight.color = c;
        if (time > secondsInDay) 
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time = 0;
        dayCount++;
    }
}
