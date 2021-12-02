using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public virtual bool OnApply(Vector2 worldPoint)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    public virtual bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, GameItem item)
    {
        Debug.LogWarning("OnApplytoTileMap is not implemented");
        return true;
    }

    public virtual void OnItemUsed(GameItem usedItem, GameItemContainer inventory)
    {
        Debug.LogWarning("OnItemUsed is not implemented");
    }

    public virtual void OnToolbarSelectedChanged(GameItem gameItem, bool selected = true)
    {
        Debug.LogWarning("OnToolbarSelected is not implemented");
    }
}
