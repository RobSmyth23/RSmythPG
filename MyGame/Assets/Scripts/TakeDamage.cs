using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeDamage : MonoBehaviour
{
    int Health = 400;
    internal void IHitYou()
    {
        Health -= 20;

        if (Health < 100)
        {
            GetComponentInChildren<Renderer>().material.color = Color.magenta;
        }
        if (Health < 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Death Screen");
        }
        throw new NotImplementedException();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
