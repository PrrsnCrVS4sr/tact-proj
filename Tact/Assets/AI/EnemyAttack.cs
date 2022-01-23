using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    ObjectPool objectPool;
    [SerializeField] Animator muzzleFl;

    [SerializeField] int ammoPerMag = 12;
    int currentAmmoCnt;
    float shootF = 20;
    float reloadT = 2;
    float startT = 0;
    bool reloading = false;
    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        muzzleFl = GetComponentInChildren<Animator>();
        currentAmmoCnt = ammoPerMag;
    }

    private void Update()
    {
        if(currentAmmoCnt ==0)
        {   
            Debug.Log("Enemy reloading");
            reloading = true;
            startT += Time.deltaTime;
            if(startT>reloadT)
            {
               startT =0;
               currentAmmoCnt = ammoPerMag;
               reloading = false;
            }
        }

    }

    public void ShootBullet()
    {
        if (!reloading)
        {
            GameObject bullet = objectPool.SpawnFromPool(transform.position, transform.rotation);
            muzzleFl.SetTrigger("Flash");
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * shootF, ForceMode2D.Impulse);
            currentAmmoCnt--;
        }
    }
}
