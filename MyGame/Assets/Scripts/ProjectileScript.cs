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
        rb.velocity = speed * (Vector3.up + 4 * transform.forward).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("You hit me!!");
        iHealth thingIHit = collision.gameObject.GetComponent<iHealth>();
        if (thingIHit != null)
        {
            thingIHit.TakeDamage(20);
        }
    }
    internal void ImShootingYou(charMovementScript charMovementScript)
    {
    

      
      
    }
}
