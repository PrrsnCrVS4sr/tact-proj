using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float autoDestroyTime = 3f;
    float damagePerBullet = 10;
    public float DamagePerBullet{get{return damagePerBullet;}}

    private void OnEnable() {
        Invoke("DestroyObject",autoDestroyTime);
    }
    private void OnCollisionEnter2D(Collision2D other) {
            Invoke("DestroyObject",0.25f);
    }



    void  DestroyObject()
    {
        gameObject.SetActive(false);
    }
}
