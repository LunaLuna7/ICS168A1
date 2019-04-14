using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public EnemyStats enemyStats;
    private float timeElapsed;
    float horizontalMove;

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
        timeElapsed -= Time.deltaTime;
        Move(1);

        if (hasAuthority == false)
        {
            return;
        }

        GetComponent<MeshRenderer>().enabled = false; //Enemy only invisible to the owner(its in reverse but easy to debug this way)



    }

}
