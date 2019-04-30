using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : Entity
{
    [SerializeField] private float moveDistance = 1f; //How far away the player moves in PPU on each platform
    [SerializeField] private float moveRate = .2f; //How often should the Entity be able to move
    public int health;
    public int currentHealth;
    private CameraShake cameraShake;
    //private ParticleSystem ps;
    public Material deadMaterial;
    public Material defaultMaterial;
    private float timeElapsed;
    public bool playerDead;
    float horizontalMove;
    public static int playersDead;
    public GameObject GameOver;

    void Start()
    {
        //ps = this.GetComponent<ParticleSystem>();
        //ps.Stop();
        currentHealth = health;
        playerDead = false;
        timeElapsed = moveRate;
        cameraShake = FindObjectOfType<CameraShake>();
        //ps = this.GetComponent<ParticleSystem>();
        playersDead = 0;
    }

    public override void Move(float move)
    {     
        if(timeElapsed <= 0)
        {
           timeElapsed = moveRate;
           transform.Translate(moveDistance * move, 0, 0);
           //StartCoroutine(activateParticles(move));
        }
    }

    public override void RpcOnDeath()
    {
        GetComponentInChildren<Renderer>().material = deadMaterial;
        playersDead++;
        playerDead = true;
        StartCoroutine(cameraShake.shake(.5f, .8f));
        if(playersDead >= 2)
        {
            
        }
        else
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
        if (other.gameObject.CompareTag("Enemy") && !playerDead)
        {
            DamagePlayer(1);
        }
    }

    public void DamagePlayer(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            RpcOnDeath();
        
    }

    public IEnumerator Revive()
    {
        yield return new WaitForSeconds(6f);
        currentHealth = health;
        playerDead = false;
        GetComponentInChildren<Renderer>().material = defaultMaterial;
        playersDead--;
    }

    /*public IEnumerator activateParticles(float direction,float duration= .3f)
    {
     
        var main = ps.main;
        if (direction < 0) main.startRotation = main.startRotation;
        ps.Play();
        yield return new WaitForSeconds(duration);
        ps.Stop();

    }*/
}
