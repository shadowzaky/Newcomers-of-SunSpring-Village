using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameItem")]
public class GameItem : ScriptableObject
{
    public string Name;
    public int id;
    public bool stackable;
    public Sprite icon;
    public ToolAction onAction;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
    public Crop crop;
    public bool itemHighlight;
    public GameObject itemPrefab;
}
