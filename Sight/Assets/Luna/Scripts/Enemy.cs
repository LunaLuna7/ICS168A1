using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public EnemyStats enemyStats;
    private float timeElapsed;
    float horizontalMove;
    public PlayerConnection playerConnection;
   
    void Start()
    {
        timeElapsed = enemyStats.moveRate;
    }

    public override void Move(float move)
    {
        if (timeElapsed <= 0)
        {
            timeElapsed = enemyStats.moveRate;
            transform.Translate(0, 0, enemyStats.moveDistance * move);
        }
    }

    public override void OnDeath()
    {
        
    }

    void Update()
    {
        Debug.Log(netId);
        timeElapsed -= Time.deltaTime;
        Move(1);

        GetComponent<MeshRenderer>().enabled = false;
        
        //if (playerConnection.netId)
        //{
        //    return;
        //S}

        GetComponent<MeshRenderer>().enabled = true; //Enemy only visible to the owner



    }

}
