using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    public Tilemap terrain;
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

        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(DestroyGround(transform.position, 2, 1f));
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

            //if (coll.gameObject.CompareTag("Ground"))
            //{
            //    Destroy(coll.gameObject);
   
            //}

        }
        Destroy(gameObject, 2);
    }

    IEnumerator DestroyGround(Vector3 explosionCentre, float explosionRadius, float time)
    {
        yield return new WaitForSeconds(time);

        for (int x = -(int)explosionRadius; x < explosionRadius; x++)
        {
            for (int y = -(int)explosionRadius; y < explosionRadius; y++)
            {

                Vector3Int tilePosition = terrain.WorldToCell(explosionCentre + new Vector3(x, y, 0));
                if (terrain.GetTile(tilePosition) != null)
                {
                    terrain.SetTile(tilePosition, null);
                }

            }
        }

    }


}

