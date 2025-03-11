using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        print("Thats a hit!"+collision.gameObject.name);

        // Check if the javelin hit the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
           
            // Destroy the javelin object
            Destroy(gameObject);
        }
    }
}
