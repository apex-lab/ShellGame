using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Crate : MonoBehaviour
{

    //Time for each movement
    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set of commands for moving the crates around either up, down, left, or right

    public IEnumerator MoveUp(){
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f);
        Vector3 start = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration){
            transform.position = Vector3.Lerp(start, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }

    public IEnumerator MoveDown(){
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.5f);
        Vector3 start = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration){
            transform.position = Vector3.Lerp(start, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }

    public IEnumerator MoveLeft(){
        Vector3 target = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
        Vector3 start = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration){
            transform.position = Vector3.Lerp(start, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }

    public IEnumerator MoveRight(){
        Vector3 target = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        Vector3 start = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration){
            transform.position = Vector3.Lerp(start, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }
}
