using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    public GameObject pickUpDrop;
    public GameItem item;
    public int quantityInOneDrop = 1;
    public int dropCount = 5;
    public float spread = 2.5f;

    public override void Hit()
    {
        while (dropCount > 0)
        {
            dropCount--;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(position, item, quantityInOneDrop);
        }
        Destroy(gameObject);
    }
}
