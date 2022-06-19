using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
    public float speed = 3.5f;
    private Vector3 dir;
    [SerializeField] private SpriteRenderer sprite;

    public bool isAttacking = false;
    private Rigidbody2D rb;
    private Animator anim;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
   
    void Start()
    {
        dir = transform.right;
        lives = 1;
    }
    private void Update()
    {
        Move();
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private IEnumerator AttackAnimation()
    {
        
        yield return new WaitForSeconds(1f);
        
    }

    void Move()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
        if (collider.Length > 0)
        {
            State = States.attack;
            isAttacking = true;
            StartCoroutine(AttackAnimation());
            dir *= -1f;
        }
        else
        {
            if (!isAttacking) State = States.WalkEnemy;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives-=1;
            //Debug.Log("у черта" + lives);
            
            State = States.attack;
        }
           
        if (lives < 1)
        {
           Die();
            
        }
    }
   
    public enum States
    {

       WalkEnemy,
       attack
    }
    
}

