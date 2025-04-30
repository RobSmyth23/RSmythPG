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

    public void CollectArrow(int amount)
    {
        currentArrows = Mathf.Min(currentArrows + amount, maxArrows);
        Debug.Log($"You Collected arrows! Current arrow count: {currentArrows}");
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
}

