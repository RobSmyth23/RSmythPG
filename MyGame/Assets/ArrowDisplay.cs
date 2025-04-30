using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowDisplay : MonoBehaviour
{
    public TextMeshProUGUI arrowText;
    public Inventory Inventory;

    private void Update()
    {
        // Update the arrow count in the UI
        if (Inventory != null && arrowText != null)
        {
            arrowText.text = "x" + Inventory.GetArrowCount().ToString();
        }
    }
}
