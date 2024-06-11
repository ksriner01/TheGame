using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float distanceBetween;
    [SerializeField] private Transform enemy;
    [SerializeField] private AudioSource hurtSfx;
    [SerializeField] private AudioSource attackSfx;

    public Animator anim;
    public Rigidbody2D rb2d;
    public GameObject player;
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = false;
    public float speed;
    private float distance;
    private float cooldownTimer = Mathf.Infinity;
    public int maxHealth = 100;
    private Health playerHealth;
    int currentHealth;
    public Scoring score;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                rb2d.velocity= Vector2.zero;
                anim.SetTrigger("Attack");
            }
        }

        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < distanceBetween)
        {
            anim.SetBool("IsMoving", true);
            bool isMovingRight = DetermineMovement();
            if (isMovingRight && !isFacingRight)
            {
                Flip();
            }
            else if (!isMovingRight && isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            anim.SetBool("IsMoving", false);
            rb2d.velocity = Vector2.zero;
        }
    }

    private void Flip()
    {
        if (!IsDead())
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x = -transform.localScale.x;
            transform.localScale = scale;
        }
    }

    private bool DetermineMovement()
    {
        anim.SetBool("IsMoving", true);
        if (transform.position.x < player.transform.position.x)
        {
            rb2d.velocity = new Vector2(speed, 0);
            return true;
        }
        else
        {
            rb2d.velocity = new Vector2(-speed, 0);
            return false;
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + colliderDistance * range * transform.localScale.x * -transform.right, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + colliderDistance * range * transform.localScale.x * -transform.right, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            //Physics2D.OverlapCircleAll(circleCollider.bounds.center + colliderDistance * range * transform.localScale.x * -transform.right, circleCollider.radius, playerLayer);
            playerHealth.TakeDamage(damage);
            attackSfx.Play();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");
        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        anim.SetBool("IsMoving", false);
        anim.SetBool("IsDead", true);
        score.AddScore(1);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<EnemyAi>().enabled = false;
        this.enabled = false;
    }
}
