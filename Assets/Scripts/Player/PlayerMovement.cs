using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    bool imMain = false;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] float speed = 5;
    PlayerHealth playerHealth;
    float inputX;



    [Header("Ground")]
    bool isGround;
    bool facingRight = true;
    public LayerMask layerMask;

    [Header("Weapons")]
    public bool isAttacking;



    StatePlayer statePlayer;
    enum StatePlayer
    {
        Attack,
        Move
    }
    void Awake()
    {
        if (instance == null) instance = this;
        playerHealth = GetComponent<PlayerHealth>();
        statePlayer = StatePlayer.Move;
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

        isGround = Physics2D.Raycast(transform.position, -transform.up, 0.5f, layerMask);

        if (Input.touchCount == 0)
        {
            inputX = 0;
        }
        animator.SetFloat("Speed", Mathf.Abs(inputX));


        switch (statePlayer)
        {
            case StatePlayer.Move:
                if (isAttacking)
                    statePlayer = StatePlayer.Attack;
                if (imMain)
                    Move();
                break;
            case StatePlayer.Attack:
                if (!isAttacking)
                    statePlayer = StatePlayer.Move;
                Attack();
                break;
        }

        if (inputX > 0 && facingRight)
            Flip();

        else if (inputX < 0 && !facingRight)
            Flip();

    }
    void Move()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                float middleScreen = Screen.width / 3 + Screen.width / 3;


                if (touch.tapCount == 2 && isGround && touch.position.x < middleScreen && touch.position.x > Screen.width / 3)
                {
                    Jump(new Vector2(0, 1));
                    return;
                }
                else if (touch.tapCount == 2 && isGround && touch.position.x > middleScreen)
                {
                    Jump(new Vector2(0.5f, 1));
                    return;
                }
                else if (touch.tapCount == 2 && isGround && touch.position.x < Screen.width / 3)
                {
                    Jump(new Vector2(-0.5f, 1));

                    return;
                }
            }


            if (touch.phase == TouchPhase.Moved)
            {
                float middleScreen = Screen.width / 3;
                if (touch.position.x > middleScreen * 2)
                {
                    inputX = 1;
                }
                if (touch.position.x < middleScreen)
                {
                    inputX = -1;
                }
                rb.velocity = new Vector2(inputX * speed, rb.velocity.y);
            }


        }

    }

    void Jump(Vector2 direction)
    {
        animator.SetTrigger("Jump");
        rb.AddForce(direction * 10, ForceMode2D.Impulse);
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void Attack()
    {
        WeaponManager.instance.Attack();
    }

    public void Pacific()
    {
        isAttacking = false;
        imMain = false;
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -transform.up);
    }

    public void SetImMain(bool enable)
    {
        imMain = enable;
    }
}
