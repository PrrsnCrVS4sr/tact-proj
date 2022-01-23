using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIController : MonoBehaviour
{
    
    [SerializeField]Transform playerTarget;

    [SerializeField] float attackRadius = 10f;

    float rawAngle;
    float halfFOV = 45.0f;
    float angleRotated;
    bool insideFOV;

    bool hasBeenDetectedOnce;

    Vector3 dir;
    Vector3 playersLastSeenPostion;
    [SerializeField] float detectionRadius = 20f;
    Patrol patrolScript;
    
    Rigidbody2D enemyRB;
    AIDestinationSetter destinationSetter;
    private void Awake() {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        patrolScript = GetComponent<Patrol>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        enemyRB = GetComponent<Rigidbody2D>();
        patrolScript.enabled = false;
        destinationSetter.enabled = false;
        destinationSetter.target = playerTarget;
    }

    private void Update()
    {

        float distanceFromPlayer = Vector2.Distance(playerTarget.position, transform.position);
        dir = (playerTarget.position - transform.position).normalized;

        
        rawAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angleAdjusted = rawAngle - 90f;

        angleRotated = transform.rotation.eulerAngles.z;


        // Debug.Log(angleRotated);
        // Debug.Log(rawAngle);
        CanSeePlayer();
        destinationSetter.PlayersLastSeePositiion(playersLastSeenPostion);
        // Debug.DrawLine(transform.position, playersLastSeenPostion, Color.red, 1);
        // Debug.Log(insideFOV);
        Debug.Log(hasBeenDetectedOnce);
        if ((distanceFromPlayer > detectionRadius || !insideFOV) && !hasBeenDetectedOnce)
        {
            // Debug.Log("1");
            patrolScript.enabled = true;
            destinationSetter.enabled = false;

        }
        else if (distanceFromPlayer < detectionRadius && distanceFromPlayer >= attackRadius && (insideFOV || hasBeenDetectedOnce))
        {
            // Debug.Log("2");
            patrolScript.enabled = false;
            destinationSetter.enabled = true;
            
            destinationSetter.Stop(false);
            CanSeePlayer();
            
            if(!insideFOV && (Vector3.Magnitude(transform.position -playersLastSeenPostion)<1f))
            {   
                // Debug.Log("inside");
                hasBeenDetectedOnce = false;
            }

        }
        else if (distanceFromPlayer < attackRadius && (insideFOV))
        {
            // Debug.Log("3");
            patrolScript.enabled = false;
            destinationSetter.enabled = true;
            
            destinationSetter.Stop(true);


            transform.rotation = Quaternion.AngleAxis(angleAdjusted, Vector3.forward);

        }
        else
        {   
            Debug.Log("4");
            patrolScript.enabled = false;
            destinationSetter.enabled = true;
            destinationSetter.Stop(false);
            

        }


    }

    private void CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
        
        if (hit.collider != null)
        {
            
            if (hit.transform.tag == "Player")
            {
                
                playersLastSeenPostion = hit.transform.position;
                CheckIfInFOV();


            }
            else
            {
                insideFOV = false;
            }

        }
    }

    void CheckIfInFOV()
    {
        
        float angleBetween ;
        GetReadableValuesOfRotatedAngle();
        angleBetween = (rawAngle - angleRotated);
        if(-halfFOV<=angleBetween&& angleBetween<=halfFOV)
        {
            insideFOV = true;
            hasBeenDetectedOnce = true;
        }
        else
        {
            insideFOV = false;
        }

       

    }

    void GetReadableValuesOfRotatedAngle()
    {   

        
        if(0<=angleRotated && angleRotated<270)
        {
            angleRotated = angleRotated + 90;
        }
        else if(270<=angleRotated&& angleRotated <360)
        {
            angleRotated=angleRotated - 270;
        }

        if( -180 <rawAngle && rawAngle <0)
        {
            rawAngle = rawAngle+ 360;
        }
        
    }


}
