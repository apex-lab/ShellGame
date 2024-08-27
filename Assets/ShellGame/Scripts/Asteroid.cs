using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public float rotationSpeed = 100f;

    //Asteroid constantly spins based on rotation speed
    void Update()
    {
        // Rotate the object around the Y-axis (upwards)
        transform.Rotate(Vector3.left, rotationSpeed / 2 * Time.deltaTime);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    //Asteroid flies across the screen diagnolly when called
    public IEnumerator Move(){
        Vector3 target = new Vector3(-12, 1, transform.position.z);
        Vector3 start = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < 2){
            transform.position = Vector3.Lerp(start, target, elapsedTime / 2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }
}
