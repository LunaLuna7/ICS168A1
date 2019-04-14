using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float moveDistance = 1f; //How far away the player moves in PPU on each platform
    [SerializeField] private float moveRate = .2f; //How often should the Entity be able to move
    private float timeElapsed;
    float horizontalMove;

    void Start()
    {
        timeElapsed = moveRate;    
    }

    public override void Move(float move)
    {     
        if(timeElapsed <= 0)
        {
           timeElapsed = moveRate;
           transform.Translate(moveDistance * move, 0, 0);   
        }
    }

    public override void OnDeath()
    {
        
    }

    void Update()
    {
        if (hasAuthority == false)
        {
            return;
        }

        timeElapsed -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(-1);
        

    }

}
