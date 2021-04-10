using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    float force = 10;

    void Start()
    {
        //GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            GetComponent<Animator>().SetTrigger("Boom");
            BoomEffect();
        }
    }

    public void BoomEffect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3);
        foreach (var coll in colliders)
        {
            if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Enemy"))
                coll.SendMessage("ApplyDamage");
        }
        Destroy(gameObject, 2);
    }
}
