using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{   
    public NavMeshAgent agent;

    public Animator Anim;

    public GameObject captureCount;

    public float wanderRadius;
    public float waitTime;
    public float defaultSpeed;
    public float runSpeed;

    private Camera myCam;

    private Image _counterImage;
    
    private Vector3 _currentPos;
    private Vector3 _newPos;
    
    private float _speed;
    private float dotProduct;
    private float distance;
    
    private bool _isWaiting;

    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        //initialize our player
        _player = GameObject.FindWithTag("Player");
        
        myCam = Camera.main;

        _counterImage = captureCount.transform.GetChild(1).GetComponent<Image>();

        agent.speed = defaultSpeed;
        _currentPos = transform.position;
        _newPos = _currentPos;
    }

    // Update is called once per frame
    void Update()
    {           
        //cat looking for player
        PlayerLook();
        
        //if player is too close to cat and within their fov
        if (dotProduct < 0.5f && distance < 1f)
        {
            //cat runs in the opposite direction
            StartCoroutine(AgentRun());
        }
        
        GetCaptured();
        
        //if the cat reaches its destination
        if (_currentPos == _newPos)
        {
            //give it a new destination
            StartCoroutine(AgentWalk());
        }

        //if the cat has not reached its new destination and it's not moving and it's not actively waiting
        //NOTE: this keeps our cat from getting stuck when it's given an unreachable destination
        if (_currentPos != _newPos && _speed == 0 && _isWaiting == false)
        {
            //give it a new destination
            StartCoroutine(AgentWalk());
        }
        
        //play the appropriate animation
        AnimPlay();   
    }

    private Vector3 RandomNavPoint(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;
        
        NavMeshHit navMeshHit = new NavMeshHit();
        NavMesh.SamplePosition(randomDirection, out navMeshHit, dist, layerMask);
        return navMeshHit.position;
    }

    private Vector3 RunAwayPoint(Vector3 origin, float dist, int layerMask)
    {
        Vector3 runDirection = _player.transform.forward * dist;
        runDirection += origin;
        
        NavMeshHit navMeshHit = new NavMeshHit();
        NavMesh.SamplePosition(runDirection, out navMeshHit, dist, layerMask);
        return navMeshHit.position;
    }

    private IEnumerator AgentWalk()
    {
        _isWaiting = true;
        _newPos = RandomNavPoint(transform.position, wanderRadius, -1);
        agent.SetDestination(_newPos);
        
        yield return new WaitForSeconds(waitTime);

        _isWaiting = false;
        _currentPos = transform.position;
    }

    private IEnumerator AgentRun()
    {
        agent.speed = runSpeed;
        _newPos = RunAwayPoint(transform.position, wanderRadius, -1);
        agent.SetDestination(_newPos);
        
        yield return new WaitForSeconds(3f);
        agent.speed = defaultSpeed;
    }

    void PlayerLook()
    {
        Vector3 direction = _player.transform.position - transform.position;

        direction.Normalize();
        
        Quaternion newQuaternion = Quaternion.LookRotation(direction);

        dotProduct = Vector3.Dot(direction, -transform.forward);
        distance = Vector3.Distance(_player.transform.position, transform.position); 
        
        //Debug.DrawRay(transform.position, direction, Color.red);
    }

    void AnimPlay()
    {
        //initialize variable as agent velocity along the forward axis
        _speed = agent.velocity.z;
        
        //if the cat is moving and the speed is our default speed
        if (_speed != 0 && agent.speed <= defaultSpeed)
        {
            //play walking animation
            Anim.SetBool("isWalking", true);
        }
        
        //if the cat is moving and the speed is our running speed
        if (_speed != 0 && agent.speed >= runSpeed)
        {
            //play running animation
            Anim.SetBool("isRunning", true);
        }
        
        //if the cat is not moving
        if(_speed == 0)
        {
            //don't play either animation
            //A.K.A. switch back to Idle animation
            Anim.SetBool("isWalking", false);
            Anim.SetBool("isRunning", false);
        }
    }

    void GetCaptured()
    {
        //if you are out of the cat's fov AND are close to the cat
        if (dotProduct > 0.5f && distance < 1f)
        {
            captureCount.SetActive(true); //activate the capture counter
            _counterImage.fillAmount += 0.25f * Time.deltaTime; //increase the radial fill of the capture counter
        }

        //if you are too far from the cat OR you are in the cat's fov OR the cat is running away 
        if (distance > 1f || dotProduct < 0.5f || agent.speed >= runSpeed)
        {
            _counterImage.fillAmount = 0f; //reset the capture counter
            captureCount.SetActive(false); //deactivate the capture counter
        }
        
        //if the radial fill of the capture counter is 1 (all the way filled)
        if (_counterImage.fillAmount >= 1f)
        {
            Destroy(gameObject); //destroy the cat
            Destroy(captureCount); //destroy the capture counter attached to the cat
            GameManager.instance.Cats++; //increase the cat score keeper
        }
    }
}
