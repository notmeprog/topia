using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Очки здоровья")]
    public int health = 100;
}
