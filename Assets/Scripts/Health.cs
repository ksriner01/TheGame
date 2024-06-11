using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private Behaviour[] components;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource hurtSfx;
    public float currentHealth { get; private set; }
    private Animator anim;
    private Boolean dead;
    private UIManager uiManager;
    public PlayerMovement grounded;
    public PauseMenu pause;

    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0) 
        {
            anim.SetTrigger("Hurt");
            hurtSfx.Play();
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour component in components)
                    component.enabled = false;
                rb.bodyType = RigidbodyType2D.Static;
                grounded.isGrounded();
                hurtSfx.Play();
                anim.SetBool("isDead", true);
                dead = true;
                //StartCoroutine (WaitTime());
                pause.GameOver();
            }
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.SetBool("isDead", false);
        anim.Play("HeroKnight_Idle");
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
    }

    //private void Update()
    //{
        //if (Input.GetKeyDown(KeyCode.E))
        //{
            //TakeDamage(1);
        //}
    //}
}
