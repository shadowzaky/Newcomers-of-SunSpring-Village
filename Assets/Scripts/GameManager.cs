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
    public GameCharacter character;
    public GameItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
    public DayTimeController timeController;
    public DialogueSystem dialogueSystem;
    public PlaceableObjectsReferenceManager placeableObjects;
    public GameItemList itemDB;
}
