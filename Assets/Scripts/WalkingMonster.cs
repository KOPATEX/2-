using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
    public float speed = 3.5f;
    private Vector3 dir;
    [SerializeField] private SpriteRenderer sprite;
    //[SerializeField] private int lives = 3;
    private Rigidbody2D rb;
    private Animator anim;
    




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
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

    void Move()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
        if (collider.Length > 0) dir *= -1f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        State = States.WalkEnimy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives-=1;
             Debug.Log("у черта" + lives);
            
            State = States.attack;
        }
           
        if (lives < 1)
        {
           Die();
            
        }
    }
   
    public enum States
    {

        WalkEnimy,
       attack
    }
    
}

