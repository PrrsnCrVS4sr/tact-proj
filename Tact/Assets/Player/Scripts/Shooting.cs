using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform barrel;
    [SerializeField] GameObject bulletPf;

    [SerializeField] float fireRate = 10;
    float lastFired;
    [SerializeField] float shootForce = 20;
    [SerializeField] Animator muzzleFlash;

    [SerializeField] int totalAmmoCount = 90;
    [SerializeField] int ammoPerMagazine = 30;
    int currentAmmoCount;
    [SerializeField] float reloadTime = 2f;
    float startTime =0;
    bool isReloading = false;

    Vector3 lastFiredLocation;

    public Vector3 LastFiredLocation{get{return lastFiredLocation;}}
    ObjectPool pool;


    void Start()
    {   
        foreach(Transform child in transform)
        {   
            
            if(child.tag == "Barrel")
            {
                barrel = child.GetComponent<Transform>();
                
                break;
            }
        }
        muzzleFlash = GetComponentInChildren<Animator>();
        pool = FindObjectOfType<ObjectPool>();
        currentAmmoCount = ammoPerMagazine;
        lastFiredLocation = new Vector3(0,0,-999);
    }

    
    void Update()
    {   
        
        
        if(Input.GetMouseButtonDown(0) && gameObject.tag == "Pistol" && !isReloading && totalAmmoCount>0)
        {
            Shoot(gameObject.tag);
            

        }
        else if(Input.GetMouseButton(0)&& gameObject.tag == "Rifle" && !isReloading && totalAmmoCount>0)
        {
            Shoot(gameObject.tag);
        }
        else if(isReloading || Input.GetKeyDown(KeyCode.R))
        {   
            
            isReloading = true;
            Debug.Log("Reloading");
            Reload();
            
        }
        
        

    }

    private void Reload()
    {   
        
        
       if(startTime<reloadTime) 
       {

           startTime += Time.deltaTime;
       }

       else
       {
           isReloading = false;
            totalAmmoCount -= (ammoPerMagazine-currentAmmoCount);
           if(totalAmmoCount>=ammoPerMagazine)
           {
               currentAmmoCount = ammoPerMagazine;
           }
           else
           {
            currentAmmoCount = totalAmmoCount;
           }
           
           startTime = 0;
           Debug.Log("reloading complete");
       }
        
        
        
        
    }

    private void Shoot(string tag)
    {   
        if(tag == "Pistol" && currentAmmoCount>0)
        {
        GameObject bullet =pool.SpawnFromPool(barrel.position,barrel.rotation);
        muzzleFlash.SetTrigger("Flash");
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(barrel.up * shootForce,ForceMode2D.Impulse);
        currentAmmoCount--;
        Debug.Log(currentAmmoCount);

        lastFiredLocation = gameObject.transform.position;
        
        }
        else if(tag =="Rifle" && currentAmmoCount>0)
        {
            if(Time.time - lastFired > 1/fireRate)
            {
                lastFired = Time.time;
                GameObject bullet =pool.SpawnFromPool(barrel.position,barrel.rotation);
                muzzleFlash.SetTrigger("Flash");
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(barrel.up * shootForce,ForceMode2D.Impulse);
                currentAmmoCount--;
                Debug.Log(currentAmmoCount);

                lastFiredLocation = gameObject.transform.position;
            }

        }
        else if(currentAmmoCount == 0)
        {
            isReloading = true;
        }

    }

   public  void  ResetLastGunFirePosition()
    {
        lastFiredLocation.z = -999;
    }

}
