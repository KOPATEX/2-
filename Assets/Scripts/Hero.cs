using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Entity
{
    // [SerializeField] private AudioSource jumpSound;
    // [SerializeField] private AudioSource damageSound;
    //  [SerializeField] private AudioSource missAttack;
    //  [SerializeField] private AudioSource attackMob;
    [SerializeField] private int health;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private GameObject losePanel;

    private bool isGrounded = false;
    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    public Joystick joystick;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private SpriteRenderer sprite;
    public static Hero Instance { get; set; }


    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    private void Awake()
    {
        lives = 5;
        health = lives;
        Instance = this;
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isRecharged = true;
        losePanel.SetActive(false);
    }

    private void Run()
    {
        if (isGrounded) State = States.run1;
        Vector3 dir = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Update()
    {
        if (isGrounded && !isAttacking && health > 0) State = States.idle;

        if (!isAttacking && joystick.Horizontal != 0 && health > 0)
            Run();
        if (!isAttacking && isGrounded && joystick.Vertical > 0.5 && health > 0)
            Jump();


        if (health > lives)
            health = lives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;
            
            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        // jumpSound.Play();
    }

    public enum States
    {
        idle,
        run1,
        jump,
        attack,
        death
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (!isGrounded && health > 0)
            State = States.jump;
    }
    public override void GetDamage()
    {
        health -= 1;

        //damageSound.Play();
        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
        }
    }
    public override void Die()
    {
        State = States.death;
        Invoke("SetLosePanel", 1.1f);

    }
    private void SetLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }
    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        if (colliders.Length == 0)
        {
            // missAttack.Play();
        }
        else
            //attackMob.Play();

            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetComponent<Entity>().GetDamage();
                StartCoroutine(EnemyOnAttack(colliders[i]));
            }
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }
    private IEnumerator EnemyOnAttack(Collider2D enemy)
    {
        SpriteRenderer enemyColor = enemy.GetComponentInChildren<SpriteRenderer>();
        enemyColor.color = new Color(1f, 0.4375f, 0.4375f);
        yield return new WaitForSeconds(0.2f);
        enemyColor.color = new Color(1, 1, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Coin"))
        {
            CoinScore.coinCount += 1;
            Destroy(collision.gameObject);
        }
    }
}