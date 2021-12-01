using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Seed Tile")]
public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController)
    {
        if (tileMapReadController.cropsManager.Check(gridPosition))
        {
            tileMapReadController.cropsManager.Seed(gridPosition);
            return true;
        }
        return false;
    }

    public override void OnItemUsed(GameItem usedItem, GameItemContainer inventory)
    {
        inventory.Remove(usedItem);
    }
}
