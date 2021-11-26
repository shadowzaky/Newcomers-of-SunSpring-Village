using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public Text text;

    int myIndex;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    public void Set(ItemSlot itemSlot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = itemSlot.item.icon;
        if (itemSlot.item.stackable) 
        {
            text.gameObject.SetActive(true);
            text.text = itemSlot.count.ToString();
        }
        else
        {
            text.gameObject.SetActive(false);
        }
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        text.text = "";
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameItemContainer inventory = GameManager.instance.inventoryContainer;
        GameManager.instance.dragAndDropController.OnClick(inventory.slots[myIndex]);
        transform.parent.GetComponent<InventoryPanel>().Show();
    }
}
