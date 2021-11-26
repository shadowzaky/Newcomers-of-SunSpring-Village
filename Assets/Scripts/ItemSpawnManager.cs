using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject pickUpItemPrefab;

    public void SpawnItem(Vector3 position, GameItem item, int quantity)
    {
        GameObject o = Instantiate(pickUpItemPrefab, position, Quaternion.identity);
        o.GetComponent<PickUpItem>().Set(item, quantity);
    }
}
