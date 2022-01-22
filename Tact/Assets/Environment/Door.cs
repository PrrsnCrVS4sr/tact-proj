using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator door;
    bool canInteract = false;
    private void Awake()
    {
        door = GetComponent<Animator>();
    }

    private void Update()
    {

        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractWithDoor();
            }
        }

    }


    private void InteractWithDoor()
    {
        door.SetTrigger("Interact");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Yooure now in door interact range");
            canInteract = true;

        }


    }
    private void OnTriggerExit2D(Collider2D other) {
        
        if (other.tag == "Player")
        {
            
            canInteract = false;

        }
    }
}
