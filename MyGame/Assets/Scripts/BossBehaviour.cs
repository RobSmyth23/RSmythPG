using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour, iHealth
{
    public GameObject portalStone;
    int health = 800;
    public Transform playerTransform;
    private Animator animator;
    [SerializeField] private float secondsDelay = 7.0f;
    [SerializeField] private float detectionRadius = 10f;
    public Vector3 originalPosition;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;

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

        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Face the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            // Return to original position
            agent.SetDestination(originalPosition);
        }

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            animator.SetTrigger("die");

            StartCoroutine(delayTimer(secondsDelay));
            ActivateSphere();
            //Some celebration effect / on screen text maybe
            Debug.Log("CELEBRATION BOSS DEFEATED");
        }
    }
    void ActivateSphere()
    {
        if (portalStone != null)
        {
            portalStone.SetActive(true); // Activate the sphere
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (health >= 400)
            {
                Debug.Log("Boss Entering attack phase 1");
                animator.SetTrigger("attack_01");
            }
            else if (health <= 399 && health >= 200)
            {
                Debug.Log("Boss Entering attack phase 2");
                animator.SetTrigger("attack_02");
            }
            else if (health > 0 && health < 200)
            {
                Debug.Log("Boss Entering attack phase 3");
                animator.SetTrigger("attack_03");
            }
            else
                return;   //left off here!
        }
    }
    
    IEnumerator delayTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Boss dead! Starting destroy object timer!");
        Destroy(gameObject);
    }



}
