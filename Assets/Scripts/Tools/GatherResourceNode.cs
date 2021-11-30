using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor4D.Common.CharacterScripts;
using HeroEditor4D.Common.Enums;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Ore
}

[CreateAssetMenu(menuName = "Data/Tool action/Gather Resource Node")]
public class GatherResourceNode : ToolAction
{
    public float sizeOfInteractableArea = 1f;
    public List<ResourceNodeType> canHitNodesOfType;

    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);
        foreach (Collider2D collider in colliders)
        {
            ToolHit hit = collider.GetComponent<ToolHit>();
            if (hit != null && hit.CanBeHit(canHitNodesOfType))
            {
                hit.Hit();
                
                return true;
            }
        }

        return false;
    }
}
