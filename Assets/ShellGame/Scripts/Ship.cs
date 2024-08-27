using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float rotationSpeed = 2f;

    void Update()
    {

    }

    //Move the ship across the screen and have it slowly rotate 
    public IEnumerator Move(){
        Vector3 target = new Vector3(-35 , transform.position.y, transform.position.z);
        Vector3 start = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < 5){
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(start, target, elapsedTime / 5);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }
}
