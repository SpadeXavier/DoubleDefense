﻿using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

// -- BASE CLASS ALL ENEMIES INHERIT FROM 
public abstract class Enemy : MonoBehaviour
{

    [Header("Health")]
    public int health;

    [Header("Left And Jump")]
    public bool leftAndJump;
    public float jumpDelayTime;


    [Header("Crawling")]
    public bool crawling;
    public float crawlDelayTime; 


    [Header("Other")]
    public Animator animator;
    public GameObject slimeParent; 
    public GameObject mark;
    public ParticleSystem explosionEffect;

    // -- private booleans 
    protected bool dying; 
    protected bool marked;

    // -- private floats 
    protected float jumpDelay;
    protected float crawlDelay; 
    
    // -- other 
    protected Vector3 pos; 

    protected void Start()
    {
        pos = transform.position; 
    }

    // Update is called once per frame
    protected void Update()
    {
        if(animator)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isCrawling", false);

        }

        // -- timers 
        jumpDelay -= Time.deltaTime;
        crawlDelay -= Time.deltaTime; 

        // -- movement 
        if (!dying)
        {
            if(leftAndJump && jumpDelay <= 0)
            {
                jumpDelay = jumpDelayTime; 
                LeftAndJump(); 
            }
            if(crawling && crawlDelay <= 0)
            {
                crawlDelay = crawlDelayTime;
                Crawl();
            }
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public bool isMarked()
    {
        return this.marked;
    }

    protected IEnumerator Die()
    {
        // -- this condition prevents explosion effect from occuring multiple times 
        if (!dying)
        {
            GameManager.instance.AddEnemyKilled();

            // -- stop movement 
            dying = true;

            // -- hide sprite 
            animator.enabled = false; // -- prevents idle animation from overriding sprite renderer  
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            this.mark.SetActive(false);

            // -- exploding effect
            explosionEffect.Play();

            // -- get explosion duration 
            var main = explosionEffect.main;
            yield return new WaitForSeconds(main.duration);

            Destroy(gameObject);
        }
    }

    public void ShowMark()
    {
        mark.SetActive(true);
        marked = true;
    }


    protected void LeftAndJump()
    {
        animator.SetBool("isJumping", true);

    }

    protected void Crawl()
    {
        animator.SetBool("isCrawling", true);
    }


}
