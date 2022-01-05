using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player" , menuName = "Player")]
public class PlayerFields : ScriptableObject
{
    public int level;
    public float hp;
    public float hit;
    public float xp;
    public int bonus;
}
