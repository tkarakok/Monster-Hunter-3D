using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Player" , menuName = "Player")]
public class PlayerFields : ScriptableObject
{
    public Sprite playerSprite;
    public string playerName;
    public int level;
    public float hp;
    public float hit;
    public float xp;
    public int bonus;
}
