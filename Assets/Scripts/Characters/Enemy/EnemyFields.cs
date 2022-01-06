using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    enemy1,
    enemy2,
    enemy3,
    enemy4,
    enemy5
}

[CreateAssetMenu(fileName = "EnemyFields", menuName = "Enemy")]
public class EnemyFields : ScriptableObject
{
    public EnemyType enemyType;
    public Sprite enemySprite;
    public string enemyName;
    public int hp;
    public int level;
    public int damage;
    public Vector3 scale;
}
