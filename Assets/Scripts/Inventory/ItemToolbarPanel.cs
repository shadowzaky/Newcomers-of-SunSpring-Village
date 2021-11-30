using System.Collections;
using System.Collections.Generic;
using HeroEditor4D.Common.Enums;
using UnityEngine;

public class ItemToolbarPanel : ItemPanel
{
    public ToolbarController toolbarController;
    int currentSelectedTool = 0;

    void Start()
    {
        Init();
        toolbarController.onChange += Hightlight;
        toolbarController.onChange += EquipWeapon;
        Hightlight(currentSelectedTool);
    }

    public override void OnClick(int id)
    {
        toolbarController.Set(id);
        Hightlight(id);
    }
    public void Hightlight(int id)
    {
        buttons[currentSelectedTool].Hightlight(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].Hightlight(true);
    }

    public void EquipWeapon(int id)
    {
        var character4D = GameManager.instance.character4D;
        var weaponName = toolbarController.GetItem?.icon.name;
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
            character4D.UnEquip(EquipmentPart.MeleeWeapon1H);
            character4D.UnEquip(EquipmentPart.MeleeWeapon2H);
        }
    }
}
