using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor4D.Common.CharacterScripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject player;
    public Character4D character4D;
    public GameItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
    public DayTimeController timeController;
}
