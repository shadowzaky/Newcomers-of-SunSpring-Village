using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public GameItemContainer inventory;
    public List<InventoryButton> buttons;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        Show();
    }

    void OnEnable()
    {
        Show();
    }

    void LateUpdate()
    {
        if (inventory?.isDirty == true)
        {
            Show();
            inventory.isDirty = false;
        }
    }

    private void SetIndex()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public virtual void Show()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }

    public virtual void OnClick(int id)
    {

    }
}
