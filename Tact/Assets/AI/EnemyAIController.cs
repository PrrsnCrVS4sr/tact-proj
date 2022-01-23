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
    }

    private void Update() {
        float distanceFromPlayer = Vector2.Distance(playerTarget.position,transform.position);
        Vector3 dir = playerTarget.position - transform.position;
        
        rawAngle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg ;
        float angleAdjusted = rawAngle -90f;

        angleRotated = transform.rotation.eulerAngles.z;
         
        GetReadableValuesOfRotatedAngle();
        // Debug.Log(angleRotated);
        // Debug.Log(rawAngle);
        CheckIfInFOV();
        // Debug.Log(insideFOV);
        if(distanceFromPlayer>detectionRadius || !insideFOV )
        {   
            //Debug.Log("1");
            patrolScript.enabled = true;
            destinationSetter.enabled = false;
            
        }
        else if(distanceFromPlayer<detectionRadius && distanceFromPlayer>= attackRadius  &&insideFOV)
        {   
            //Debug.Log("2");
            patrolScript.enabled = false;
            destinationSetter.enabled = true;
            destinationSetter.target = playerTarget;
            destinationSetter.Stop(false);
        }
        else if(distanceFromPlayer<attackRadius &&insideFOV )
        {   
            //Debug.Log("3");
            destinationSetter.enabled = true;
            destinationSetter.target = playerTarget;
            destinationSetter.Stop(true);
            
            
            transform.rotation = Quaternion.AngleAxis(angleAdjusted, Vector3.forward);
            
        }
        
        
    }

    void CheckIfInFOV()
    {
        
        float angleBetween ;
        angleBetween = (rawAngle - angleRotated);
        if(-halfFOV<=angleBetween&& angleBetween<=halfFOV)
        {
            insideFOV = true;
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
