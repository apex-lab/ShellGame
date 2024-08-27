using UnityEngine;
using System.Collections;

public class LidMovement : MonoBehaviour
{
    //Variables for lid movement
    public Transform lid;  
    private Vector3 openPosition;  
    public float speed = 2f;  
    private Vector3 closedPosition; 
    private bool isMoving = false;

    void Start()
    {
        // Store the initial position of the lid (closed position)
        closedPosition = lid.position;
        openPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    public void MoveLid()
    {
        // Only start the coroutine if the lid is not already moving
        if (!isMoving)
        {
            StartCoroutine(MoveLidUpAndDown());
        }
    }

    private IEnumerator MoveLidUpAndDown()
    {
        isMoving = true;

        // Move the lid up to the open position
        yield return StartCoroutine(MoveToPosition(lid, openPosition));

        // Wait for a short time at the open position
        yield return new WaitForSeconds(1f);

        // Move the lid back down to the closed position
        yield return StartCoroutine(MoveToPosition(lid, closedPosition));

        isMoving = false; 
    }

    private IEnumerator MoveToPosition(Transform obj, Vector3 targetPosition)
    {
        // While the object hasn't reached the target position
        while (Vector3.Distance(obj.position, targetPosition) > 0.01f)
        {
            // Move the object closer to the target position
            obj.position = Vector3.Lerp(obj.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }

        // Set the exact target position at the end of the movement to avoid overshooting
        obj.position = targetPosition;
    }
}
