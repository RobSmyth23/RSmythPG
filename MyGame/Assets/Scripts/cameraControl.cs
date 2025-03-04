using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    //public Transform character; // Assign your character's transform in the Inspector
    charMovementScript myCHar; // Assign your character's movement script in the Inspector

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        myCHar = FindObjectOfType<charMovementScript>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            myCHar.enabled = false; // Disable the character movement script
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            MoveCameraToMousePosition();
        }

        if (Input.GetMouseButtonUp(0)) // 0 is the left mouse button
        {
            myCHar.enabled = true; // Enable the character movement script
            StopAllCoroutines();
            StartCoroutine(MoveCamera(originalPosition, originalRotation));
        }
    }

    void MoveCameraToMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            StartCoroutine(MoveCamera(targetPosition, transform.rotation));
        }
    }

    System.Collections.IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }
    }
}
