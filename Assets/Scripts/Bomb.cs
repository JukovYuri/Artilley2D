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

    public void Shot()
    {
        float AngleInDegrees = 45f;
        const float g = 9.8f;

        Vector3 direction = PlayerMovement.instance.transform.position - transform.position;
        direction.z = 0;
        //Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        //transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);


        float x = direction.magnitude;
        float y = direction.y;

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

       // GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v;


    }


}

