using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : Entity
{

    public EnemyStats enemyStats;
    private float timeElapsed;
    float horizontalMove;
    public PlayerConnection playerConnection;
    public ParticleSystem ps;
   
    void Start()
    {
        timeElapsed = enemyStats.moveRate;
        ps = this.GetComponent<ParticleSystem>();
        ps.Stop();
    }

    public override void Move(float move)
    {
        if (timeElapsed <= 0)
        {
            timeElapsed = enemyStats.moveRate;
            transform.Translate(0, 0, enemyStats.moveDistance * move);
        }
    }
    

    
    public override void RpcOnDeath()
    {
     
    }

    void Update()
    {
        timeElapsed -= Time.deltaTime;
        Move(1);

        //GetComponent<MeshRenderer>().enabled = false;
        
        //if (playerConnection.netId)
        //{
        //    return;
        //S}

        //GetComponent<MeshRenderer>().enabled = true; //Enemy only visible to the owner



    }

}
