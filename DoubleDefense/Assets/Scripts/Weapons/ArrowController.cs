﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float pullbackLength = .2f; 

    private int damage; 

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetDamage(int damage)
    {
        this.damage = damage; 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>(); 
        if(enemy)
        {
            enemy.TakeDamage(damage); 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            DestroyArrow(); 
        }
    }

    void DestroyArrow()
    {
        Destroy(gameObject); 
    }
    
    public void ReleaseAnimation()
    {
        this.GetComponent<Animator>().SetBool("isShot", true); 
    }

    public void LoadAnimation()
    {
        this.transform.position = this.transform.position + new Vector3(pullbackLength, 0, 0); 
    }

    public void PutBackAnimation()
    {
        this.transform.position = this.transform.position + new Vector3(pullbackLength, 0, 0);

    }
}
