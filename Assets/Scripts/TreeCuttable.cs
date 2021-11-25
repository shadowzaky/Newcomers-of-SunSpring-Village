using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    public GameObject pickUpDrop;
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
            GameObject wood = Instantiate(pickUpDrop);
            wood.transform.position = position;
        }
        Destroy(gameObject);
    }
}
