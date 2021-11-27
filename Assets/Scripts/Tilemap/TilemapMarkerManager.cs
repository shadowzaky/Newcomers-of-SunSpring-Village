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

    void Update()
    {
        targetTilemap.SetTile(oldCellPosition, null);
        targetTilemap.SetTile(markedCellPosition, tile);
        oldCellPosition = markedCellPosition;
    }
}
