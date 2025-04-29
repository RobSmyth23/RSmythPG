using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class charMovementScript : MonoBehaviour, iHealth
{
    public float jumpForce = 4.0f;
    private Rigidbody rb;
    float speed = 3.0f;
    float turningSpeed = 90.0f;
    float moveSpeed = 4.0f;
    public float runMultiplier = 2f;
    float zoomSpeed = 0.2f;
    int health = 550;
    public float minZoom = 5.0f;
    public float maxZoom = 20.0f;
    private float currentZoom = -5.0f;
    public Transform character; 
    public Vector3 offset; 
    private bool isLookingBehind = false;
    public Vector3 lookBehindOffset;
    bool isOptionsMenuOpen = false;
    public GameObject optionsMenu;
    public GameObject projectileCloneTemplate;
    private float damageCooldown = 1f; 
    private float lastDamageTime = 0f;
    public Image HealthBar;
    int maxHealth = 550;

    void Start()
    {
        transform.position = new Vector3(1, 1, 1);
        rb = GetComponent<Rigidbody>();
        Camera.main.transform.localPosition = new Vector3(0, 1.57f, currentZoom);
        HealthBar.fillAmount = 1;
    }

    
    void Update()
    {
        if (shouldZoomIn()) zoomIn();
        if (shouldZoomOut()) zoomOut();
        if (shouldMoveForward()) moveForward();
        if (shouldMoveBack()) moveBack();
        if (shouldMoveLeft()) moveLeft();
        if (shouldMoveRight()) moveRight();
        if (shouldTurnLeft()) turnLeft();
        if (shouldTurnRight()) turnRight();
        if (shouldJump()) jump();
        if (Input.GetKeyDown(KeyCode.C))
        {
            isLookingBehind = !isLookingBehind;
            if (isLookingBehind)
            {
                currentZoom = Mathf.Abs(currentZoom);
                // Look behind the character
                Camera.main.transform.localPosition = new Vector3(0, 1.57f, currentZoom);
                Camera.main.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                // Follow the character normally
                currentZoom = -Mathf.Abs(currentZoom);
                Camera.main.transform.localPosition = new Vector3(0, 1.57f, currentZoom);
                Camera.main.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                LoadOptionsMenu();
                isOptionsMenuOpen = true;
            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject newGo = Instantiate(projectileCloneTemplate, transform.position + Vector3.up + 1f * transform.forward, transform.rotation);
        }

        

    }
    private void jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private bool shouldJump()
    {
        return (Mathf.Abs(rb.velocity.y) < 0.001f) && Input.GetKeyDown(KeyCode.Space);
    }

    private void turnLeft()
    {
        transform.Rotate(new Vector3(0, -1, 0), turningSpeed * Time.deltaTime);
    }

    private void turnRight()
    {
        transform.Rotate(new Vector3(0, 1, 0), turningSpeed * Time.deltaTime);
    }
    private void moveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
    private void moveRight()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private bool shouldTurnLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    private bool shouldTurnRight()
    {
        return Input.GetKey(KeyCode.D);
    }
    private bool shouldMoveLeft()
    {
        return Input.GetKey(KeyCode.Q);
    }
    private bool shouldMoveRight()
    {
        return Input.GetKey(KeyCode.E);
    }

    private void moveForward()
    {
        float currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= runMultiplier;
        }

        transform.position += currentSpeed * transform.forward * Time.deltaTime;
    }

    private void moveBack()
    {
        transform.position -= speed * transform.forward * Time.deltaTime;
    }

    private bool shouldMoveForward()
    {
        return Input.GetKey(KeyCode.W);
    }

    private bool shouldMoveBack()
    {

        return Input.GetKey(KeyCode.S);
    }
    private bool shouldZoomIn()
    {
        return Input.GetAxis("Mouse ScrollWheel") > 0f;
    }

    private bool shouldZoomOut()
    {
        return Input.GetAxis("Mouse ScrollWheel") < 0f;
    }

    private void zoomIn()
    {
        float size = Mathf.Abs(currentZoom);
        size -= zoomSpeed;
        size = Mathf.Clamp(size, minZoom, maxZoom);
        currentZoom = Mathf.Sign(currentZoom) * size; 
      
        Camera.main.transform.localPosition = new Vector3(0, 1.57f, currentZoom);

    }

    private void zoomOut()
    {
        float size = Mathf.Abs(currentZoom);
        size += zoomSpeed;
        size = Mathf.Clamp(size, minZoom, maxZoom);
        currentZoom = Mathf.Sign(currentZoom) * size;
        Camera.main.transform.localPosition = new Vector3(0, 1.57f, currentZoom);
    }

    void LoadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        HealthBar.fillAmount = (float)health / maxHealth;
        if (health <= 20)
        {
            //warning message
            Debug.Log("Health Low!!!! Health Low!!!!");
        }
        if(health == 0)
        {
            SceneManager.LoadScene("DeathScreen", LoadSceneMode.Additive);
           //LoadDeathScene();
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                TakeDamage(10); // Inflict damage
                Debug.Log("Taken 10 damage");
                lastDamageTime = Time.time; // Update the last damage time
            }
        }
    }
}