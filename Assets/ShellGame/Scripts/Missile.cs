using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    //Variables for missile movement
    private Vector3 startPoint; 
    private Vector3 endPoint; 
    public float speed = 8f;     
    public float waveHeight = 2f;  
    public float waveFrequency = 4f;  
    public GameObject explosionEffect;  
    private Vector3 previousPosition;  

    void Start()
    {
        // Set initial position to the start point
        startPoint = transform.position;
        endPoint = new Vector3(10, transform.position.y, transform.position.z);

        // Store the initial position to calculate direction later
        previousPosition = transform.position;
    }

    public IEnumerator Move()
    {
        float journeyLength = Vector3.Distance(startPoint, endPoint);
        float startTime = Time.time;

        while (true)
        {
            // Calculate the fraction of the journey completed based on time
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Move along the linear path between startPoint and endPoint
            Vector3 linearPosition = Vector3.Lerp(startPoint, endPoint, fractionOfJourney);

            // Apply a sine wave to the y-axis (or another axis for wave direction)
            float waveOffset = Mathf.Sin(fractionOfJourney * Mathf.PI * 2 * waveFrequency) * waveHeight;

            // Adjust the object's position with the wave effect
            Vector3 newPosition = new Vector3(linearPosition.x, linearPosition.y + waveOffset, linearPosition.z);

            // Rotate the object to face the direction of movement
            Vector3 direction = newPosition - previousPosition;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // Update the object's position
            transform.position = newPosition;

            // Update previous position for the next frame
            previousPosition = transform.position;

            // Stop when the object reaches the endpoint
            if (fractionOfJourney >= 1f)
            {
                transform.position = endPoint;

                // Trigger the explosion effect at the endpoint
                Explode();

                break;
            }

            yield return null;  
        }
    }

    void Explode()
    {
        // Instantiate the explosion particle effect at the object's current position
        if (explosionEffect != null)
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        
        // Check if the particle system is active and start it
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }

        //Destroy the particle system after it finishes
        Destroy(explosion, ps.main.duration);
    }

        //Destroy the object after the explosion
        Destroy(gameObject);
    }
}
