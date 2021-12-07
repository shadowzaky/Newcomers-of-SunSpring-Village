using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    public Color unTintedColor;
    public Color tintedColor;
    public float speed = 0.5f;
    Image image;
    float fadeAlpha;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void TintScreen()
    {
        StopAllCoroutines();
        fadeAlpha = 0f;
        StartCoroutine(FadeInOutImageColor(unTintedColor, tintedColor));
    }

    public void UnTintScreen()
    {
        StopAllCoroutines();
        fadeAlpha = 0f;
        StartCoroutine(FadeInOutImageColor(tintedColor, unTintedColor));
    }

    private IEnumerator FadeInOutImageColor(Color start, Color end)
    {
        while (fadeAlpha < 1f)
        {
            fadeAlpha += Time.deltaTime * speed;
            fadeAlpha = Mathf.Clamp(fadeAlpha, 0, 1f);
            Color c = image.color;
            c = Color.Lerp(start, end, fadeAlpha);
            image.color = c;
            yield return new WaitForEndOfFrame();
        }
    }
}
