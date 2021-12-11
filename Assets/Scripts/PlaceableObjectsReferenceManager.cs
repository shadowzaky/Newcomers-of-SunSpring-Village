using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectsReferenceManager : MonoBehaviour
{
    public PlaceableObjectsManager placeableObjectsManager;

    public void Place(GameItem item, Vector3Int position)
    {
        placeableObjectsManager?.Place(item, position);
    }

    public bool Check(Vector3Int position)
    {
        return placeableObjectsManager?.Check(position) == true;
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        placeableObjectsManager?.PickUp(gridPosition);
    }
}
