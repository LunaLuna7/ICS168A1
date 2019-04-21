using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float moveDistance = 1f; //How far away the player moves in PPU on each platform
    [SerializeField] private float moveRate = .2f; //How often should the Entity be able to move
    public int health;
    public int currentHealth;
    public Material deadMaterial;
    public Material defaultMaterial;
    private float timeElapsed;
    public bool playerDead;
    float horizontalMove;

    void Start()
    {
        currentHealth = health;
        playerDead = false;
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
        playerDead = true;
        GetComponentInChildren<Renderer>().material = deadMaterial;
        StartCoroutine(Revive());
    }

    void Update()
    {
        if (hasAuthority == false)
        {
            return;
        }

        timeElapsed -= Time.deltaTime;


        if (!playerDead && Input.GetKeyDown(KeyCode.RightArrow))
            Move(1);
        if (!playerDead && Input.GetKeyDown(KeyCode.LeftArrow))
            Move(-1);
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DamagePlayer(1);
        }
    }

    public void DamagePlayer(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            OnDeath();
        
    }

    public IEnumerator Revive()
    {
        yield return new WaitForSeconds(3f);
        currentHealth = health;
        playerDead = false;
        GetComponentInChildren<Renderer>().material = defaultMaterial;
    }
}
