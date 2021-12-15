using System;
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
        VisualizeMap();
    }

    void OnDestroy()
    {
        for (int i = 0; i < placeableObjectsContainer.placeableObjects.Count; i++)
        {
            PlaceableObject placeableObject = placeableObjectsContainer.placeableObjects[i];
            if (placeableObject.targetObject == null) { continue; }

            IPersistant persistant = placeableObject.targetObject.GetComponent<IPersistant>();
            if (persistant != null)
            {
                string jsonString = persistant.Read();
                placeableObject.objectState = jsonString;
            }

            placeableObjectsContainer.placeableObjects[i].targetObject = null;
        }
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        PlaceableObject placeableObject = placeableObjectsContainer.Get(gridPosition);
        if (placeableObject == null)
        {
            return;
        }

        ItemSpawnManager.instance.SpawnItem(targetTilemap.GetCellCenterWorld(gridPosition), placeableObject.placedItem, 1);
        Destroy(placeableObject.targetObject.gameObject);
        placeableObjectsContainer.Remove(placeableObject);
    }

    void VisualizeMap()
    {
        for (int i = 0; i < placeableObjectsContainer.placeableObjects.Count; i++)
        {
            VisualizeItem(placeableObjectsContainer.placeableObjects[i]);
        }
    }

    void VisualizeItem(PlaceableObject placeableObject)
    {
        GameObject go = Instantiate(placeableObject.placedItem.itemPrefab);
        go.transform.parent = transform;

        Vector3 position = targetTilemap.GetCellCenterWorld(placeableObject.positionOnGrid);
        position -= Vector3.forward * 0.1f;
        go.transform.position = position;

        IPersistant persistant = go.GetComponent<IPersistant>();
        if (persistant != null)
        {
            persistant.Load(placeableObject.objectState);
        }

        placeableObject.targetObject = go.transform;
    }

    public bool Check(Vector3Int position)
    {
        return placeableObjectsContainer.Get(position) != null;
    }

    public void Place(GameItem item, Vector3Int positionOnGrid)
    {
        if (Check(positionOnGrid)) { return; }
        PlaceableObject placeableObject = new PlaceableObject(item, positionOnGrid);
        VisualizeItem(placeableObject);
        placeableObjectsContainer.placeableObjects.Add(placeableObject);
    }
}
