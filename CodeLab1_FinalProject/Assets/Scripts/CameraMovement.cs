using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera myCam;

    //public GameObject level;
    
    public float zoomMultiplier = 10f;
    public float minZoom = -7f;
    public float maxZoom = -0.75f;

    private float _horizontalExtent = 12.4f;
    private float _verticalExtent = 7f;

    private Vector3 _camOrigin;
   
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        _camOrigin = myCam.transform.position;
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

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //center camera to mouse pos
            transform.position = Vector3.Slerp(transform.position,
                new Vector3(zoomPos.x, transform.position.y, zoomPos.z), zoomMultiplier * Time.deltaTime);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.position = Vector3.Slerp(transform.position, _camOrigin, zoomMultiplier * Time.deltaTime);
        }
        
        /*Vector3 leftLowerBound = myCam.ViewportToWorldPoint(new Vector3(0, 0, myCam.farClipPlane));
        Vector3 rightUpperBound = myCam.ViewportToWorldPoint(new Vector3(1, 1, myCam.farClipPlane));

        print("leftlower: " + leftLowerBound);
        print("rightupper: " + rightUpperBound);

        if (leftLowerBound.x < -_horizontalExtent)
        {
            leftLowerBound.x = -_horizontalExtent;
        }

        if (rightUpperBound.x > _horizontalExtent)
        {
            rightUpperBound.x = _horizontalExtent;
        }

        if (leftLowerBound.y < -_verticalExtent)
        {
            leftLowerBound.y = -_verticalExtent;
        }

        if (rightUpperBound.y > _verticalExtent)
        {
            rightUpperBound.y = _verticalExtent;*/
        //}
    }
}
