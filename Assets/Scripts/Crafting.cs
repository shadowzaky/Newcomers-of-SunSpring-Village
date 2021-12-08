using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public GameItemContainer inventory;

    public void Craft(CraftingRecipe recipe)
    {
        if (inventory.CheckFreeSpace() == false)
        {
            Debug.Log("Not enough space to craft the item");
            return;
        }

        for (int i = 0; i < recipe.elemets.Count; i++)
        {
            if (!inventory.CheckItem(recipe.elemets[i]))
            {
                Debug.Log("Not enough " + recipe.elemets[i].item.name + " to craft the item.");
                return;
            }    
        }

        for (int i = 0; i < recipe.elemets.Count; i++)
        {
            inventory.Remove(recipe.elemets[i].item, recipe.elemets[i].count);
        }
        inventory.Add(recipe.output.item, recipe.output.count);
    }
}
