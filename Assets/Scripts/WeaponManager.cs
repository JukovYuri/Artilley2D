using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponManager : MonoBehaviour, IPointerDownHandler
{
    public static WeaponManager instance;
    [SerializeField] Animator animator;
    [SerializeField] Animator hammerAnimator, gunAnimator;

    public GameObject aim;

    [Header("Gun")]
    public GameObject gun;
    bool isGun;
    [Header("Hammer")]
    public GameObject hammer;
    bool isHammer;

    [Header("Bomb")]
    public GameObject bomb;
    public Transform bombPosition;
    GameObject emptyBomb;
    bool isBomb;

    public GameObject bullet;

    PlayerMovement playerMovement;
    float pointerDownTimer = 0;

    Vector3 direction;
    bool isButton;
    private void Start()
    {
        if (instance == null) instance = this;
        playerMovement = PlayerMovement.instance;
    }
    private void Update()
    {
        if (!playerMovement.isAttacking)
        {
            WeaponsOff();
        }

        if (isButton)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)
            {
                pointerDownTimer += Time.deltaTime;
                if (pointerDownTimer > 2)
                {
                    aim.SetActive(true);
                }

            }
            else
            {
                isButton = false;
                pointerDownTimer = 0;
            }
        }
    }
    public void GetGun()
    {
        playerMovement.isAttacking = true;
        isGun = true;
        gun.SetActive(true);
    }
    public void GetBomb()
    {
        isBomb = true;
        emptyBomb = Instantiate(bomb, bombPosition.position, Quaternion.identity);
        playerMovement.isAttacking = true;

    }
    public void GetHammer()
    {
        playerMovement.isAttacking = true;
        isHammer = true;
        hammer.SetActive(true);
    }

    public void Attack()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if (isHammer)
                {
                }
                if (isGun)
                {
                    direction = Camera.main.ScreenToWorldPoint(touch.position) - gun.transform.position;
                    direction.z = 0;
                    if (aim)
                    {
                        aim.transform.right = direction * PlayerMovement.instance.transform.lossyScale.x;
                        gun.transform.right = -direction * PlayerMovement.instance.transform.lossyScale.x;
                    }
                }
                if (isBomb)
                {
                    if (emptyBomb)
                        emptyBomb.transform.position = bombPosition.position;


                    direction = Camera.main.ScreenToWorldPoint(touch.position) - gun.transform.position;
                    direction.z = 0;

                    if (aim)
                        aim.transform.right = direction * PlayerMovement.instance.transform.lossyScale.x;
                }


            }
            else if (touch.phase == TouchPhase.Ended && touch.tapCount > 1)
            {
                if (isHammer)
                {
                    animator.SetTrigger("AttackHammer");
                    hammerAnimator.SetTrigger("Attack");
                    print("hammer");
                }
                if (isGun)
                {
                    animator.SetTrigger("AttackGun");
                    gunAnimator.SetTrigger("Attack");
                    print("gun");
                }
                if (isBomb)
                {
                    animator.SetTrigger("AttackBomb");
                    if (aim)
                        emptyBomb.GetComponent<Bomb>().StartFly(-aim.transform.right * 10);
                    else
                        emptyBomb.GetComponent<Bomb>().StartFly(transform.right);

                    print("boom");
                }
            }
        }




    }


    void WeaponsOff()
    {
        aim.SetActive(false);
        gun.SetActive(false);
        hammer.SetActive(false);
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData != null)
            isButton = true;
    }
}

