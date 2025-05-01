using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPickup : MonoBehaviour
{
    public int arrowAmount = 5; // Amount of arrows the chest gives

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player is near
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddArrows(arrowAmount); // Add arrows to inventory
                Debug.Log($"Player collected {arrowAmount} arrows!");
                Destroy(gameObject); // Remove chest after pickup
            }
        }
    }
}
