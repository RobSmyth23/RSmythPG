using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour, iHealth
{
    int health = 800;
    public Transform playerTransform;
    private Animator animator;
    [SerializeField] private float secondsDelay = 7.0f;
    [SerializeField] private float detectionRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            if (distance < detectionRadius)
            {
                // Example trigger (e.g., enter alert state)
                animator.SetTrigger("alert");
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            animator.SetTrigger("die");

            StartCoroutine(delayTimer(secondsDelay));

            //Some celebration effect / on screen text maybe
            Debug.Log("CELEBRATION BOSS DEFEATED");
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (health >= 400)
            {
                animator.SetTrigger("attack_01");
            }
            else if (health >= 200 && health <= 399)
            {
                animator.SetTrigger("attack_02");
            }
            else if (health > 0 && health < 200)
            {
                animator.SetTrigger("attack_03");
            }
            else
                return;   //left off here!
        }
    }
    
    IEnumerator delayTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
