using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnManager : MonoBehaviour
{
    
    public static UnitSpawnManager Inst { get; private set; }
    void Awake()=>Inst = this;


    public bool SpawnUnit(Vector3 spawnPos,Card card)
    {
        
        GameObject unit = Instantiate(Resources.Load(card.item.name) as GameObject, spawnPos, Quaternion.identity);

        return true;
    }
}
