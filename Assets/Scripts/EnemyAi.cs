using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public float speed;
    private float distance;
    public float distanceBetween;
    private bool isFacingLeft = true;
    private float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > distanceBetween)
        {
            anim.SetBool("IsMoving", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        } else
        {
            anim.SetBool("IsMoving", false);
        }

        Flip();
    }
    private void Flip()
    {
        if (isFacingLeft && horizontal < 0f || !isFacingLeft && horizontal > 0f)
        {
            isFacingLeft = !isFacingLeft;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
