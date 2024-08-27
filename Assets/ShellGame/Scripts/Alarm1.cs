using UnityEngine;
using System.Collections;

public class Alarm1 : MonoBehaviour
{
    //Variables for circular movement
    public Transform centerPoint;
    public float radius = 0.5f;
    public float angularSpeed = 2f;
    public float duration = 2f;

    void Start()
    {

    }

    // Coroutine to move the object in a circular path for a set duration
    public IEnumerator Move()
    {
        //Set starting variables and obect position
        transform.position = new Vector3(centerPoint.position.x + radius, centerPoint.position.y, centerPoint.position.z);
        float startTime = Time.time;  
        float angle = 0f;

        while (Time.time - startTime < duration)
        {
            // Update the angle based on angular speed and time
            angle += angularSpeed * Time.deltaTime;

            // Calculate the new position using trigonometry
            float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
            float y = centerPoint.position.y;
            float z = centerPoint.position.z + Mathf.Sin(angle) * radius;

            // Apply the new position to the object
            transform.position = new Vector3(x, y, z);

            // Wait until the next frame
            yield return null;
        }
        // Move object to a new final position after the circular movement
        transform.position = new Vector3(centerPoint.position.x, centerPoint.position.y - 3, centerPoint.position.z);
    }
}
