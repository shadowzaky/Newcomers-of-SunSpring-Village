using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VerticalStatusBar : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Slider bar;

    public void Set(int current, int max)
    {
        bar.maxValue = max;
        bar.value = current;

        text.text = current.ToString() + " / " + max.ToString();
    }
}
