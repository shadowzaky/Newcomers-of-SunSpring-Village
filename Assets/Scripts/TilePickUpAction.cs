using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Harvest")]
public class TilePickUpAction : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, GameItem item)
    {
        tileMapReadController.cropsManager.PickUp(gridPosition);
        tileMapReadController.placeableObjectsManager.PickUp(gridPosition);
        return true;
    }
}
