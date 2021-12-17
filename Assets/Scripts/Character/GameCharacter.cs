using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor4D.Common.CharacterScripts;
using HeroEditor4D.Common.Enums;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EquipWeapon(GameItem gameItem)
    {
        var character4D = GameManager.instance.character.GetComponent<Character4D>();
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

    public void UnequipWeapon()
    {
        var character4D = GameManager.instance.character.GetComponent<Character4D>();
        character4D.UnEquip(EquipmentPart.MeleeWeapon1H);
        character4D.UnEquip(EquipmentPart.MeleeWeapon2H);
    }
}
