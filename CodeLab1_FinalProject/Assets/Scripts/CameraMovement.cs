using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera myCam;
    public float zoomMultiplier = 10f;
    public float minZoom = -7f;
    public float maxZoom = -0.75f;
   
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            myCam.orthographicSize += zoom;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            myCam.orthographicSize += zoom;
        }

        myCam.orthographicSize = Mathf.Clamp(myCam.orthographicSize, minZoom, maxZoom);
        
        print(Input.GetAxis("Mouse ScrollWheel"));

        /*if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit point = new RaycastHit();
            Physics.Raycast(ray, out point, 25f);
            
            
            transform.position 
        }*/
    }
}
