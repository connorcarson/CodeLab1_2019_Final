using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Image playerIndicator;
    
    private Camera myCam;
    
    private Vector3 _destination;
    private Vector3 _oldDest;

    private RectTransform _playerIndicatorRect;
        
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        _oldDest = transform.position;
        _destination = _oldDest;
        
        _playerIndicatorRect = playerIndicator.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_destination != _oldDest)
        {
            agent.SetDestination(_destination);
            _oldDest = _destination;
        }
        
        MovePlayer();

        Vector3 currentPos = myCam.WorldToScreenPoint(transform.position);
        Vector3 currentPOs2 = myCam.WorldToScreenPoint(transform.forward);

        playerIndicator.transform.position = currentPos;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            _playerIndicatorRect.sizeDelta = new Vector2(_playerIndicatorRect.sizeDelta.x - 200f * Time.deltaTime, _playerIndicatorRect.sizeDelta.y - 200f * Time.deltaTime);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            _playerIndicatorRect.sizeDelta = new Vector2(_playerIndicatorRect.sizeDelta.x + 200f * Time.deltaTime, _playerIndicatorRect.sizeDelta.y + 200f * Time.deltaTime);
        }

        _playerIndicatorRect.sizeDelta = new Vector2(Mathf.Clamp(_playerIndicatorRect.sizeDelta.x, 100, 300), Mathf.Clamp(_playerIndicatorRect.sizeDelta.y, 100, 300));
    }

    void MovePlayer()
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
