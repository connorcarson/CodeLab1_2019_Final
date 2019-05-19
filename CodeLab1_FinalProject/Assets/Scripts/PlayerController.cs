using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    
    private Camera myCam;
    
    private Vector3 _destination;
    private Vector3 _oldDest;
        
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        _oldDest = transform.position;
        _destination = _oldDest;
    }

    // Update is called once per frame
    void Update()
    {
        if (_destination != _oldDest)
        {
            agent.SetDestination(_destination);
            _oldDest = _destination;
        }
        
        MoveTruck();
    }

    void MoveTruck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray myRay = myCam.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit myRaycastHit;

            if (Physics.Raycast(myRay, out myRaycastHit))
            {
                _destination = myRaycastHit.point;
            }
        }
    }
    
}
