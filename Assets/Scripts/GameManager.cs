using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    PlayerMovement playerMovement;
    public List<EnemyMovement> enemys = new List<EnemyMovement>();
    public List<EnemyMovement> emptyEnemys;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        playerMovement = PlayerMovement.instance;

    }
    private void Start()
    {
        playerMovement.imMain = true;
        emptyEnemys = enemys;
    }

    void Update()
    {
        if (playerMovement.imMain)
        {

        }
        else
        {
            Queue();
        }
    }




    void Queue()
    {
        if (emptyEnemys.Count > 0)
        {
            if (!emptyEnemys[0].imMain)
            {
                emptyEnemys[0].imMain = true;

                if (!emptyEnemys[0].imMain)
                {
                    emptyEnemys.Remove(emptyEnemys[0]);
                    emptyEnemys[1].imMain = true;
                }
            }

        }
        else
        {
            playerMovement.imMain = true;
            emptyEnemys = enemys;
        }

    }

    public void AddEnemy(EnemyMovement enemy)
    {
        enemys.Add(enemy);
    }

}
