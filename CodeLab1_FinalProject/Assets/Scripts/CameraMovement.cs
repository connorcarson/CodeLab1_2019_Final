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
        
        Vector3 zoomPos = myCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myCam.farClipPlane));
        
        print(zoomPos);

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //center camera to mouse pos
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(zoomPos.x, transform.position.y, zoomPos.z), zoomMultiplier * Time.deltaTime);
        }
    }
}
