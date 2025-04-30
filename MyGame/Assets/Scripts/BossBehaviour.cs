using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour, iHealth
{
    public GameObject portalStone;
    int health = 800;
    private Transform playerTransform;
    private Animator animator;
    private NavMeshAgent agent;

    [SerializeField] private float secondsDelay = 7.0f;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float attackRange = 5.5f;
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime = 0f;
    public Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        originalPosition = transform.position;

        // Start in idle state
        animator.SetBool("isWalking", false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found! Assign player manually.");
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= attackRange + 1.5f) // Allow attacking slightly further
        {
            agent.ResetPath();
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            animator.SetBool("isWalking", false);
            AttemptAttack();
        }

        else if (distanceToPlayer <= detectionRadius) // Chase if in detection range
        {
            agent.isStopped = false; // Allow movement
            agent.SetDestination(playerTransform.position);
            animator.SetBool("isWalking", true); // Enable walking animation
        }
        else // Return to original position if player is too far
        {
            agent.isStopped = false;
            agent.SetDestination(originalPosition);

            if (Vector3.Distance(transform.position, originalPosition) > 0.5f) // If boss is still moving back, walk
            {
                animator.SetBool("isWalking", true);
            }
            else // Otherwise, idle
            {
                animator.SetBool("isWalking", false);
            }
        }
        // Face the player while chasing
        if (distanceToPlayer <= detectionRadius)
        {
            FacePlayer();
        }
    }
    void FacePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void AttemptAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("Boss is attacking!");
            lastAttackTime = Time.time;

            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            animator.SetBool("isWalking", false); // Force stop walking animation

            if (health >= 400)
            {
                Debug.Log("Triggering attack_01");
                animator.SetTrigger("attack_01");
            }
            else if (health <= 399 && health >= 200)
            {
                Debug.Log("Triggering attack_02");
                animator.SetTrigger("attack_02");
            }
            else if (health > 0 && health < 200)
            {
                Debug.Log("Triggering attack_03");
                animator.SetTrigger("attack_03");
            }

            StartCoroutine(ResumeChaseAfterDelay());
        }
    }
    IEnumerator ResumeChaseAfterDelay()
    {
        yield return new WaitForSeconds(attackCooldown); // Wait for attack animation
        agent.isStopped = false; // Resume movement
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
    
    
    IEnumerator delayTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Boss dead! Starting destroy object timer!");
        Destroy(gameObject);
    }



}
