using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    public float speed = 7.5f;
    public float pickUpDistance = 2f;
    public float ttl = 50f;

    GameItem item;
    int quantity = 1;

    void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    public void Set(GameItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.icon;
    }

    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (distance < 0.1f)
        {
            //*TODO* Should be moved into specified controller rather than being checked here.
            if (GameManager.instance.inventoryContainer != null) 
            {
                GameManager.instance.inventoryContainer.Add(item, quantity);
            }
            else 
            {
                Debug.LogWarning("No inventory container attached to game manager.");
            }
            Destroy(gameObject);
        }
    }
}
