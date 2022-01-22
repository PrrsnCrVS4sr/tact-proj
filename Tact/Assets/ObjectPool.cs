using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] int poolSize = 10;
    public GameObject pf;
    Queue<GameObject> objectPool;


    private void Awake() {
        PopulatePool();
        
    }

    private void PopulatePool()
    {   
        objectPool = new Queue<GameObject>() ;
        for(int i =0 ; i<poolSize; i++)
        {
           
            GameObject objIst = Instantiate(pf);
            objectPool.Enqueue(objIst);
            objIst.SetActive(false);
            
        }
    }

    public GameObject SpawnFromPool(Vector2 postion, Quaternion rotation)
    {
        GameObject obj = objectPool.Dequeue();
        obj.SetActive(true);
        obj.transform.position = postion;
        obj.transform.rotation = rotation;
        objectPool.Enqueue(obj);



        return obj;
    }
}
