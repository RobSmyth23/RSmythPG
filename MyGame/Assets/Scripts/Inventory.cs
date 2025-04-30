using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxArrows = 5;
    private int currentArrows = 0;

    public bool HasArrows()
    {
        return currentArrows > 0;
    }

    public void AddArrows(int amount)
    {
        currentArrows += amount;
        Debug.Log($"Arrows in inventory: {currentArrows}");
    }

    public void UseArrow()
    {
        if (HasArrows())
        {
            currentArrows--;
            Debug.Log($"SHOOT! Remaining arrows: {currentArrows}");
        }
        else
        {
            Debug.Log("No arrows left!");
        }
    }
    public int GetArrowCount()
    {
        return currentArrows; // Return the current arrow count
    }
}

