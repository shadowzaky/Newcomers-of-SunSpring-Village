using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameItem")]
public class GameItem : ScriptableObject
{
    public string Name;
    public bool stackable;
    public Sprite icon;
}
