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
    public Light2D playerLight;
    public float playerNightLightIntensity = 0.6f;

    float time;
    int dayCount;

    float Hours
    {
        get { return time / 3600f;}
    }

    float Minutes
    {
        get { return time % 60f;}
    }

    void Update()
    {
        time += Time.deltaTime * timeScale;
        text.text = Hours.ToString("00") + ":" + Minutes.ToString("00");

        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        gobalLight.color = c;
        playerLight.intensity = v * playerNightLightIntensity;
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
