using System.Collections;
using UnityEngine;

public class CeilingLight : MonoBehaviour
{

    //Variables for initial rotation before it falls
    public Transform pivotPoint;  
    public float rotationAngle = -20f;  
    public float duration = 0.2f; 

    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    //Light twists and falls then bounces on the floor when called
    public IEnumerator Move()
    {

        float elapsedTime = 0f;
        float totalRotation = 0f;

        // Rotate over time until the desired angle is reached
        while (elapsedTime < duration)
        {
            // Calculate rotation for this frame
            float rotationThisFrame = (rotationAngle / duration) * Time.deltaTime;
            totalRotation += rotationThisFrame;

            // Rotate the object around the X-axis (world space)
            transform.RotateAround(pivotPoint.position, transform.forward, rotationThisFrame);

            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        // Ensure the object reaches exactly the desired angle at the end
        float remainingRotation = rotationAngle - totalRotation;
        if (remainingRotation != 0f)
        {
            transform.RotateAround(pivotPoint.position, transform.forward, remainingRotation);
        }

        //Enable gravity so object falls
        rb.useGravity = true;
        yield return new WaitForSeconds(0.2f);


        //Finish rotation
        rotationAngle = -180f;
        duration = 0.4f;

        elapsedTime = 0f;
        totalRotation = 0f;

        // Rotate over time until the desired angle is reached
        while (elapsedTime < duration)
        {
            // Calculate rotation for this frame
            float rotationThisFrame = (rotationAngle / duration) * Time.deltaTime;
            totalRotation += rotationThisFrame;

            // Rotate the object around the X-axis (world space)
            transform.RotateAround(transform.position, transform.forward, rotationThisFrame);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches exactly the desired angle at the end
        remainingRotation = rotationAngle - totalRotation;
        if (remainingRotation != 0f)
        {
            transform.RotateAround(transform.position, transform.forward, remainingRotation);
        }
    }

    //Object bounces when it hits the floot
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object hits the ground or other surface
        if (collision.gameObject.CompareTag("Room"))
        {
            // Add a set torque to make the object rattle a bit after hitting the ground
            Vector3 randomTorque = new Vector3(-0.1f, 0.5f , 0.2f);
            rb.AddTorque(randomTorque, ForceMode.Impulse);

            Vector3 bounceForce = Vector3.up * 0.1f;  
            rb.AddForce(bounceForce, ForceMode.Impulse);
        }
    }
}
