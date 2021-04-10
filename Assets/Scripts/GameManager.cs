using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;
    PlayerMovement playerMovement;
    public List<EnemyMovement> enemys = new List<EnemyMovement>();

    //public List<EnemyMovement> emptyEnemys;

    public float timeMove;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        playerMovement = PlayerMovement.instance;

    }
    private void Start()
    {
        StartCoroutine(Queue());
        //emptyEnemys = enemys;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
        }

        //    if (playerMovement.imMain)
        //    {
        //        // ход игрока
        //    }
        //    else
        //    {
        //        Queue();
        //    }
    }

IEnumerator Queue()
    {
        PlayerMovement.instance.SetImMain(true);
        yield return new WaitForSeconds(timeMove);
        PlayerMovement.instance.SetImMain(false);

        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].SetImMain(true);
            yield return new WaitForSeconds(timeMove);
            enemys[i].SetImMain(false);
        }

        StartCoroutine(Queue());

        //if (PlayerMovement.instance.живой)
        //{
        //    StartCoroutine(Queue());
        //}

    }




    //void Queue()
    //{
    //    if (emptyEnemys.Count > 0)
    //    {
    //        if (!emptyEnemys[0].imMain)
    //        {
    //            emptyEnemys[0].imMain = true;

    //            if (!emptyEnemys[0].imMain)
    //            {
    //                emptyEnemys.Remove(emptyEnemys[0]);
    //                emptyEnemys[1].imMain = true;
    //            }
    //        }

    //    }
    //    else
    //    {
    //        playerMovement.imMain = true;
    //        emptyEnemys = enemys;
    //    }

    //}

    public void AddEnemy(EnemyMovement enemy)
    {
        enemys.Add(enemy);
    }

}
