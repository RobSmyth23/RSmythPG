using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalStoneTrigger : MonoBehaviour
{
    private bool isActivated = false; // Prevent multiple triggers

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("Player")) // Check if it's the player
        {
            isActivated = true; // Mark the trigger as activated
            StartCoroutine(HandleSceneTransition());
        }
    }

    private IEnumerator HandleSceneTransition()
    {
        SceneManager.LoadScene("EndGame"); // Load EndGame scene
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        SceneManager.LoadScene("MainMenu"); // Load MainMenu scene
    }
}
