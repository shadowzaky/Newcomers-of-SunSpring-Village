using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Action/Plow")]
public class PlowTile : ToolAction
{
    public List<TileBase> canPlow;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        TileBase tileToPlow = tileMapReadController.GetTileBase(gridPosition);
        if (canPlow.Contains(tileToPlow))
        {
            tileMapReadController.cropsManager.Plow(gridPosition);
        }
        return false;
    }
}
