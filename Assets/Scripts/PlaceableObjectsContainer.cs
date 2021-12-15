using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlaceableObject
{
    public GameItem placedItem;
    public Transform targetObject;
    public Vector3Int positionOnGrid;
    /// <summary>
    /// JSON string which contains the state of the object
    /// </summary>
    public string objectState;

    public PlaceableObject(GameItem item, Vector3Int pos)
    {
        placedItem = item;
        positionOnGrid = pos;
    }
}

[CreateAssetMenu(menuName = "Data/Placeable Objects Container")]
public class PlaceableObjectsContainer : ScriptableObject
{
    public List<PlaceableObject> placeableObjects;

    public PlaceableObject Get(Vector3Int position)
    {
        return placeableObjects.Find(x => x.positionOnGrid == position);
    }

    internal void Remove(PlaceableObject placeableObject)
    {
        placeableObjects.Remove(placeableObject);
    }
}
