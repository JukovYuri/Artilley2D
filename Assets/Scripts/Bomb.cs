using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public void StartFly(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(direction , ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
            // if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Enemy"))
            if (coll.gameObject.CompareTag("Enemy"))
                coll.SendMessage("ApplyDamage");
        }
        Destroy(gameObject, 2);
    }
}

