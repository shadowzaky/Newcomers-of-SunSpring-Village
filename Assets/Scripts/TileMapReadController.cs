using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    public Tilemap tilemap;
    public List<TileData> tileDatas;

    Dictionary<TileBase, TileData> dataFromTiles;
    void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tileBase in tileData.tiles)
            {
                dataFromTiles.Add(tileBase, tileData);
            }
        }
    }

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
        TileBase tile = tilemap.GetTile(gridPosition);

        return null;
    }

    public TileData GetTileData(TileBase tileBase)
    {
        return dataFromTiles[tileBase];
    }
}
