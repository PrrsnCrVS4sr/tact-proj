using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float totalHealth = 100;
   
    [SerializeField]float currentHealth;

     public float CurrentHealth{get{return currentHealth;}}

    private void Start() {
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    public void EventAnyDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<Bullet>())
        {   Bullet bullet = other.gameObject.GetComponent<Bullet>();
            EventAnyDamage(bullet.DamagePerBullet);
        }
    }
}
