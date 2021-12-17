using System;
using Assets.HeroEditor4D.Common.CharacterScripts;
using HeroEditor4D.Common.Enums;
using UnityEngine;

[Serializable]
public class Stat
{
    public int currentValue;
    public int maxValue;

    public Stat(int current, int max)
    {
        currentValue = current;
        maxValue = max;
    }

    internal void Subtract(int amount)
    {
        currentValue -= amount;
    }

    internal void Add(int amount)
    {
        currentValue += amount;
        if (currentValue > maxValue)
        {
            SetToMax();
        }
    }

    internal void SetToMax()
    {
        currentValue = maxValue;
    }
}

public class GameCharacter : MonoBehaviour
{
    public Stat hp;
    public VerticalStatusBar hpBar;
    public Stat stamina;
    public VerticalStatusBar staminaBar;
    public bool isDead;
    public bool isExhausted;

    void Start()
    {
        UpdateHPBar();
        UpdateStaminaBar();
    }

    void UpdateHPBar()
    {
        hpBar.Set(hp.currentValue, hp.maxValue);
    }

    void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.currentValue, stamina.maxValue);
    }

    public void TakeDamage(int amount)
    {
        hp.Subtract(amount);
        if (hp.currentValue <= 0)
        {
            isDead = true;
        }
        UpdateHPBar();
    }

    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHPBar();
    }

    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHPBar();
    }

    public void GetTired(int amount)
    {
        stamina.Subtract(amount);
        if (stamina.currentValue <= 0)
        {
            isExhausted = true;
        }
        UpdateStaminaBar();
    }

    public void Rest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }

    public void FullRest()
    {
        stamina.SetToMax();
        UpdateStaminaBar();
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
