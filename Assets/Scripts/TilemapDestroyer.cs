using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDestroyer : MonoBehaviour
{
    public Tilemap terrain;

    public void DestroyGround(Vector3 explosionCentre, float explosionRadius)
    {
        for (int x = -(int)explosionRadius; x < explosionRadius; x++)
        {
            for (int y = -(int)explosionRadius; y < explosionRadius; y++)
            {

                Vector3Int tilePosition = terrain.WorldToCell(explosionCentre + new Vector3(x, y, 0));
                if (terrain.GetTile(tilePosition) != null)
                {
                    DestroyTile(tilePosition);
                }

            }
        }

        void DestroyTile (Vector3Int tilePosition)
        {
            terrain.SetTile(tilePosition, null);
        }

    }
}
