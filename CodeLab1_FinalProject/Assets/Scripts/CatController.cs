using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{

    public NavMeshAgent agent;

    public Animator Anim;

    public float wanderRadius;
    public float waitTime;
    
    private Vector3 _currentPos;
    private Vector3 _newPos;
    private float _speed;

    private bool _isWaiting;

    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        
        _currentPos = transform.position;
        _newPos = _currentPos;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLook();
        
        if (_currentPos == _newPos)
        {
            StartCoroutine(MoveAgent());
        }

        if (_currentPos != _newPos && _speed == 0 && _isWaiting == false)
        {
            StartCoroutine(MoveAgent());
        }
        
        _speed = agent.velocity.z;
        
        if (_speed != 0)
        {
            Anim.SetBool("isMoving", true);
        }
        if(_speed == 0)
        {
           Anim.SetBool("isMoving", false);
        }
        
    }

    private Vector3 RandomNavPoint(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;
        
        NavMeshHit navMeshHit = new NavMeshHit();
        NavMesh.SamplePosition(randomDirection, out navMeshHit, dist, layerMask);
        return navMeshHit.position;
    }

    private IEnumerator MoveAgent()
    {
        _isWaiting = true;
        _newPos = RandomNavPoint(transform.position, wanderRadius, -1);
        agent.SetDestination(_newPos);
        
        yield return new WaitForSeconds(waitTime);

        _isWaiting = false;
        _currentPos = transform.position;
    }

    void PlayerLook()
    {
        Vector3 direction = _player.transform.position - transform.position;

        direction.Normalize();
        
        Quaternion newQuaternion = Quaternion.LookRotation(direction);

        float dotProduct = Vector3.Dot(direction, transform.forward);
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        
        
        Debug.DrawRay(transform.position, direction, Color.red);


        if (dotProduct < 0.3f && distance < 1f)
        {
            Debug.Log("You're too close to the cat!");
        }
    }
}
