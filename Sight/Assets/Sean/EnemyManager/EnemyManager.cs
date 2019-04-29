using System.Collections;
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
    public PlayerConnection playerConnection1;
    public PlayerConnection PlayerConnection2 = new PlayerConnection();
    public static int EnemyManagers;

    private void Awake()
    {
        //================update=============
        //So I basically kill the second EnemyManager since one is spawn for each player so there is only 1 on the scene
        //from the first connected player
        EnemyManagers++;
        if (EnemyManagers > 1)
            this.enabled = false;
    }

    void Start()
    {
        
        //Need to comment out so there is an enemySpawner on the other guys computer and then we can do the visibility base
        //on authority
        
        //if (isLocalPlayer == false)
          //  return;
        
        enemySpawnObject = GameObject.FindGameObjectWithTag("spawnLocations");

        foreach (Transform child in enemySpawnObject.transform)
        {
            enemySpawnLocations.Add(child);
        }

        if (enemy == null)
        {
            throw new MissingReferenceException();
        }

        InvokeRepeating("RpcSpawnEnemy", 0, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerConnection1 == null)
            playerConnection1 = GetComponent<PlayerConnection>();
 
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

    [ClientRpc]
    void RpcSpawnEnemy()
    {

        Debug.Log(playerConnection1.isLocalPlayer);
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
            NetworkServer.SpawnWithClientAuthority(newEnemy, connectionToClient);

            int playerToSee = Random.Range(0, 2);

            //================update=============
            //some werid stuff about randomising who can see waht base on playerConnection is local player. Atm this doesnt seem to
            //be working completely since I think what this guy spawns is not actually on the otehr players? I dunno play around wtih it

            if (playerToSee == 0)
            {
                Debug.Log("Owner can see");
                if (playerConnection1.isLocalPlayer)
                {
                    Debug.Log("A");
                    newEnemy.GetComponent<MeshRenderer>().enabled = true;
                }
                else if (!playerConnection1.isLocalPlayer)
                {
                    Debug.Log("B");
                    newEnemy.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            else
            {
                Debug.Log("Owner cant see");
                if (playerConnection1.isLocalPlayer)
                {
                    Debug.Log("C");
                    newEnemy.GetComponent<MeshRenderer>().enabled = false;
                }
                else if (!playerConnection1.isLocalPlayer)
                {
                    Debug.Log("D");
                    newEnemy.GetComponent<MeshRenderer>().enabled = true;
                }
            }

        }

        currentlyUsedLocations = new List<int>();
        waveNum++;

       


        
    }
}
