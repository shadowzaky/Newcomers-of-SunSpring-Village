using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileData> tileDatas;
    public CropsManager cropsManager;

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;
        if (mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            worldPosition = position;
        }

        return tilemap.WorldToCell(worldPosition);
    }

    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        return tilemap.GetTile(gridPosition);
    }
}
