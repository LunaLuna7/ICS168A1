﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*This is a class that can spawn enemies in any of the 10 columns.
We can ramp up the game so that we spawn a certain amount of enemies
every wave. */
public class EnemyManager : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private int waveNum = 1; //current wave number, can use to determine what enemy to spawn
    [SerializeField]private int waveAmplifier = 3; //how many levels before making the game harder
    [SerializeField]private int baseNumEnemies = 3; //how many enemies there will be at least
    [SerializeField]private GameObject enemy;
    private GameObject enemySpawnObject;
    private List<Transform> enemySpawnLocations = new List<Transform>(); //holds all spawn locations in game
    private List<int> currentlyUsedLocations = new List<int>(); //tracks current indexes, ensuring no repeats
    public PlayerConnection playerConnection;

    private void Awake()
    {
        playerConnection = GetComponent<PlayerConnection>();
    }

    void Start()
    {
        //if (isLocalPlayer == false)
          //  return;
        
        enemySpawnObject = GameObject.FindGameObjectWithTag("spawnLocations");
        foreach(Transform child in enemySpawnObject.transform)
        {
            enemySpawnLocations.Add(child);
        }

        if (enemy == null)
        {
            throw new MissingReferenceException();
        }

        InvokeRepeating("CmdSpawnEnemy", 0, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //returns a transform within the enemySpawnLocations that hasn't been used
    Transform RandomPoint()
    {
        //while loop until return
        while(currentlyUsedLocations.Count != enemySpawnLocations.Count)
        {
            int index = Random.Range(0, enemySpawnLocations.Count);

            if (!currentlyUsedLocations.Contains(index))
            {
                currentlyUsedLocations.Add(index);
                return enemySpawnLocations[index];
            }
        }

        //this is just here so the compiler will stop yelling at me! IGNORE
        return enemySpawnLocations[0];
    }

    [Command]
    void CmdSpawnEnemy()
    {
        //How many enemies to spawn
        int enemyNumber = (waveNum/waveAmplifier) + baseNumEnemies;
        //ensure that the number of enemies does not exceed the number of columns-1, or else game is impossible
        if (enemyNumber >= 10)
            enemyNumber = enemySpawnLocations.Count - 1;

        for (int i = 0; i < enemyNumber; i++)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.tag = "Enemy";
            newEnemy.transform.position = RandomPoint().position;
            NetworkServer.SpawnWithClientAuthority(enemy, connectionToClient);
        }

        currentlyUsedLocations = new List<int>();

        Debug.Log(waveNum);
        waveNum++;
        Debug.Log("WHAT are you: " + playerConnection.isLocalPlayer);
        enemy.GetComponent<MeshRenderer>().enabled = false;
        if (playerConnection.isLocalPlayer)
            enemy.GetComponent<MeshRenderer>().enabled = true;

        
    }
}
