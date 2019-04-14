using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Contain all teh functionalities of the game living objects(players, enemies)
/// </summary>
public abstract class Entity : NetworkBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
    abstract public void Move(float move);
    abstract public void OnDeath();
}
