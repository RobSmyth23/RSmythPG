using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMovementScript : MonoBehaviour
{
    float speed = 3f;
    float turningSpeed = 90f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(1, 2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMoveForward()) moveForward();
        if (shouldMoveBack()) moveBack();
        if (shouldTurnLeft()) turnLeft();
        if (shouldTurnRight()) turnRight();


    }

    private void turnLeft()
    {
        transform.Rotate(new Vector3(0, -1, 0), turningSpeed * Time.deltaTime);
    }

    private void turnRight()
    {
        transform.Rotate(new Vector3(0, 1, 0), turningSpeed * Time.deltaTime);
    }

    private bool shouldTurnLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    private bool shouldTurnRight()
    {
        return Input.GetKey(KeyCode.D);
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
        //if (Input.GetKey(KeyCode.W)) return true;
        //else return false;

        return Input.GetKey(KeyCode.W);
    }

    private bool shouldMoveBack()
    {

        return Input.GetKey(KeyCode.S);
    }
}