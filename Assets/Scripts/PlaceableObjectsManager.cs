using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObjectsManager : MonoBehaviour
{
    public PlaceableObjectsContainer placeableObjectsContainer;
    public Tilemap targetTilemap;

    void Start()
    {
        GameManager.instance.GetComponent<PlaceableObjectsReferenceManager>().placeableObjectsManager = this;
    }

    public void Place(GameItem item, Vector3Int positionOnGrid)
    {
        GameObject go = Instantiate(item.itemPrefab);
        Vector3 position = targetTilemap.GetCellCenterWorld(positionOnGrid);
        position -= Vector3.forward * 0.1f;
        go.transform.position = position;
        placeableObjectsContainer.placeableObjects.Add(new PlaceableObject(
            item,
            go.transform,
            positionOnGrid
        ));
    }
}
