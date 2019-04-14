using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All the stats of the Enemies that will be spawn on the game
/// </summary>
[CreateAssetMenu(menuName = "Enemy")]
public class EnemyStats : ScriptableObject
{
    public EnemyType type;
    public float moveDistance;
    public float moveRate;
}

public enum EnemyType
{
    normal, //Both players can see
    player1, //Player1 can see
    player2 //Player2 can see
}
