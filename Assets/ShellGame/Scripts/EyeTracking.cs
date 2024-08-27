using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varjo.XR;
using LSL;
using System.Linq;
using System;

public class EyeTracking : MonoBehaviour
{

    //Variables needed to collect eye tracking data
    private VarjoEyeTracking.GazeData gazeData;
    public Camera xrCamera;
    private Vector3 rayOrigin;
    private Vector3 direction;
    public float gazeRadius = 0.01f;
    private RaycastHit hit;
    private List<VarjoEyeTracking.GazeData> dataSinceLastUpdate;
    private List<VarjoEyeTracking.EyeMeasurements> eyeMeasurementsSinceLastUpdate;

    //Variables needed to send LSL data
    string StreamName = "Unity.Destroyed";
    string StreamType = "Markers";
    private StreamOutlet outlet;
    private string[] sample = {""};

    private bool tracking = false;

    // Start is called before the first frame update
    void Start()
    {
        //Create LSL outlet
        var hash = new Hash128();
        hash.Append(StreamName);
        hash.Append(StreamType);
        hash.Append(gameObject.GetInstanceID());
        StreamInfo streamInfo = new StreamInfo(StreamName, StreamType, 1, LSL.LSL.IRREGULAR_RATE,
        channel_format_t.cf_string, hash.ToString());
        outlet = new StreamOutlet(streamInfo);
    }

    // Update is called once per frame
    void Update()
    {
        //When tracking is enabled data is recieved and sent using Varjo and LSL
        if(tracking){
            if (VarjoEyeTracking.IsGazeCalibrated()){
                gazeData = VarjoEyeTracking.GetGaze();
                if (gazeData.status != VarjoEyeTracking.GazeStatus.Invalid){
                    rayOrigin = xrCamera.transform.TransformPoint(gazeData.gaze.origin);
                    direction = xrCamera.transform.TransformDirection(gazeData.gaze.forward);
                }
            }
            
            //Checks what is being looked at and sends data to LSL accordingly
            if (Physics.SphereCast(rayOrigin, gazeRadius, direction, out hit)){
                VarjoEyeTracking.GetGazeList(out dataSinceLastUpdate, out eyeMeasurementsSinceLastUpdate);
                sample[0] = rayOrigin + "/" + direction + "/" + hit.collider.tag + "/";
                outlet.push_sample(sample);
            } else {
                sample[0] = rayOrigin + "/" + direction + "/" + "NULL" + "/";
                outlet.push_sample(sample);
            }
        }
    }

    public void StartTracking(){
        tracking = true;
    }

    //Used to push other samples to mark certain events
    public void pushSample(string sampleText){
        sample[0] = sampleText;
        outlet.push_sample(sample);
    }
}
