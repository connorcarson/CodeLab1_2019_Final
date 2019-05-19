using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour
{
    public Camera myCam;
    public GameObject cat;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = myCam.WorldToScreenPoint(cat.transform.position);

        transform.position = currentPos;
    }
}
