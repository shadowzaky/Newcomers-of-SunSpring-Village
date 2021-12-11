using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileData> tileDatas;
    public CropsManager cropsManager;
    public PlaceableObjectsReferenceManager placeableObjectsManager;

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }
        if (tilemap == null)
        {
            return Vector3Int.zero;
        }

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
        if (tilemap == null)
        {
            tilemap = GameObject.Find("BaseTilemap").GetComponent<Tilemap>();
        }
        if (tilemap == null)
        {
            return null;
        }

        return tilemap.GetTile(gridPosition);
    }
}
