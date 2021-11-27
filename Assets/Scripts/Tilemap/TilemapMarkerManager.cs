using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapMarkerManager : MonoBehaviour
{
    public Tilemap targetTilemap;
    public TileBase tile;

    public Vector3Int markedCellPosition;
    Vector3Int oldCellPosition;
    bool show;

    void Update()
    {
        if (show)
        {
            targetTilemap.SetTile(oldCellPosition, null);
            targetTilemap.SetTile(markedCellPosition, tile);
            oldCellPosition = markedCellPosition;
        }
    }

    internal void Show(bool selectable)
    {
        show = selectable;
        targetTilemap.gameObject.SetActive(show);
    }
}
