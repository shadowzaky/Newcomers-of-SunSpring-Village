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

    public override void OnToolbarSelectedChanged(GameItem gameItem, bool selected = true)
    {
        if (selected)
        {
            EquipWeapon(gameItem);
        } 
        else 
        {
            UnequipWeapon();
        }
        
    }

    public void EquipWeapon(GameItem gameItem)
    {
        var character4D = GameManager.instance.character4D;
        var weaponName = gameItem?.icon.name;
        if (weaponName != null)
        {
            var weapon = character4D.SpriteCollection.MeleeWeapon1H.Find(x => x.Name == weaponName);
            if (weapon != null)
            {
                character4D.Equip(weapon, EquipmentPart.MeleeWeapon1H);
            }
            else
            {
                weapon = character4D.SpriteCollection.MeleeWeapon2H.Find(x => x.Name == weaponName);
                character4D.Equip(weapon, EquipmentPart.MeleeWeapon2H);
            }
        }
        else
        {
            UnequipWeapon();
        }
    }

    private void UnequipWeapon()
    {
        var character4D = GameManager.instance.character4D;
        character4D.UnEquip(EquipmentPart.MeleeWeapon1H);
        character4D.UnEquip(EquipmentPart.MeleeWeapon2H);
    }
}
