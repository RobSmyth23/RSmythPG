using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    public int arrowAmount = 1;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.gameObject.name}"); // Debug what collides

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected for arrow pickup!");

            Inventory playerInventory = other.GetComponent<Inventory>();

            if (playerInventory != null)
            {
                playerInventory.AddArrows(1);
                Debug.Log("Arrow added to inventory!");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Player does not have an Inventory component!");
            }
        }
    }

}
