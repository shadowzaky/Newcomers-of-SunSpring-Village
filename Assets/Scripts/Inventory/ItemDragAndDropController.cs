using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDropController : MonoBehaviour
{
    public ItemSlot itemSlot;
    public GameObject dragItemIcon;

    RectTransform iconTransform;

    void Start()
    {
        itemSlot = new ItemSlot();
        iconTransform = dragItemIcon.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (dragItemIcon.activeInHierarchy == true)
        {
            MoveDragIconWithMouse();
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    ItemSpawnManager.instance.SpawnItem(GetMousePosition(), itemSlot.item, itemSlot.count);
                    itemSlot.Clear();
                    dragItemIcon.SetActive(false);
                }
            }
        }
    }

    internal void RemoveItem(int count = 1)
    {
        if (itemSlot == null) { return; }
        if (itemSlot.item.stackable)
        {
            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            itemSlot.Clear();
        }
        UpdateIcon();
    }

    private Vector2 GetMousePosition()
    {
        return Input.mousePosition;
    }

    private void MoveDragIconWithMouse()
    {
        Vector2 mousePosition = GetMousePosition();
        iconTransform.position = new Vector3(mousePosition.x, mousePosition.y, 10);
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        if (this.itemSlot.item == null)
        {
            this.itemSlot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            if (itemSlot.item == this.itemSlot.item)
            {
                itemSlot.count += this.itemSlot.count;
                this.itemSlot.Clear();
            }
            else
            {
                GameItem item = itemSlot.item;
                int count = itemSlot.count;

                itemSlot.Copy(this.itemSlot);
                this.itemSlot.Set(item, count);
            }
        }
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (itemSlot.item == null)
        {
            dragItemIcon.SetActive(false);
        }
        else
        {
            dragItemIcon.SetActive(true);
            dragItemIcon.GetComponent<Image>().sprite = itemSlot.item.icon;
        }
    }

    public bool Check(GameItem item, int count = 1)
    {
        if (itemSlot == null || itemSlot.item == null) { return false; }
        if (item.stackable)
        {
            return itemSlot.item == item && itemSlot.count >= count;
        }
        return itemSlot.item == item;
    }
}
