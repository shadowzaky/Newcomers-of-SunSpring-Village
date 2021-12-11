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
}
