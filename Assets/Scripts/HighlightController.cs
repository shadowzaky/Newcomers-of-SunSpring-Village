using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    public GameObject highlighter;

    GameObject currentTarget;

    public void Highlight(GameObject target)
    {
        if (currentTarget != target)
        {
            Vector3 position = target.transform.position;
            Highlight(position);
            currentTarget = target;
        }
    }

    private void Highlight(Vector3 position)
    {
        highlighter.SetActive(true);
        highlighter.transform.position = position;
    }

    public void Hide()
    {
        currentTarget = null;
        highlighter.SetActive(false);
    }
}
