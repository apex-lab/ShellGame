using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Variables for crates
    public GameObject[] crates;
    public Material correctMat;
    public Material incorrectMat;
    private bool playing;

    //Variables for distractors
    public GameObject asteroid;
    public GameObject ship;
    public GameObject missile;
    public GameObject alarm1;
    public GameObject alarm2;
    public GameObject ceilingLight;

    //Variables for sound effects
    public AudioSource alarm;
    public AudioSource explosion;
    public AudioSource engine;
    public AudioSource missileShot;
    public AudioSource electricSurge;
    public AudioSource metalCrash;

    public EyeTracking eyeTracking;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayGame());
    }

    // Update is called once per frame
    void Update()
    {
        //After the movement is finished detect mouse clicks and reveal if correct crate was selected
        if (Input.GetMouseButtonDown(0) && !playing){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                if (hit.collider.CompareTag("Correct")){
                    crates[0].GetComponent<Renderer>().material = correctMat;
                } else if (hit.collider.CompareTag("Incorrect")){
                    hit.collider.GetComponent<Renderer>().material = incorrectMat;
                }
            }
        }
    }

    IEnumerator PlayGame(){
        playing = true;

        //Move the lid up and down to reveal the correct crate
        Transform crate = crates[0].transform.GetChild(0);
        Transform lid = crate.transform.GetChild(0);
        LidMovement lidScript = lid.GetComponent<LidMovement>();
        lidScript.MoveLid();
        yield return new WaitForSeconds(5);

        //Get the scripts to move each of the crates
        Crate crate1 = crates[0].GetComponent<Crate>();
        Crate crate2 = crates[1].GetComponent<Crate>();
        Crate crate3 = crates[2].GetComponent<Crate>();
        float duration = crate1.duration + 0.01f;

        //Get the scripts for distractors
        Asteroid asteroidScript = asteroid.GetComponent<Asteroid>();
        Ship shipScript = ship.GetComponent<Ship>();
        Missile missileScript = missile.GetComponent<Missile>();
        Alarm1 alarm1Script = alarm1.GetComponent<Alarm1>();
        Alarm2 alarm2Script = alarm2.GetComponent<Alarm2>();
        CeilingLight ceilingLightScript = ceilingLight.GetComponent<CeilingLight>();

        eyeTracking.StartTracking();

        //Set sequence of crate movements, distractors, and sound effects
        MoveUp(crate1);
        yield return new WaitForSeconds(duration);
        MoveRight(crate2);
        MoveLeft(crate1);
        yield return new WaitForSeconds(duration);
        MoveDown(crate1);
        MoveUp(crate3);
        yield return new WaitForSeconds(duration);
        MoveLeft(crate3);
        MoveRight(crate2);
        yield return new WaitForSeconds(duration);
        MoveDown(crate3);
        MoveUp(crate1);
        yield return new WaitForSeconds(duration);
        StartCoroutine(asteroidScript.Move());
        eyeTracking.pushSample("Distractor: Asteroid");
        MoveRight(crate1);
        MoveLeft(crate3);
        yield return new WaitForSeconds(duration);
        MoveDown(crate1);
        MoveUp(crate2);
        yield return new WaitForSeconds(duration);
        MoveLeft(crate2);
        MoveRight(crate1);
        yield return new WaitForSeconds(duration);
        MoveDown(crate2);
        MoveUp(crate3);
        yield return new WaitForSeconds(duration);
        MoveRight(crate3);
        MoveDown(crate1);
        yield return new WaitForSeconds(duration);
        missileShot.Play();
        eyeTracking.pushSample("Sound Effect: Missile Shot");
        StartCoroutine(missileScript.Move());
        eyeTracking.pushSample("Distractor: Missile");
        MoveLeft(crate1);
        MoveRight(crate3);
        yield return new WaitForSeconds(duration);
        MoveLeft(crate1);
        MoveDown(crate3);
        yield return new WaitForSeconds(duration);
        MoveUp(crate1);
        MoveUp(crate2);
        yield return new WaitForSeconds(duration);
        explosion.Play();
        eyeTracking.pushSample("Sound Effect: Explosion");
        electricSurge.Play();
        eyeTracking.pushSample("Sound Effect: Electric Surge");
        StartCoroutine(ceilingLightScript.Move());
        eyeTracking.pushSample("Distractor: Ceiling Light");
        MoveLeft(crate3);
        MoveRight(crate2);
        yield return new WaitForSeconds(0.3f);
        metalCrash.Play();
        eyeTracking.pushSample("Sound Effect: Metal");
        yield return new WaitForSeconds(duration - 0.3f);
        MoveDown(crate2);
        MoveUp(crate1);
        yield return new WaitForSeconds(duration);
        engine.Play();
        eyeTracking.pushSample("Sound Effect: Engine");
        StartCoroutine(shipScript.Move());
        eyeTracking.pushSample("Distractor: Ship");
        MoveRight(crate1);
        MoveLeft(crate3);
        yield return new WaitForSeconds(duration);
        MoveDown(crate1);
        MoveUp(crate2);
        yield return new WaitForSeconds(duration);
        MoveLeft(crate2);
        MoveRight(crate1);
        yield return new WaitForSeconds(duration);
        MoveDown(crate2);
        MoveUp(crate3);
        yield return new WaitForSeconds(duration);
        MoveRight(crate3);
        MoveLeft(crate2);
        yield return new WaitForSeconds(duration);
        StartCoroutine(alarm1Script.Move());
        StartCoroutine(alarm2Script.Move());
        eyeTracking.pushSample("Distractor: Alarms");
        alarm.Play();
        eyeTracking.pushSample("Sound Effect: Alarms");
        MoveDown(crate3);
        MoveUp(crate1);
        yield return new WaitForSeconds(duration);
        MoveLeft(crate1);
        MoveDown(crate2);
        yield return new WaitForSeconds(duration);
        MoveLeft(crate1);
        MoveRight(crate2);
        yield return new WaitForSeconds(duration);
        MoveRight(crate2);
        MoveDown(crate1);
        yield return new WaitForSeconds(duration);
        MoveUp(crate2);
        yield return new WaitForSeconds(1);
        alarm.Stop();
        eyeTracking.pushSample("End of Movement");
        
        playing = false;
    }

    //Movement functions
    public void MoveUp(Crate crate){
        StartCoroutine(crate.MoveUp());
    }
    public void MoveDown(Crate crate){
        StartCoroutine(crate.MoveDown());
    }
    public void MoveLeft(Crate crate){
        StartCoroutine(crate.MoveLeft());
    }
    public void MoveRight(Crate crate){
        StartCoroutine(crate.MoveRight());
    }
}
