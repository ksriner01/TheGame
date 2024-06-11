using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    private float horizontal;
    private float speed = 8f;
    private float jumpPower = 16f;
    private bool isFacingRight = true;
    private float timeSinceAttack;
    private int currentAttack;
    private float waitTime;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public SoundEffectsPlayer audiosfx;
    public Enemy[] enemies;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource movingSfx;
    [SerializeField] private AudioSource attackSfx;
    [SerializeField] private AudioSource enemySfx;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(horizontal));

        timeSinceAttack += Time.deltaTime;

        waitTime += Time.deltaTime;

        if (anim.Equals("HeroKnight_Run"))
        {
            //movingSfx.Play();
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            anim.SetBool("IsJumping", true);
        } 

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", true);
        } else if (Input.GetButtonUp("Jump")) {
            anim.SetBool("IsJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.J) && timeSinceAttack > 0.25f)
        {
            currentAttack++;

            if (currentAttack > 3)
            {
                currentAttack = 1;
            }
            if (timeSinceAttack > 1.0f)
            {
                currentAttack = 1;
            }
            anim.SetTrigger("Attack" + currentAttack);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemyCollider in hitEnemies)
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null && enemy.IsAlive())
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                    enemySfx.Play();
                }
            }
            timeSinceAttack = 0.0f;
            attackSfx.Play();
        }

        if (isGrounded()) 
        {
            anim.SetBool("IsFalling", false);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
