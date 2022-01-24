using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    [SerializeField]float moveSpeed = 2f;
    [SerializeField] Camera cam;

    
    Vector2 movement;
    Vector2 relMovement;
    Vector2 mousePos;

    Vector2 lookDirection;
    float theta;
    
    Health playerHealth;

    private void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<Health>();
    }
    void Update()
    {  
        if(playerHealth.CurrentHealth <=0)
        {
            Death();
        }
        relMovement.x = Input.GetAxisRaw("Horizontal");
        relMovement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        lookDirection = mousePos - playerRB.position  ;

        theta =  Mathf.Atan2(lookDirection.y,lookDirection.x);

        
        movement.x = relMovement.x * Mathf.Sin(theta) + relMovement.y * Mathf.Cos(theta);
        movement.y = relMovement.y * Mathf.Sin(theta) - relMovement.x * Mathf.Cos(theta);

        
        
        

    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate() {
        
        playerRB.MovePosition(playerRB.position + movement * moveSpeed * Time.fixedDeltaTime);

        
        float angle =  Mathf.Atan2(lookDirection.y,lookDirection.x)*Mathf.Rad2Deg - 90f;
        playerRB.rotation = angle;

        
    }
}
