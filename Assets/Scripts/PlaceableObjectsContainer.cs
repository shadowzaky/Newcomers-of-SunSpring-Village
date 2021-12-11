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

    public PlaceableObject(GameItem item, Transform target, Vector3Int pos)
    {
        placedItem = item;
        targetObject = target;
        positionOnGrid = pos;
    }
}

[CreateAssetMenu(menuName = "Data/Placeable Objects Container")]
public class PlaceableObjectsContainer : ScriptableObject
{
    public List<PlaceableObject> placeableObjects;
}
