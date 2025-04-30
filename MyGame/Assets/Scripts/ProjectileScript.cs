using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    float speed = 10f;
    Rigidbody rb;
    internal int check = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null && !rb.isKinematic) // Ensure Rigidbody is not kinematic
        {
            rb.velocity = speed * (Vector3.up + 4 * transform.forward).normalized;
        }
        else
        {
            Debug.LogWarning("Projectile Rigidbody is kinematic! Velocity won't be applied.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You hit me with a trigger!");

        iHealth thingIHit = other.GetComponent<iHealth>();
        if (thingIHit != null)
        {
            thingIHit.TakeDamage(50); // Apply damage
            Debug.Log("Boss took damage!");
            Destroy(gameObject); // Destroy the projectile after hitting
        }
    }
    internal void ImShootingYou(charMovementScript charMovementScript)
    {
    

      
      
    }
}
