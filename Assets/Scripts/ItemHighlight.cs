using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemHighlight : MonoBehaviour
{
    public Vector3Int cellPosition;
    Vector3 targetPosition;
    public Tilemap targetTilemap;
    SpriteRenderer spriteRenderer;

    bool canSelect;
    bool show;

    public bool CanSelect
    {
        set 
        {
            canSelect = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    public bool Show
    {
        set 
        {
            show = value;
            gameObject.SetActive(canSelect && show);
        }
    }

    void Update()
    {
        targetPosition = targetTilemap.GetCellCenterWorld(cellPosition);
        transform.position = targetPosition;
    }

    internal void Set(Sprite icon)
    {
        if (spriteRenderer == null) { spriteRenderer = GetComponent<SpriteRenderer>(); }

        spriteRenderer.sprite = icon;
    }
}
