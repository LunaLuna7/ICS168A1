using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Deals with the player network logic
/// </summary>

public class PlayerConnection : NetworkBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject myPlayer;


    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer == false)
            return;
      
        CmdSpawnPlayer();
        //InvokeRepeating("CmdSpawnEnemy", 0, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == false)
            return;

    }

    [Command]
    void CmdSpawnPlayer()
    {
        //p its on the server
        GameObject p = Instantiate(player);
        myPlayer = p;

        //Propagates p to all clients
        NetworkServer.SpawnWithClientAuthority(p, connectionToClient);
    }
}
