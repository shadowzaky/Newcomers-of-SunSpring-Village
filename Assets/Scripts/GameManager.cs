using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject player;
    public GameItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
}
