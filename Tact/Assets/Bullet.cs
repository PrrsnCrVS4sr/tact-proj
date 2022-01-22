using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float autoDestroyTime = 2f;



    private void OnEnable() {
        Invoke("DestroyObject",autoDestroyTime);
    }
    private void OnCollisionEnter2D(Collision2D other) {
            Invoke("DestroyObject",1f);
    }



    void  DestroyObject()
    {
        gameObject.SetActive(false);
    }
}
