using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMovementScript : MonoBehaviour
{
    public float jumpForce = 4.0f;
    private Rigidbody rb;
    float speed = 3.0f;
    float turningSpeed = 90.0f;
    float moveSpeed = 9.0f;
    float zoomSpeed = 0.2f;
    public float minZoom = 5.0f;
    public float maxZoom = 20.0f;
    private float currentZoom = -5.0f;
    public Transform character; 
    public Vector3 offset; 
    private bool isLookingBehind = false;
    
    void Start()
    {
        transform.position = new Vector3(1, 1, 1);
        rb = GetComponent<Rigidbody>();
        Camera.main.transform.localPosition = new Vector3(0, 1.57f, currentZoom);
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
        transform.position += speed * transform.forward * Time.deltaTime;
    }

    private void moveBack()
    {
        transform.position -= transform.forward * Time.deltaTime;
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
}