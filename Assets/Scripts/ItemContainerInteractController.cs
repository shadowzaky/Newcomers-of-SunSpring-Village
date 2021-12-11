using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerInteractController : MonoBehaviour
{
    GameItemContainer targetItemContainer;
    InventoryController inventoryController;
    public ItemContainerPanel itemContainerPanel;
    Transform openedChest;
    public float maxDistance = 2.5f;

    private void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    void Update()
    {
        if (openedChest != null)
        {
            float distance = Vector2.Distance(openedChest.position, transform.position);
            if (distance > maxDistance)
            {
                openedChest.GetComponent<LootContainerInteract>().Close();
            }
        }
    }

    public void Open(GameItemContainer itemContainer, Transform targetChest)
    {
        targetItemContainer = itemContainer;
        itemContainerPanel.inventory = targetItemContainer;
        inventoryController.Open();
        itemContainerPanel.gameObject.SetActive(true);
        openedChest = targetChest;
    }

    public void Close()
    {
        inventoryController.Close();
        itemContainerPanel.gameObject.SetActive(false);
        openedChest = null;
    }
}
