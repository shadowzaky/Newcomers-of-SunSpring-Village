using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f; // 15 minutes in seconds
    public Color nightLightColor;
    public AnimationCurve nightTimeCurve;
    public Color dayLightColor = Color.white;
    public Text text;
    public float timeScale = 60f;
    public float startAtTime = 28800f;
    public UnityEngine.Rendering.Universal.Light2D gobalLight;
    public UnityEngine.Rendering.Universal.Light2D playerLight;
    public float playerNightLightIntensity = 0.6f;
    public List<TimeAgent> agents;

    int oldPhase = 0;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void OnStart()
    {
        time = startAtTime;
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }

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
        HandleTimeLabelUpdate();
        HandleDayNightCycle();
        HandleNextDay();
        HandleTimeAgents();
    }

    private void HandleTimeLabelUpdate()
    {
        time += Time.deltaTime * timeScale;
        text.text = Hours.ToString("00") + ":" + Minutes.ToString("00");
    }

    private void HandleDayNightCycle()
    {
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        gobalLight.color = c;
        playerLight.intensity = v * playerNightLightIntensity;
        
    }

    private void HandleNextDay()
    {
        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    
    private void HandleTimeAgents()
    {
        int phase = (int)(time / phaseLength);
        if (oldPhase != phase)
        {
            oldPhase = phase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }
    }

    private void NextDay()
    {
        time = 0;
        dayCount++;
    }
}
