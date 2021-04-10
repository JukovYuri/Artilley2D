using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    bool imMain = false;
    public GameObject bomb;
    public Transform bombPosition;
    GameObject emptyBomb;


    Rigidbody2D rb;
    Animator animator;
    [SerializeField] float speed = 3;
    [SerializeField] float timerMove = 5;
    float health = 5;
    bool facingRight;
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //GameManager.instance.AddEnemy(this);
    }
    void Update()
    {

        // тут способность принимать урон
        // тут анимация

        if (!imMain)
        {
            return;
        }

       print(gameObject.name + " готов к бою!");


        //if (imMain)
        //{
        //    if (timerMove >= 0)
        //    {
        //        timerMove -= Time.deltaTime;
        //        Move();
        //    }
        //    else
        //    {
        //        DoDashDamage();
        //        timerMove = 3;
        //    }
        //}
        //else
        //{
        //    animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        //}
    }
    void Move()
    {
        float distToPlayer = PlayerMovement.instance.transform.position.x - transform.position.x;
        rb.velocity = new Vector2(distToPlayer / Mathf.Abs(distToPlayer) * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (distToPlayer < 0 && facingRight)
            Flip();

        else if (distToPlayer > 0 && !facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void DoDashDamage()
    {
        emptyBomb = Instantiate(bomb, bombPosition.position, Quaternion.identity);
        emptyBomb.GetComponent<Bomb>().Shot();
        animator.SetTrigger("AttackBomb");
    }
    public void Pacific()
    {
        imMain = false;
    }

    public void ApplyDamage()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetImMain(bool enable)
    {
        imMain = enable;
    }
}
