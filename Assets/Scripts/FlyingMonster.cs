using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingMonster : Entity
{
    private SpriteRenderer sprite;
    [SerializeField] private AIPath aiPath;
    private Collider2D col;
    private Animator anim;


    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        lives = 2;
        aiPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.flipX = aiPath.desiredVelocity.x <= 0.01f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();

            //State = States.attack;
        }
    }
    public override void Die()
    {
        col.isTrigger = true;
        anim.SetTrigger("death");
        aiPath.enabled = false;
    }
}
