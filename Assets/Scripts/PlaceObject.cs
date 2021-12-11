using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Place Object")]
public class PlaceObject : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, GameItem item)
    {
        if (tileMapReadController.placeableObjectsManager.Check(gridPosition)) { return false; }
        tileMapReadController.placeableObjectsManager.Place(item, gridPosition);
        return true;
    }

    public override void OnItemUsed(GameItem usedItem, GameItemContainer inventory)
    {
        Debug.Log("MATT got here");
        inventory.Remove(usedItem);
    }
}
