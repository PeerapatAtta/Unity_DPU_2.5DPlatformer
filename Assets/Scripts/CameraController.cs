using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance; // reference to the camera controller
    
    public CinemachineBrain theCMBrain; // reference to the cinemachine brain

    private void Awake()
    {
        instance = this; // set the instance to this camera controller
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
